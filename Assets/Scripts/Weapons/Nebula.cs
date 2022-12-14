using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebula : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyBoss enemyBoss = collision.GetComponent<EnemyBoss>();

        if (enemy != null)
        {
            enemy.Damage(1f);
        }
        Destroy(gameObject, 1f);

        if (enemyBoss != null)
        {
            enemyBoss.Damage(1f);
        }
        Destroy(gameObject, 1f);
    }
}
