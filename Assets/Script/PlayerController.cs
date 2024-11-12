using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private float sx;
    private Animator am;
    private Rigidbody2D rb;
    private bool isGrounded;



    void Start()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sx = transform.localScale.x;
    }

    void Update()
    {
        // การเคลื่อนไหวซ้ายขวาด้วยปุ่ม A และ D
        float x = 0;
        if (Input.GetKey(KeyCode.A))
        {
            x = -1; // ซ้าย
        }
        else if (Input.GetKey(KeyCode.D))
        {
            x = 1; // ขวา
        }

        // ตั้งค่าความเร็วให้ตัวละคร
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        // อัปเดต Animation
        am.SetFloat("speed", Mathf.Abs(x));

        // กระโดดด้วย Space Bar เมื่ออยู่บนพื้น
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            am.SetBool("jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // ตั้งค่าว่าไม่อยู่บนพื้นเพื่อป้องกันการกระโดดซ้ำ
        }

        // ปรับทิศทางของตัวละครตามการเคลื่อนไหว
        if (x > 0)
        {
            transform.localScale = new Vector3(sx, transform.localScale.y, transform.localScale.z);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-sx, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            am.SetBool("jump", false);
            // Debug.Log("Player touched the ground");
        }
    }

}
