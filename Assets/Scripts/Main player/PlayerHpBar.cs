using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject foreground;

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0.75f, 0);

        float hpRatio = (float)player.PlayerHp / player.PlayerMaxHp;
        foreground.transform.localScale = new Vector3(hpRatio, 1, 1);
    }
}
