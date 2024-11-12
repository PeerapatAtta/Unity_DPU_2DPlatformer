using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Rigidbody rb;
    public int ballspeed = 0;
    public int jumpspeed = 0;
    private bool istouching = false;

    public GameObject cube;

    public TMPro.TextMeshProUGUI textUI;
    public TMPro.TextMeshProUGUI textUITimer;
    private int currentScore = 0;
    public float currentTime = 0f;
    public float maxTime = 60f;
    // private bool isGameOver = false; // ตัวแปรเช็คสถานะของเกมว่าจบหรือยัง


    //respawn coin
    public GameObject coinPrefab;

    //Object pooling
    public int sizeOfCoin = 10;
    public GameObject[] coins;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Object pooling
        coins = new GameObject[sizeOfCoin];

        for (int i = 0; i < sizeOfCoin; i++)
        {
            coins[i] = Instantiate(coinPrefab, new Vector3(Random.Range(-5f, 5f), Random.Range(1f, 2f), Random.Range(-5f, 5f)), coinPrefab.transform.rotation);
        }


    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = "Score: " + currentScore;
        textUITimer.text = "Time: " + currentTime;
        currentTime -= Time.deltaTime;


        //Move
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(1f * ballspeed, 0f, 0f), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(-1f * ballspeed, 0f, 0f), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0f, 0f, -1f * ballspeed), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0f, 0f, 1f * ballspeed), ForceMode.Force);
        }
        //Jump
        if (Input.GetKey(KeyCode.Space) && istouching)
        {
            rb.AddForce(new Vector3(0f, 1f * jumpspeed, 0f), ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            istouching = false;
        }

    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
        {
            istouching = true;
        }
    }

    //Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            CollectCoin(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

    }

    //Collect coin
    private void CollectCoin(GameObject coin)
    {
        currentScore++;
        // Destroy(coin);
        coin.SetActive(false);

        //wait 3 seconds -> respawn randomly on the map
        StartCoroutine(RespawnCoin(coin));
    }

    //Respawn coin
    private IEnumerator RespawnCoin(GameObject coin)
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Respawn Coin");
        // Instantiate(coinPrefab, new Vector3(Random.Range(-3f, 3f), Random.Range(0f, 1f), Random.Range(-3f, 3f)), coinPrefab.transform.rotation);
        coin.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 1f), Random.Range(-5f, 5f));
        coin.SetActive(true);
    }


}
