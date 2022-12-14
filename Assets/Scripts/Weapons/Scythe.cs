using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.right * 5 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyBoss enemyBoss = collision.GetComponent<EnemyBoss>();

        if (enemy != null)
        {
            enemy.Damage(1f);
            gameObject.SetActive(false);
        }

        if (enemyBoss != null)
        {
            enemyBoss.Damage(1f);
            gameObject.SetActive(false);
        }
    }
}
