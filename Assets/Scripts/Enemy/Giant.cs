using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    enum GiantState
    {
        Idle,
        Chasing,
        Attack
    }

    [SerializeField] GameObject knifePrefab;

    private Animator animator;
    GiantState giantState = GiantState.Idle;
    float waitTimer = 2f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        switch (giantState)
        {
            case GiantState.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    giantState = GiantState.Chasing;
                }
                break;
            case GiantState.Chasing:
                float distance = Vector3.Distance(transform.position, player.transform.position);
                base.Update();

                if (distance > 5f)
                {
                    animator.SetBool("IsWalking", true);
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                    giantState = GiantState.Attack;
                }
                break;
            case GiantState.Attack:
                animator.SetTrigger("Attack");
                giantState = GiantState.Idle;
                waitTimer = 3f;
                break;
        }
    }

    public void SpawnKnife(int number)
    {
        //Spawn Knife
        Instantiate(knifePrefab, transform.position, Quaternion.identity);
    }
}
