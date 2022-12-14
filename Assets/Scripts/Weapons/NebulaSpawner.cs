using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaSpawner : BaseWeapon
{
    [SerializeField] GameObject nebulaPrefab;

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
                Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 3;
                Instantiate(nebulaPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
