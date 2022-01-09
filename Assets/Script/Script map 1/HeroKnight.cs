using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{
    //========================Menu========================
    public GameObject pauseMenu;


    //========================Control Hero========================
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;

    //========================Parameters animation========================
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;

    //========================Attack========================
    [Header("Attack")]
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public float attackRange = 0.5f;
    public int heroDamage = 100;
    public LayerMask monsterLayers;

    //========================Health========================
    [Header("Health and Mana")]
    public float maxHealth = 1000;
    public float health { get { return currentHealth; } }
    private float currentHealth;
    //========================Mana========================
    public int maxMana = 300;
    public int mana { get { return currentMana; } }
    private int currentMana;
    //========================Armor========================
    public int heroArmor = 20;

    //========================Healing and Recovery Mana========================
    private float healingTime = 1;
    private int healing = 5;
    private int manaRecovery = 1;

    //========================Skill of hero========================
    public float displayTime = 2.0f;
    //Skill: Block Damage
    private bool isBlock = false;
    [Header("Skill 1: Slashing Wind")]
    
    [Header("Skill 2: Shield Effective")]
    [SerializeField] ParticleSystem shieldEffect = null;
    public float skill2CountDownTime = 20.0f;
    public float skill2Effective = 7.0f;
    private float skill2Timer;
    private bool isCountdown2 = false;

    public GameObject skill2DialogBox;
    float timerSkill2Display;
    [Header("Skill 3: Ultimate")]
    [SerializeField] ParticleSystem ultimateSkill = null;
    public float skill3CountDownTime = 40.0f;
    private float skill3Timer;
    private bool isCountdown3 = false;
    public GameObject skill3DialogBox;
    float timerSkill3Display;
    // Use this for initialization
    void Start()
    {
        //Dialog Skill
        skill2DialogBox.transform.position = new Vector3(124.5f, 42, 0);
        skill2DialogBox.SetActive(false);
        timerSkill2Display = -1;
        skill3DialogBox.transform.position = new Vector3(124.5f, 42, 0);
        skill3DialogBox.SetActive(false);
        timerSkill3Display = -1;

        //Skill and particle
        shieldEffect.Stop();
        ultimateSkill.Stop();

        //Health and mana
        currentHealth = maxHealth;
        currentMana = maxMana;

        //Animation
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        //========================Skill of hero========================
        //Skill 2: Shield Effective
        skill2Timer = Mathf.Clamp(skill2Timer - Time.deltaTime, -2, skill2CountDownTime);
        if(skill3Timer <= 0){
            isCountdown3 = false;
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(isCountdown3 == true){
                DisplayDialog3();
            }else
                skill3();
        }

        skill3Timer = Mathf.Clamp(skill3Timer - Time.deltaTime, -2, skill3CountDownTime);
        if(skill2Timer <= 0){
            isCountdown2 = false;
        }
        if(Input.GetKeyDown(KeyCode.W)){
            if(isCountdown2 == true){
                DisplayDialog2();
            }else
                skill2();
        }
        
        //Skill: Block dame
        if(Input.GetKeyDown(KeyCode.Q) && !m_rolling){
                Block();
        }
        else if(Input.GetKeyUp(KeyCode.Q)){
            notBlock();
        }
        //========================Display text========================
        //Skill 2
        if (timerSkill2Display >= 0)
        {
            timerSkill2Display -= Time.deltaTime;
            if (timerSkill2Display < 0)
            {
                skill2DialogBox.SetActive(false);
                skill2DialogBox.transform.position = new Vector3(124.5f, 42, 0);
            }
        
        }
        if(skill2DialogBox.activeSelf == true){
            skill2DialogBox.transform.position += new Vector3(0, m_speed * 10 * Time.deltaTime, 0);
        }
        //SKill 3
        if (timerSkill3Display >= 0)
        {
            timerSkill3Display -= Time.deltaTime;
            if (timerSkill3Display < 0)
            {
                skill3DialogBox.SetActive(false);
                skill3DialogBox.transform.position = new Vector3(124.5f, 42, 0);
            }
        
        }
        if(skill3DialogBox.activeSelf == true){
            skill3DialogBox.transform.position += new Vector3(0, m_speed * 10 * Time.deltaTime, 0);
        }

        //========================Healing and mana recovery========================
        healingTime -= Time.deltaTime;
        if (healingTime <= 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + healing, 0, maxHealth);
            UIHealthBar.instance.SetValueHealth(currentHealth / (float)maxHealth);

            currentMana = Mathf.Clamp(currentMana + manaRecovery, 0, maxMana);
            UIHealthBar.instance.SetValueMana(currentMana / (float)maxMana);
            healingTime = 1;
        }

        //========================Handle input and movement========================
        float inputX;
        if(isBlock == false){
            inputX = Input.GetAxis("Horizontal");
        }
        else{
            inputX = 0;
        }

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Set direction of sprite, and flipX depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        //========================Handle Animations========================
        //Attack
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            //Call function attack
            Attack();

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }



        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && m_grounded == true)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        //Pause Menu
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager._instance.pause();
            pauseMenu.SetActive(true);
          
        }
        
    }

    // Animation Events


    //========================Attack and health========================
    void Attack()
    {
        if (!pauseMenu.activeSelf) //Check pause menu is active?
        {
            Collider2D[] hitHeros;
            currentMana = Mathf.Clamp(currentMana - 20, 0, maxMana);

            UIHealthBar.instance.SetValueMana(currentMana / (float)maxMana);
            //Active attackPoint depending facing direction of hero
            if (m_facingDirection == -1)
            {
                hitHeros = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, monsterLayers);
            }
            else
            {
                hitHeros = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, monsterLayers);
            }


            foreach (Collider2D obj in hitHeros)
            {
                MonsterController monsterController = obj.GetComponent<MonsterController>();
                if (monsterController.isActiveAndEnabled)
                {
                    monsterController.ChangeHealth(-heroDamage);

                }

            }
        }
        else return;
        
    }
    void OnDrawGizmosSelected()
    {
        if (attackPointLeft == null)
            return;
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);

        if (attackPointRight == null)
            return;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0 && !m_rolling)//If hero rolling, he can dodge
        {
            float temp = Mathf.Clamp(amount + heroArmor, -maxHealth, 0);
            if(isBlock == false){
                m_animator.SetTrigger("Hurt");
                //Damage is reduced by armor 
                currentHealth = Mathf.Clamp(currentHealth + temp, 0, maxHealth);
            }
            else{//Damage is reduced by Block SKill
                m_animator.SetBool("BlockAction", true);
                currentHealth = Mathf.Clamp(currentHealth + (temp*0.1f), 0, maxHealth);
                Invoke("resetBlockAction",0.5f);
            }
        }
        else
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValueHealth(currentHealth / (float)maxHealth);

        //Death
        if (currentHealth == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        m_animator.SetTrigger("Death");
        this.enabled = false;
    }

    //========================Skill of hero========================
    //Skill 1: Slashing Wind
    //Skill 2: Shield Buff Effective
    public void skill2(){
        if (!pauseMenu.activeSelf) // check pause menu is active?
        {
            shieldEffect.Play();
            skill2Timer = skill2CountDownTime;
            isCountdown2 = true;

            currentMana = Mathf.Clamp(currentMana - 80, 0, maxMana);
            UIHealthBar.instance.SetValueMana(currentMana / (float)maxMana);

            //Increased strength
            healing += 10;
            manaRecovery += 5;
            heroArmor += 10;
            heroDamage += 50;

            Invoke("returnNormalStrengh", 7);//Call returnNormalStrengh after 7s to let the hero's power return to its original state 

        }
        else return;
    }
    private void returnNormalStrengh()
    {
        shieldEffect.Stop();
        //Normail strength
        healing -= 10;
        manaRecovery -= 5;
        heroArmor -= 10;
        heroDamage -= 50; 
    }
    public void DisplayDialog2()
    {
        if(skill2DialogBox.activeSelf == true)
            return;
        timerSkill2Display = displayTime;
        skill2DialogBox.SetActive(true);

    }
    //Skill 3: Ultimate
    public void skill3(){
        if (!pauseMenu.activeSelf) // check pause menu is active?
        {
            ultimateSkill.Play();
            skill3Timer = skill3CountDownTime;
            isCountdown3 = true;

            currentMana = Mathf.Clamp(currentMana - 150, 0, maxMana);
            UIHealthBar.instance.SetValueMana(currentMana / (float)maxMana);
        }
        else return;
    }
    public void DisplayDialog3()
    {
        if(skill3DialogBox.activeSelf == true)
            return;
        timerSkill3Display = displayTime;
        skill3DialogBox.SetActive(true);

    }
    //Skill: Block
    private void Block(){
        isBlock = true;
        if (!m_rolling)
        {
            m_animator.SetBool("Block", true);
        }
        else
            return;
    }

    private void notBlock(){
        isBlock = false;
        m_animator.SetBool("Block", false);

    }

    private void resetBlockAction(){
        m_animator.SetBool("BlockAction",false);
    }
    
    public void DisplayDialogBlock()
    {
        if(skill2DialogBox.activeSelf == true)
            return;
        timerSkill2Display = displayTime;
        skill2DialogBox.SetActive(true);

    }
    
}
