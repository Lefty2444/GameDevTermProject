using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public float waveTime = 10;
    public int value = 1;

    public GameObject normal;
    public GameObject goblin;
    public GameObject giant;

    private void Start()
    {
        InvokeRepeating("SpawnWave", 1, waveTime);
    }

    void SpawnWave()
    {
        int currentValue = value;
        while (currentValue >= 10)
        {
            SpawnEnemy(giant);
            currentValue -= 10;
        }
        while (currentValue >= 4)
        {
            SpawnEnemy(goblin);
            currentValue -= 4;
        }
        while (currentValue > 0)
        {
            SpawnEnemy(normal);
            currentValue--;
        }
        value += 2;
    }

    void SpawnEnemy(GameObject enemy)
    {
        for (int i = 0; i < 50; i++)
        {
            int randomChildIdx = Random.Range(0, transform.childCount);
            Transform randomChild = transform.GetChild(randomChildIdx);
            if ((randomChild.transform.position - Movement.player.transform.position).sqrMagnitude > 10)
            {
                Instantiate(enemy, randomChild.position, randomChild.rotation);
                return;
            }
        }
        
    }
}

