using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonsterController
{
    public GameObject crown;
    public GameObject projectilePrefab;

    int fireEveryNAttackTime = 1;
    int countAttack = 1;

    protected override void Start()
    {

        base.maxHealth = 10000;
        base.monsterDistance = 2.7f;
        base.Start();

    }
    protected override void Attack()
    {
        base.Attack();
        // attackAudioSource.Play();
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

        Destroy(projectileObject, 2.0f);
    }

    protected override void Death()
    {
        m_animator.SetBool("Death", true);
        //Disable object
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GetComponent<Rigidbody2D>().simulated = false;
        Instantiate(crown, rigidbody2D.position, Quaternion.identity);
        this.enabled = false;
    }
}