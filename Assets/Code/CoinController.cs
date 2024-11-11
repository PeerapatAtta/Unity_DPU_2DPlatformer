using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // เพิ่มการอ้างอิงถึง TextMeshPro

public class CoinController : MonoBehaviour
{
    void Start()
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
       Text txt;
        txt = GameObject.Find("/Canvas/Text").GetComponent<Text>();
        GameManager.nScore++;
        txt.text = "Score = " + GameManager.nScore;

        //add sound
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        Destroy(gameObject, 0.5f); // ทำลายเหรียญหลังจากชนกับ Player 0.5 วินาที
    }


    void Update()
    {

    }
}
