using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pineapple : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        {
            if (player != null)
            {
                player.Heal(2.5f);
                Destroy(gameObject);
            }
        }
    }
}