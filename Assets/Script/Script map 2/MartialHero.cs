using UnityEngine;

public class MartialHero : MonsterController
{
    public GameObject crown;

    public GameObject projectilePrefab;

    bool isFireNextTime = false;

    int skillEveryNAttackTime = 1;
    int countAttack = 1;

    public float baseMonsterDistance = 2.5f;

    // change stat here
    protected override void Start()
    {

        base.maxHealth = 1000;
        base.speed = 3.0f;
        base.Start();

    }


    override protected void Attack()
    {
        Debug.Log(countAttack);
        // attackAudioSource.Play();

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

        BossSlashingScript projectile = projectileObject.GetComponent<BossSlashingScript>();
        projectile.transform.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        Rigidbody2D projectilerigidbody2D = projectile.GetComponent<Rigidbody2D>();

        projectilerigidbody2D.AddForce(direction * 300);
        Destroy(projectileObject, 2.0f);

    }
    protected override void Death()
    {
        base.heroKnight.ChangeLevelExp(70);
        base.Death();
        Instantiate(crown, rigidbody2D.position + new Vector2(0, 1.5f), Quaternion.identity);
    }
}
