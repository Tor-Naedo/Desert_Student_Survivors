using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : BaseWeapon
{
    [SerializeField] GameObject firecirclePrefab;
    

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);

                for (int i = 0; i < level; i++)
            {
                Instantiate(firecirclePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}

