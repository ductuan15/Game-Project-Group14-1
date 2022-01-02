using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator m_animator;

    //Direction
    private Vector2 direction;

    //Attack
    private float timeSinceAttack = 3.0f;
    private float timerAttack;

    public int monsterDamage;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float speed = .0f;

    new Rigidbody2D rigidbody2D;

    new Renderer renderer;


    public LayerMask heroLayers;
    public GameObject hero;
    public float delayTime = 0.4f;

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
        renderer = GetComponent<Renderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        timerAttack = timeSinceAttack;
    }

    // Update is called once per frame
    void Update()
    {
        timerAttack = Mathf.Clamp(timerAttack - Time.deltaTime, -2, timeSinceAttack);
        Vector2 heroPosition = hero.transform.position;
        Vector2 position = transform.position;
        direction = (heroPosition - position).normalized;
        // Check monster in camera
        if (renderer.isVisible)
        {
            if (Vector2.Distance(heroPosition, position) <= 2.2f)
            {
                m_animator.SetBool("Run", false);
                if (timerAttack <= 0)
                {
                    Attack();
                    direction = Vector2.zero;
                    timerAttack = timeSinceAttack;
                }
            }
            else
            {
                m_animator.SetBool("Run", true);
                position.x += speed * Time.deltaTime * direction.x;
                transform.position = position;
                if (direction.x <= 0)
                // flip picture depending on direction of monster
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
        else
        {
            m_animator.SetBool("Run", false);

        }

    }

    //delay attack
    IEnumerator ExampleCoroutine(Collider2D obj)
    {
        yield return new WaitForSeconds(delayTime);
        
    }
    void Attack()
    {
        m_animator.SetTrigger("Attack1");


        Collider2D[] hitHeros = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, heroLayers);


        foreach (Collider2D obj in hitHeros)
        {
            StartCoroutine(ExampleCoroutine(obj));
            //obj.GetComponent<HeroKnight>().ChangeHealth(monsterDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
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
        m_animator.SetBool("Death", true);
        this.enabled = false;
    }
}
