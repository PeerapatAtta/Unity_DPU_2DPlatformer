using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform obj;  // ตัวละครที่กล้องจะติดตาม
    public float minX;     // ขอบเขตซ้ายสุดของกล้อง
    public float maxX;     // ขอบเขตขวาสุดของกล้อง

    void Update()
    {
        // จำกัดตำแหน่ง x ของกล้องให้อยู่ในขอบเขตที่กำหนด
        float clampedX = Mathf.Clamp(obj.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
