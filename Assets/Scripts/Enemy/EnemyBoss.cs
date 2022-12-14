using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] GameObject DiamondPrefab;

    [SerializeField] GameObject ApplePrefab;
    [SerializeField][Range(0f, 1f)] float chance = 1f;

    [SerializeField] GameObject PineapplePrefab;
    [SerializeField][Range(0f, 1f)] float pinechance = 1f;

    [SerializeField] GameObject CoinPrefab;
    [SerializeField][Range(0f, 1f)] float coinchance = 1f;

    [SerializeField] private AudioSource enemyDamageSoundEffect;

    internal float BossHp;
    [SerializeField] internal float BossMaxHp = 300f;

    [SerializeField] float speed = 1f;
    public bool isChasing = true;
    GameObject player;
    Material material;

    private void Start()
    {
        BossHp = BossMaxHp;

        player = GameObject.FindGameObjectWithTag("Player");

        material = GetComponent<SpriteRenderer>().material;

        StartCoroutine(BossCameraCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.OnDamage();
            StartCoroutine(FlashCoroutine());
        }
    }
    IEnumerator FlashCoroutine()
    {
        material.SetFloat("_Flash", 0.5f);
        yield return new WaitForSeconds(1f);
        material.SetFloat("_Flash", 0f);
    }

    IEnumerator BossCameraCoroutine()
    {
        Time.timeScale = 0;

        Camera.main.GetComponent<PlayerCamera>().target = gameObject;

        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 4;
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 3;
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 4;
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 5;
        yield return new WaitForSecondsRealtime(1f);

        Camera.main.GetComponent<PlayerCamera>().target = player;

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1;

    }

    private void Update()
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

        if(BossHp <= 150f)
        {
            speed = 2f;
        }
    }
    public float BossHPRatio()
    {
        return (float)BossHp / (float)BossMaxHp;
    }

    internal void Damage(float damage)
    {

        if(BossHp <= 100)
        {
            material.SetFloat("_Flash", -0.69f);
        }

        if (--BossHp <= 0f)
        {
            Destroy(gameObject);

        }

        enemyDamageSoundEffect.Play();

        if (BossHp <= 0f)
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
