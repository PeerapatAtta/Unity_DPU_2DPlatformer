using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using TMPro; // เพิ่มการอ้างอิงสำหรับ TextMeshProUGUI

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI coinScore;
    public TextMeshProUGUI HP;
    public int nScore = 0;

    // อ้างอิงถึง PlayerHPController
    private PlayerHPController playerHPController;


    // Start is called before the first frame update
    void Start()
    {
        // ค้นหา PlayerHPController ใน Scene
        playerHPController = FindObjectOfType<PlayerHPController>();

    }

    // Update is called once per frame
    void Update()
    {
        coinScore.text = "Coin Score = " + nScore;
        
        // อัปเดต HP ตามค่า currentHP ใน PlayerHPController
        if (playerHPController != null)
        {
            HP.text = "HP = " + playerHPController.currentHP;
        }
    }
}
