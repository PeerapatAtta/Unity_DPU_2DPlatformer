using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class ScoreController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI coinScore;
    private int currentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinScore.text = "Coin Score = " + nScore;        
    }
}
