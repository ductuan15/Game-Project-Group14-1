using UnityEngine;

public class MartialHero : MonsterController
{
    // Start is called before the first frame update
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
        base.speed = 30;
        base.Start();
        
    }


    override protected void Attack()

    {
        Debug.Log(maxHealth);
        base.Attack();
        if (countAttack == 0)
        {
            base.speed = skillSpeed;
            countAttack--;
        }
        else if (countAttack < 0)
        {
            countAttack = skillEveryNAttackTime;
            base.speed = baseSpeed;
        }
        else
        {
            countAttack--;
        }

    }
}
