using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour
{
    public float dirX, moveSpeed = 4f;
    bool moveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ถ้าตำแหน่ง x ของวัตถุมากกว่า 8 ให้เปลี่ยนทิศทางไปทางซ้าย
        if(transform.position.x > 8f)
        {
            moveRight = false;
        }
        if(transform.position.x < -8f)
        {
            moveRight = true;
        }

        if(moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
        
    }
}
