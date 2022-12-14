using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Katana : BaseWeapon
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(KatanaCoroutine());
    }

    //Katana kill
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyBoss enemyBoss = collision.GetComponent<EnemyBoss>();

        if (enemy != null)
        {
            enemy.Damage(level);
        }

        if (enemyBoss != null)
        {
            enemyBoss.Damage(level);
        }
    }

    IEnumerator KatanaCoroutine()
    {
        while (true)
        {
            transform.localScale = Vector3.one * level;
            //enable katana
            spriteRenderer.enabled = true;
            boxCollider2D.enabled = true;

            //wait 0.5
            yield return new WaitForSeconds(0.5f);

            //disable katana
            spriteRenderer.enabled = false; 
            boxCollider2D.enabled = false; 

            //wait 2 secs
            yield return new WaitForSeconds(1f);

        }
    }
}
