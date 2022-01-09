using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonsterController

{
    public GameObject projectilePrefab;

    int fireEveryNAttackTime = 1;
    int countAttack = 1;

    protected override void Start()
    {
        base.maxHealth = 10000;
        base.Start();

    }
    protected override void Attack()
    {
        base.Attack();
        attackAudioSource.Play();
        if (countAttack == 1)
        {
            monsterDistance = 100;
            countAttack--;
        }
        else if (countAttack == 0)
        {
            Launch();
            countAttack = fireEveryNAttackTime;
            monsterDistance = 2;
        }
        else
        {
            Debug.Log(countAttack);
            countAttack--;
        }
    }


    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position, Quaternion.identity);

        FireballScript projectile = projectileObject.GetComponent<FireballScript>();

        Rigidbody2D projectilerigidbody2D = projectile.GetComponent<Rigidbody2D>();
        projectilerigidbody2D.SetRotation(-90.0f * direction.x);
        projectilerigidbody2D.AddForce(direction * 300);

        Destroy(projectileObject, 1.0f);
    }
}