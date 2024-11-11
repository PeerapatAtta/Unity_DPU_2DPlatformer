using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public AudioClip coinSound; // เพิ่มตัวแปรสำหรับเก็บ AudioClip

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        Collider2D c;
        if (!GetComponent<Collider2D>())
        {
            c = gameObject.AddComponent<BoxCollider2D>();
        }
        else
        {
            c = GetComponent<Collider2D>();
        }
        c.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Text txt = GameObject.Find("/Canvas/Text").GetComponent<Text>();
        GameManager.nScore++;
        txt.text = "Coin Score = " + GameManager.nScore;

        // สร้างวัตถุชั่วคราวเพื่อเล่นเสียงเก็บเหรียญ
        GameObject audioObject = new GameObject("CoinSound");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = coinSound;
        audioSource.Play();

        // ทำลายวัตถุเสียงหลังเสียงเล่นจบ
        Destroy(audioObject, coinSound.length);

        // ซ่อนเหรียญและตั้งเวลา Respawn
        gameObject.SetActive(false);
        Invoke("Respawn", 3f);
    }

    void Respawn()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}
