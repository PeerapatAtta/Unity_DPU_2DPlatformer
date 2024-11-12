using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public float currentTime = 0f;   // เวลาปัจจุบัน
    public float maxTime = 60f;    // เวลาสูงสุด
    public TMPro.TextMeshProUGUI textUITimer;  // ข้อความแสดงเวลา

    public string sceneName;    // ชื่อฉากที่จะโหลด

    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;

    }

    // Update is called once per frame
    void Update()
    {
        textUITimer.text = "Time: " + currentTime.ToString("0") + " Seconds";

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;

            // ทำอะไรสักอย่างเมื่อเวลาหมด
            SceneManager.LoadScene(sceneName);
        }

    }
}
