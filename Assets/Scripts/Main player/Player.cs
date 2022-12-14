using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    //SPEED AND WEAPON
    [SerializeField] private float speed = 1f;
    [SerializeField] BaseWeapon[] weapons;

    //TEXT
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text coinText;

    [SerializeField] TMP_Text levelStatText;
    [SerializeField] TMP_Text coincounterText;
    [SerializeField] TMP_Text diamondText;
    [Header("")]
    [SerializeField] TMP_Text enemiesKilledText;
    [SerializeField] TMP_Text deceasedKilledText;
    [SerializeField] TMP_Text mummiesKilledText;
    [SerializeField] TMP_Text snakesKilledText;
    [SerializeField] TMP_Text skeletonsKilledText;

    [SerializeField] GameObject LevelUpScreen;
    [SerializeField] GameObject GameOverScreen;

    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isInvulnerable;


    //HP
    internal float PlayerHp;
    [SerializeField] internal float PlayerMaxHp = 10f;

    //AUDIO
    [SerializeField] private AudioSource levelupSoundEffect;

    //PARTICLES 
    public ParticleSystem speedtrail;

    public CameraShake cameraShake;

    Material material;


    private void Start()
    {
        weapons[0].LevelUp();

        PlayerHp = PlayerMaxHp;

        animator = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
    }
    public void CreateTrail()
    {
        speedtrail.Play();
    }

    public void LevelUpKatana()
    {
        weapons[0].LevelUp();
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
    }
    public void LevelUpBackKatana()
    {
        weapons[1].LevelUp();
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
    }
    public void LevelUpScythe()
    {
        weapons[2].LevelUp();
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
    }
    public void LevelUpNebula()
    {
        weapons[3].LevelUp();
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
    }

    public void LevelUpHealth()
    {
        IncreaseHp();
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
    }

    public void LevelUpSpeed()
    {
        CreateTrail();
        IncreaseSpeed();
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
    }

    public void OnDamage()
    {
        StartCoroutine(cameraShake.Shake(0.2f, 0.3f));
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            StartCoroutine(DamageCoroutine());
            if (--PlayerHp <= 0f)
            {
                Time.timeScale = 0f;
                GameOverScreen.SetActive(true);
            }
        }
    }

    IEnumerator DamageCoroutine()
    {
        material.SetFloat("_Speedster", -0.5f);
        yield return new WaitForSeconds(0.5f);
        material.SetFloat("_Speedster", 0f);
    }

    public void IncreaseHp()
    {
        PlayerMaxHp++;
    }

    public void IncreaseSpeed()
    {
        speed++;
        material.SetFloat("_Speedster", 0.5f);
    }

    void Update()
    {
        levelText.text = "Level: " + currentLevel;
        coinText.text = "Coins: " + coins;

        //STATS
        levelStatText.text = "Level: " + currentLevel;
        coincounterText.text = "Coins: " + coins;
        diamondText.text = "Diamonds: " + diamond;

        enemiesKilledText.text = "Enemies Killed: " + EnemiesKilled;
        deceasedKilledText.text = "Deceased Killed: " + DeceasedKilled;
        mummiesKilledText.text = "Mummies Killed: " + MummiesKilled;
        snakesKilledText.text = "Snakes Killed: " + SnakesKilled;
        skeletonsKilledText.text = "Skeletons Killed: " + SkeletonsKilled;

        isInvulnerable = false;
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        transform.position += new Vector3(inputX, inputY) * speed * Time.deltaTime;

        if (inputX != 0)
        {
            transform.localScale = new Vector3(inputX > 0 ? -1 : 1, 1, 1);
        }

        animator.SetBool("IsRunning", inputX != 0 || inputY != 0);
    }

    internal int currentExp;
    internal int expToLevel = 5;
    internal int currentLevel;

    internal Action<int, int> OnExpGained;

    internal void AddExp()
    {
        currentExp++;

        if (currentExp >= expToLevel)
        {
            currentExp = 0;
            expToLevel += 5;
            currentLevel++;
            levelupSoundEffect.Play();

            int maxWeapons = weapons.Length;
            int randomWeaponIndex = UnityEngine.Random.Range(0, maxWeapons);
            weapons[randomWeaponIndex].LevelUp();

            Time.timeScale = 0;
            LevelUpScreen.SetActive(true);
        }

        OnExpGained(currentExp, expToLevel);
    }
    public void Heal(float amount)
    {
        if (PlayerHp <= 0f)
        {
            return;
        }

        PlayerHp += amount;

        if (PlayerHp > PlayerMaxHp)
        {
            PlayerHp = PlayerMaxHp;
        }
    }

    public float GetHPRatio()
    {
        return (float)PlayerHp / (float)PlayerMaxHp;
    }

    public int coins;
    public int diamond;
    public static int DeceasedKilled;
    public static int MummiesKilled;
    public static int SnakesKilled;
    public static int SkeletonsKilled;
    public static int EnemiesKilled;

    public void AddCoins()
    {
        coins++;
    }

    public void AddDiamonds()
    {
        diamond++;
    }

    public static void AddDeceasedKilledCount()
    {
        DeceasedKilled++;
    }
    public static void AddMummiesKilledCount()
    {
        MummiesKilled++;
    }

    public static void AddSnakesKilledCount()
    {
        SnakesKilled++;
    }

    public static void AddSkeletonsKilledCount()
    {
        SkeletonsKilled++;
    }
    public static void AddEnemiesKilledCount()
    {
        EnemiesKilled++;
    }
}

