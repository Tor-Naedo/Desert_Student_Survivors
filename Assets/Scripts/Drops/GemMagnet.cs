using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMagnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            GameObject[] gems = GameObject.FindGameObjectsWithTag(tag: "Gem"); ;
            if(gems.Length > 0)
            {
                foreach(GameObject gem in gems)
                {
                    Gem gem1 = gem.GetComponent<Gem>();
                    gem1.MagnetToPlayer();
                }
            }
            gems = null;
            Destroy(gameObject);
        }
        
    }
}
