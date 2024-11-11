using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStart : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab ของเหรียญ
    public List<Vector3> spawnPositions; // ตำแหน่งต่าง ๆ ที่ต้องการให้เหรียญปรากฏ

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(coinPrefab, position, Quaternion.identity);
        }
    }
}