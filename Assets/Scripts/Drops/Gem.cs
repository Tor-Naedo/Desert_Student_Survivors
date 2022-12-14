using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.AddExp();
            Destroy(gameObject);
        }
    }

    public void MagnetToPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(MoveToPlayer(player));
        }
    }

    IEnumerator MoveToPlayer(Player player)
    {
        while(player != null)
        {
            Vector3 destination = player.transform.position;
            Vector3 source = gameObject.transform.position;
            Vector3 direction = destination - source;

            direction.Normalize();
            transform.position += 2 * Time.deltaTime * direction;
            yield return null;
        }
    }
}
