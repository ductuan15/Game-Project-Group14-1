using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardScript : MonsterController

{

    public GameObject projectilePrefab;
    bool isFireNextTime = false;

    int fireEveryNAttackTime = 1;
    int countAttack = 1;
    protected override void Attack()
    {
        base.Attack();
        if (countAttack == 1)
        {
            monsterDistance = 100;
            countAttack--;
        }
        else if(countAttack == 0){
            Launch();
            countAttack =  fireEveryNAttackTime;
            monsterDistance = 2;
        }else{
            Debug.Log(countAttack);
            countAttack--;

        }
    }


    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position, Quaternion.identity);

        FireballScipt projectile = projectileObject.GetComponent<FireballScipt>();

        Rigidbody2D projectilerigidbody2D = projectile.GetComponent<Rigidbody2D>();
        projectilerigidbody2D.SetRotation(-90.0f * direction.x);
        projectilerigidbody2D.AddForce(direction * 300);

        Destroy(projectile, 3.0f);
    }

}
