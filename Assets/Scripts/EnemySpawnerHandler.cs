using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerHandler : MonoBehaviour
{
    
    [SerializeField] GameObject Enemy;
    public float spawnTime = 1f;
    public static int enemyCounter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0 && enemyCounter<=3)
        {
            GameObject enemy = Instantiate(Enemy);
            enemy.transform.position = transform.position; 
            spawnTime = 5f;
            enemyCounter++;
            
        }
        
    }
}
