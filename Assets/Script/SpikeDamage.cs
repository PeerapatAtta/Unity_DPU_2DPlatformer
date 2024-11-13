using System.Collections;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damageAmount = 1;         // ความเสียหายที่ทำต่อครั้ง
    public float damageInterval = 5f;    // ช่วงเวลาระหว่างความเสียหาย (เป็นอมตะ 5 วินาที)

    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Object ที่ชนคือ Player และมี Tag เป็น "Player"
        if (other.CompareTag("Player"))
        {
            PlayerHPController healthSystem = other.GetComponent<PlayerHPController>();
            if (healthSystem != null)
            {
                // เริ่ม Coroutine สำหรับทำความเสียหาย
                damageCoroutine = StartCoroutine(ApplyDamageOverTime(healthSystem));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // หยุดการทำความเสียหายเมื่อออกจากหนาม
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator ApplyDamageOverTime(PlayerHPController healthSystem)
    {
        while (true)
        {
            // ตรวจสอบว่าตัวละครสามารถรับความเสียหายได้ (ไม่อยู่ในสถานะอมตะ)
            if (healthSystem.CanTakeDamage()&&healthSystem.currentHP>0)
            {
                healthSystem.TakeDamage(damageAmount); // ลด HP ครั้งแรก

                // เริ่มช่วงเวลาอมตะ (เป็นเวลา 5 วินาที)
                healthSystem.StartInvulnerability(damageInterval);
            }

            // รอเป็นเวลา 5 วินาทีก่อนทำความเสียหายครั้งถัดไป
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
