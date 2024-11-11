using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public AudioClip coinSound;

    private void Start()
    {
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

        // เล่นเสียงเก็บเหรียญ
        GameObject audioObject = new GameObject("CoinSound");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = coinSound;
        audioSource.Play();
        Destroy(audioObject, coinSound.length);

        // ซ่อนเหรียญและแจ้งให้ CoinPool เริ่มการ Respawn หลังจาก 3 วินาที
        gameObject.SetActive(false);
        CoinPool.Instance.RespawnCoinWithDelay(gameObject, 3f);
    }
}
