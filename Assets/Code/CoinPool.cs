using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;
    public GameObject coinPrefab;
    public int poolSize = 10;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public GameObject respawnEffectPrefab; // Prefab ของ Particle Effect

    private Queue<GameObject> coinPool;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        coinPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }

        SpawnCoinsAtStart();
    }

    private void SpawnCoinsAtStart()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = GetCoin();
            coin.transform.position = GetRandomPosition();
        }
    }

    public GameObject GetCoin()
    {
        if (coinPool.Count > 0)
        {
            GameObject coin = coinPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(true);
            return coin;
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Enqueue(coin);
    }

    public void RespawnCoinWithDelay(GameObject coin, float delay)
    {
        StartCoroutine(RespawnCoinCoroutine(coin, delay));
    }

    private IEnumerator RespawnCoinCoroutine(GameObject coin, float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnCoin(coin);
        Debug.Log("Playing Particle Effect at: " + coin.transform.position); // ตรวจสอบตำแหน่งและการเรียกใช้งาน
        PlayRespawnEffect(coin.transform.position); // แสดง Particle Effect หลังจากที่เหรียญ Respawn
    }

    public void RespawnCoin(GameObject coin)
    {
        coin.transform.position = GetRandomPosition();
        coin.SetActive(true);
        // PlayRespawnEffect(coin.transform.position); // เรียกฟังก์ชันแสดงผล Particle Effect
    }

    private void PlayRespawnEffect(Vector3 position)
    {
        if (respawnEffectPrefab != null)
        {
            Debug.Log("Instantiating Particle Effect"); // ตรวจสอบว่าฟังก์ชันนี้ถูกเรียกใช้งานหรือไม่
            // Debug.Log("Playing respawn effect at: " + position); // เพิ่ม Debug เพื่อตรวจสอบตำแหน่ง
            GameObject effect = Instantiate(respawnEffectPrefab, position, Quaternion.identity); // สร้าง Particle Effect ที่ตำแหน่งที่กำหนด
            Destroy(effect, 1f); // ทำลาย Particle Effect หลังจาก 1 วินาที
        }
        else
        {
            Debug.LogError("Respawn effect prefab is not assigned.");
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector3(randomX, randomY, 0);
    }
}
