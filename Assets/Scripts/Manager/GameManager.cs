using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    public static float startTime;

    [SerializeField] GameObject Deceased;
    [SerializeField] GameObject Snake;
    [SerializeField] GameObject Mummy;
    [SerializeField] GameObject Skeletons;
    [SerializeField] GameObject Demon;
    [SerializeField] GameObject Giant;

    [SerializeField] GameObject player;

    [SerializeField] float spawnDistance = 20f;

    private void Update()
    {
        float time = Time.time - startTime;
        string minutes = ((int)time / 60).ToString();
        string seconds = (time % 60).ToString("f0");

        timerText.text = minutes + ":" + seconds;
    }

    private void Start()
    {
        startTime = Time.time;
        StartCoroutine(SpawnEnemiesCoroutine());
        StartCoroutine(SpawnDeceasedCoroutine());
        StartCoroutine(SpawnBossCoroutine());
    }
    IEnumerator SpawnEnemiesCoroutine()
    {
        Spawn(Snake, 2);
        Spawn(Deceased, 2);
        yield return new WaitForSeconds(5f);
        Spawn(Skeletons, 10, false);
        yield return new WaitForSeconds(5f);
        Spawn(Mummy, 2);
        yield return new WaitForSeconds(5f);
        Spawn(Snake, 2);
        Spawn(Deceased, 2);
        yield return new WaitForSeconds(5f);
        Spawn(Snake, 2);

        Spawn(Deceased, 2);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            Spawn(Snake, 5);
            Spawn(Deceased, 5);
            Spawn(Mummy, 5);
            yield return new WaitForSeconds(5f);
            Spawn(Skeletons, 10, false);
        }
    }

    //2:00
    IEnumerator SpawnDeceasedCoroutine()
    {
        yield return new WaitForSeconds(120f);
        Spawn(Giant, 2);
        Spawn(Deceased, 10);
        Spawn(Deceased, 10);
    }

    //3:30 
    IEnumerator SpawnMummyCoroutine()
    {
        yield return new WaitForSeconds(210f);
        Spawn(Giant, 2);
        Spawn(Mummy, 10);
        Spawn(Mummy, 10);
    }

    //5:00
    IEnumerator SpawnBossCoroutine()
    {
        yield return new WaitForSeconds(300f);
        BossSpawn(Demon, 1);
    }

    private void Spawn(GameObject enemyPrefab, int numberOfEnemies, bool isChasing = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 8;
            spawnPosition += player.transform.position;
            GameObject go = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Enemy enemy = go.GetComponent<Enemy>();
            enemy.isChasing = isChasing;
        }
    }

    private void BossSpawn(GameObject enemyPrefab, int numberOfEnemies, bool isChasing = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 8;
            spawnPosition += player.transform.position;
            GameObject go = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        }
    }
}

