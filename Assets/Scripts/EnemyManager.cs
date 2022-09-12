using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemiesPool;
    [SerializeField] private GameObject currentEnemies;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius;
    private float enemyY = 1;
    private float difficultyMultiplier = 3;
    public void GameStart()
    {
        StartCoroutine(SpawningEnemies());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        Vector3 randomPos = player.position;
        randomPos += Random.insideUnitSphere.normalized * spawnRadius;
        randomPos = new Vector3(randomPos.x, enemyY, randomPos.z);
        SpawnFromPool(randomPos);
    }
    private void SpawnFromPool(Vector3 pos)
    {
        if (enemiesPool.transform.childCount > 0)
        {
            var enemy = enemiesPool.transform.GetChild(0);
            enemy.transform.parent = currentEnemies.transform;
            enemy.transform.position = pos;
            enemy.GetComponent<Enemy>().SetEnemy(player,this,gameManager);
            enemy.transform.LookAt(player);
            enemy.gameObject.SetActive(true);
        }
    }
    public void BackToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.parent = enemiesPool.transform;
        enemy.transform.position = enemiesPool.transform.position;
    }
    public void KillPointsAward(int points)
    {
        gameManager.AddPoints(points);
    }
    public IEnumerator SpawningEnemies()
    {
        while (gameManager.GameOn)
        {
            for (int i = 0; i < difficultyMultiplier; i++)
            {
                SpawnEnemies();
            }
            yield return new WaitForSeconds(5); 
        }
    }
}
