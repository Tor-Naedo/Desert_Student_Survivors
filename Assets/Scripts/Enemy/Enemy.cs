using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject DiamondPrefab;

    [SerializeField] GameObject ApplePrefab;
    [SerializeField][Range(0f, 1f)] float chance = 1f;

    [SerializeField] GameObject PineapplePrefab;
    [SerializeField][Range(0f, 1f)] float pinechance = 1f;

    [SerializeField] GameObject CoinPrefab;
    [SerializeField][Range(0f, 1f)] float coinchance = 1f;

    [SerializeField] private AudioSource enemyDeathSoundEffect;

    internal float EnemyHp;
    [SerializeField] internal float EnemyMaxHp = 1f;

    [SerializeField] float speed = 1f;
    public bool isChasing = true;
    protected GameObject player;

    protected virtual void Start()
    {
        EnemyHp = EnemyMaxHp;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.OnDamage();
        }
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 destination = player.transform.position;
        Vector3 source = gameObject.transform.position;
        Vector3 direction = destination - source;

        if (!isChasing)
        {
            direction = Vector3.left;
        }
        direction.Normalize();
        transform.position += direction * Time.deltaTime * speed;

        float scaleX = 1f;
        if (direction.x < 0)
        {
            scaleX = -1f;
        }

        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    internal void Damage(float damage)
    {

        if (--EnemyHp <= 0f)
        {
            Player.AddEnemiesKilledCount();
            Destroy(gameObject);

        }

        enemyDeathSoundEffect.Play();

        if (EnemyHp <= 0f)
        {
            Instantiate(DiamondPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);

            if (Random.value < chance)
            {
                Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized;
                spawnPosition += player.transform.position;

                Instantiate(ApplePrefab, spawnPosition, Quaternion.identity);
                Destroy(gameObject);
            }

            if (Random.value < pinechance)
            {
                Vector3 pineapplePosition = UnityEngine.Random.insideUnitCircle.normalized;
                pineapplePosition += player.transform.position;

                Instantiate(PineapplePrefab, pineapplePosition, Quaternion.identity);
                Destroy(gameObject);
            }

            if (Random.value < coinchance)
            {
                Vector3 coinPosition = UnityEngine.Random.insideUnitCircle.normalized;
                coinPosition += player.transform.position;

                Instantiate(CoinPrefab, coinPosition, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
