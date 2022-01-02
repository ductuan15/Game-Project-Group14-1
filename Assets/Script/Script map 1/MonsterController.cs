using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator m_animator;
    

    //Direction
    private int m_facingDirection = -1;

    //Attack
    private float m_timeSinceAttack = 0;
    public int monsterDamage;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask heroLayers;
    private HeroKnight hero;

    //Health
    public int maxHealth = 1000;
    public int health { get { return currentHealth; } }
    private int currentHealth;

    //public float timeInvincible = 1.0f;
    //private bool isInvincible = false;
    //private float invincibleTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //m_timeSinceAttack += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.L)){
            Attack();
            m_timeSinceAttack = 0;
        }

    }

    void Attack()
    {
        m_animator.SetTrigger("Attack1");

        Collider2D[] hitHeros = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, heroLayers);

        foreach(Collider2D obj in hitHeros){
            obj.GetComponent<HeroKnight>().ChangeHealth(-monsterDamage);
            //obj.GetComponent<HeroKnight>().ChangeHealth(monsterDamage);
        }
        //if (m_facingDirection == -1)
        //    m_animation.SetTrigger("Attack1");
        //else
        //    m_animation.SetTrigger("Attack1Left");
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void ChangeHealth(int amount)
    {
        m_animator.SetTrigger("TakeHit");

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("CurrentHealth: " + currentHealth);

        //Death
        if (currentHealth == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        m_animator.SetTrigger("Death");

    }
}
