using UnityEngine;

public class MartialHero : MonsterController
{

    public GameObject projectilePrefab;

    bool isFireNextTime = false;

    int skillEveryNAttackTime = 1;
    int countAttack = 1;

    float skillSpeed = 100.0f;

    public float baseSpeed = 2.0f;

    public float baseMonsterDistance = 2.5f;

    // change stat here
    protected override void Start()
    {
        base.maxHealth = 100000;
        base.speed = 5f;
        base.Start();

    }


    override protected void Attack()
    {
        Debug.Log(countAttack);
        base.Attack();
        if (countAttack == 0)
        {
            base.monsterDistance = 200;
            countAttack--;

        }
        else if (countAttack < 1)
        {

            Launch();
            base.monsterDistance = 2.0f;
            countAttack = skillEveryNAttackTime;
        }
        else
        {
            countAttack--;
        }


    }
    void Launch()
    {
        Debug.Log("Fire");
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position, Quaternion.identity);

        SliceScript projectile = projectileObject.GetComponent<SliceScript>();
        projectile.transform.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        Rigidbody2D projectilerigidbody2D = projectile.GetComponent<Rigidbody2D>();

        projectilerigidbody2D.AddForce(direction * 300);
        Destroy(projectileObject, 2.0f);


    }
}
