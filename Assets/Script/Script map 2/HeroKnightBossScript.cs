using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// skill
// colddown
// increase monster distance 
// dash into hero
// ATTACK!!
public class HeroKnightBossScript : MonsterController
{

    public GameObject crown;

    // Skill
    int skillEveryNAttackTime = 4;
    int countAttack;

    public float baseMonsterDistance = 1.5f;
    public float baseSpeed = 2.5f;

    protected override void Start()
    {
        base.maxHealth = 1000;
        countAttack = skillEveryNAttackTime;
        base.monsterDistance = baseMonsterDistance;
        base.speed = baseSpeed;
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        countAttack--;
        if (countAttack == 1)
        {
            base.speed = 30f;
        }
        else if (countAttack == 0)
        {
            base.speed = baseSpeed;
            countAttack = skillEveryNAttackTime;
        }
    }

    protected override void Death()
    {
        base.Death();
        Instantiate(crown, rigidbody2D.position + new Vector2(0, 1.5f), Quaternion.identity);
    }
}
