using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHPController : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    private bool isInvulnerable = false;
    public string sceneName;        // ชื่อฉากที่จะโหลดเมื่อ HP หมด
    public AudioClip damageSound;   // เสียงที่เล่นเมื่อ HP ลด
    private AudioSource audioSource;

    void Start()
    {
        currentHP = maxHP;

        // ตรวจสอบว่ามี AudioSource อยู่แล้วหรือไม่ หากไม่มีให้เพิ่ม
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvulnerable) return;

        currentHP -= damageAmount;
        Debug.Log("HP: " + currentHP);

        // เล่นเสียงความเสียหาย
        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // ปรับให้สามารถรับค่าเวลาในการตั้งสถานะอมตะ
    public void StartInvulnerability(float duration)
    {
        if (!isInvulnerable)
        {
            StartCoroutine(InvulnerabilityCoroutine(duration));
        }
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }

    void Die()
    {
        Debug.Log("Character is dead.");
        // ไปยังหน้าเกมโอเวอร์ หรือฉากที่กำหนดเมื่อ HP หมด
        SceneManager.LoadScene(sceneName);

    }

    public bool CanTakeDamage()
    {
        return !isInvulnerable;
    }
}
