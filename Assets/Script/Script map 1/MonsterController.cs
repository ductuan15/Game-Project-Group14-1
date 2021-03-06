using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //Audio
    AudioSource audioSource;
    public AudioClip attackAudioClip;
    public AudioClip walkingAudioClip;
    public AudioClip deathAudioClip;
    //Animation
    public Animator m_animator;
    protected Vector2 direction;
    public float positionY = 1;

    //Attack
    protected float timeSinceAttack = 3.0f;
    protected float timerAttack = 0;
    public int monsterDamage = 100;
    public float monsterDistance = 0;

    //Attack Point
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public float attackRange = 0.5f;
    public float speed = 1.5f;

    //Check visible
    protected new Rigidbody2D rigidbody2D;
    protected new Renderer renderer;

    //Attack
    public LayerMask heroLayers;
    public GameObject hero;
    public float delayTime = 0.4f;

    public ParticleSystem attackEffect;

    //Health
    public float maxHealth = 1000;
    public float health { get { return currentHealth; } }
    private float currentHealth;
    private bool isCanMove = true;
    //Level up for hero
    protected HeroKnight heroKnight;

    protected virtual void Start()
    {
        currentHealth = this.maxHealth;
        renderer = GetComponent<Renderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        heroKnight = GameObject.Find("HeroKnight").GetComponent<HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        timerAttack = Mathf.Clamp(timerAttack - Time.deltaTime, -2, timeSinceAttack);
        Vector2 heroPosition = hero.transform.position;
        Vector2 position = transform.position;
        direction = (heroPosition - position).normalized;
        // flip picture depending on direction of monster
        if (direction.x <= 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
        // Check monster in camera
        if (renderer.isVisible)
        {
            if (Vector2.Distance(heroPosition, position) >= monsterDistance && isCanMove)
            {
                m_animator.SetBool("Run", true);
                position.x += speed * Time.deltaTime * direction.x;
                transform.position = position;
            }
            else
            {
                m_animator.SetBool("Run", false);
                if (timerAttack <= 0)
                {
                    m_animator.SetTrigger("Attack1");
                    setCanNotMove();

                    direction = Vector2.zero;
                    timerAttack = timeSinceAttack;
                }

            }
        }
        else
        {
            m_animator.SetBool("Run", false);
        }

    }

    //delay attack
    protected virtual void Attack()
    {
        audioSource.PlayOneShot(attackAudioClip);
        Collider2D[] hitHeros;

        if (direction.x > 0)
        {
            hitHeros = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, heroLayers);
        }
        else
        {
            hitHeros = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, heroLayers);
        }



        foreach (Collider2D obj in hitHeros)
        {
            obj.GetComponent<HeroKnight>().ChangeHealth(-monsterDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;
        if (attackPointLeft == null)
            return;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }

    public void ChangeHealth(float amount)
    {
        m_animator.SetTrigger("TakeHit");
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        //UIHealthBar
        transform.GetChild(2).GetComponent<MonsterHealthBar>().setSize(currentHealth / (float)maxHealth);

        //Death
        if (currentHealth == 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        m_animator.SetBool("Death", true);
        heroKnight.ChangeLevelExp(30);
        //Disable object
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GetComponent<Rigidbody2D>().simulated = false;
        this.enabled = false;
    }
    void setCanNotMove()
    {
        attackEffect.Play();
        isCanMove = false;
    }
    void setCanMove()
    {
        isCanMove = true;
    }
    void PlayWalkSound()
    {
        audioSource.PlayOneShot(walkingAudioClip);
    }

    void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathAudioClip);

    }
    private void OnParticleCollision(GameObject other)
    {
        ChangeHealth(-heroKnight.heroDamage*4);
    }
}