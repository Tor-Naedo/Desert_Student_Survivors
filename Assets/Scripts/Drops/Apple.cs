using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    //[SerializeField] float healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        {
            if (player != null)
            {
                player.Heal(5);
                Destroy(gameObject);
            }
        }
    }
}

