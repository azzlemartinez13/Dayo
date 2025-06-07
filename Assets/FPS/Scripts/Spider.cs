using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum MaterialType { Wood, Metal, Barrel, Skin, Stone, Wall }

[RequireComponent(typeof(NavMeshAgent))]
public class Spider : MonoBehaviour
{

    public int Level;
    public Base Base;
    private EnemySpawner spawner;

    private bool isInitialized = false;



    [Header("Spider Settings")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] public float maxHP;
    public float currentHP;
    [SerializeField] private float stoppingDistance = 0.5f;
    [SerializeField] private float rotateSpeed = 5f;


    [Header("Attack Settings")]
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int attackDamage = 10;

    private float attackTimer = 0f;
    private Base baseTarget;


    public MaterialType materialType;

    [Header("Effects & Sounds")]
    public GameObject smallExplosionEffect;
    public AudioSource woodHitSound;
    public AudioSource metalHitSound;
    public AudioSource characterHitSound;
    public AudioSource destructionSound;

    public HealthComponent healthComponent;

    //reference to health your currently targeting
    private HealthComponent targetHealth;

    //public NavMeshAgent Agent;
    private Transform player;
    private bool playerInRange = false;

    Animator animator;


    public event Action<Spider> OnDeath;

    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        //currentHealth = maxHealth;
        currentHP = maxHP;

        //Agent = GetComponent<NavMeshAgent>();
        //Agent.speed = moveSpeed;
        //Agent.stoppingDistance = stoppingDistance;
        //Agent.updatePosition = true;
        //Agent.updateRotation = true;
        //Agent.angularSpeed = 120f;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No object tagged 'Player' found.");
        }

        //Agent.SetDestination(player.position);
        animator = GetComponent<Animator>();

        //if (animator == null)
        //    animator = GetComponent<Animator>();
        //animator.SetBool("isWalking", true);
    }

    public void InitializeSpider(EnemySpawner spawner, int level, Base baseTarget, HealthComponent[] hpComponents)
    {

        this.Level = level;
        this.Base = baseTarget;
        this.baseTarget = baseTarget;  // Set target reference

        this.spawner = spawner;
        //Agent = GetComponent<NavMeshAgent>();
        //Agent.enabled = true;
        //Agent.isStopped = false;
        //Agent.ResetPath();
        //Agent.SetDestination(player.position);

        currentHP = maxHP;

        isInitialized = true;

        playerInRange = false;

        //if (Agent != null)
        //{
        //    Agent.enabled = true;
        //    Agent.ResetPath();
        //}

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject wallObj = GameObject.FindGameObjectWithTag("Wall");


        if (playerObj != null)
        {
            player = playerObj.transform;
            //Agent.SetDestination(player.position);
            Debug.Log("Spider initialized and targeting player.");
        }
        else if (baseTarget != null)
        {
            player = baseTarget.transform; // fallback to base
            //Agent.SetDestination(baseTarget.transform.position);
            Debug.LogWarning("No 'Player' found. Targeting base instead.");

        }

        else if (wallObj != null)
        {
            player = baseTarget.transform; // fallback to base
            //Agent.SetDestination(baseTarget.transform.position);
            Debug.LogWarning("No 'Player' found. Targeting wall instead.");

        }
        else
        {
            Debug.LogError("No Player or Base target available for spider!");
        }

    }


    //public void InitializeSpider(int level, Base baseTarget, HealthComponent[] hpComponents)
    //{
    //    this.Level = level;
    //    this.Base = baseTarget;

    //    // Reset HP
    //    currentHP = maxHP;

    //    // Optionally, reset NavMeshAgent and internal state
    //    playerInRange = false;
    //    if (Agent != null)
    //    {
    //        Agent.enabled = true;
    //        Agent.ResetPath();
    //    }

    //    // Positioning is handled by spawner
    //}

    //void Update()
    //{  //single attack
    //    if (player != null && agent.enabled)
    //    {
    //        if (!playerInRange)
    //        {
    //            agent.SetDestination(player.position);
    //        }
    //        else
    //        {
    //            agent.SetDestination(transform.position);
    //        }

    //        Vector3 direction = agent.steeringTarget - transform.position;
    //        direction.y = 0;

    //        if (direction.sqrMagnitude > 0.01f)
    //        {
    //            Quaternion lookRotation = Quaternion.LookRotation(direction);
    //            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    //        }

    //        Debug.DrawLine(transform.position, agent.steeringTarget, Color.green);
    //    }

    //    if (playerInRange && baseTarget != null)
    //    {
    //        attackTimer += Time.deltaTime;
    //        if (attackTimer >= attackSpeed)
    //        {
    //            AttackBase();
    //            attackTimer = 0f;
    //        }
    //    }
    //}

    void Update()
    {  //continuous attack with timer

        //if (!Agent.pathPending && Agent.remainingDistance > Agent.stoppingDistance)
        //{
        //    if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
        //    {
        //        if (player != null)
        //            Agent.SetDestination(player.position);
        //    }
        //}
        //if (animator != null)
        //{
        //    //animator.SetFloat("Speed", Agent.velocity.magnitude);
        //}

        //if (player != null && Agent.enabled)
        //{

        //    Agent.SetDestination(player.position);

        //    Vector3 direction = Agent.steeringTarget - transform.position;
        //    direction.y = 0;

        //    if (direction.sqrMagnitude > 0.01f)
        //    {
        //        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        //    }

        //    Debug.DrawLine(transform.position, Agent.steeringTarget, Color.green);
        //}

        // Attack target if in range
        if (targetHealth != null)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackSpeed)
            {
                targetHealth.TakeDamage(attackDamage);
                Debug.Log($"Spider dealt {attackDamage} damage to target!");
                attackTimer = 0f;
            }
        }
    }


    private void AttackBase()
    {
        baseTarget.TakeDamage(attackDamage);
        Debug.Log($"Spider attacked base for {attackDamage} damage!");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Base") || other.CompareTag("Player"))
        {
            targetHealth = other.GetComponent<HealthComponent>();

            if (targetHealth == null)
            {

                Debug.LogWarning("No HealthComponent found on target.");
            }

            Debug.Log("Spider started attacking target.");
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }

        if (other.TryGetComponent<Base>(out Base b))
        {
            baseTarget = b;
        }
    }



    private void OnTriggerExit(Collider other)
    {           //stop attacking
        if (other.CompareTag("Wall") || other.CompareTag("Base") || other.CompareTag("Player"))
        {
            if (targetHealth != null && targetHealth.gameObject == other.gameObject)
            {

                targetHealth = null;
                Debug.Log("Spider stopped attacking target.");
            }
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (other.TryGetComponent<Base>(out Base b) && b == baseTarget)
            {
                baseTarget = null;
            }
        }
    }



    public void TakeDamage(int damage)
    {

        currentHP -= damage;

        if (currentHP <= 0)
        {
            DestroyObject();
        }

        // Play hit sound based on material type
        switch (materialType)
        {
            case MaterialType.Wood: woodHitSound?.Play(); break;
            case MaterialType.Metal: metalHitSound?.Play(); break;
            case MaterialType.Skin: characterHitSound?.Play(); break;
        }

    }



    private void DestroyObject()
    {
        OnDeath?.Invoke(this);

        if (destructionSound != null)
        {
            destructionSound.Play();
        }

        if (smallExplosionEffect != null)
        {
            Instantiate(smallExplosionEffect, transform.position, transform.rotation);
        }

        float delay = destructionSound != null ? destructionSound.clip.length : 0f;
        Destroy(gameObject, delay > 0 ? delay : 0f);
    }

    public void ResetSpider()
    {
        currentHP = maxHP;



        playerInRange = false;

        //if (Agent != null)
        //{
        //    Agent.enabled = true;
        //    Agent.ResetPath();
        //}

    }
}


