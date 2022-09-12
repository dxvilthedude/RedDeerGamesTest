using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private EnemyManager enemyManager;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float attackRange;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animation anim;
    [SerializeField] private Image hpBar;

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int pointsForKill;
    [SerializeField] private float health = 100;
    [SerializeField] private float currentHealth;

    private GameManager gameManager;
    private bool playerInAttackRange;
    private bool alreadyAttacked;
    private bool playerInAttackCollider;
    private PlayerHealth playerHealth;
    private float damage = 10;
    public void SetEnemy(Transform playerTransform, EnemyManager eManager, GameManager gManager)
    {
        player = playerTransform;
        enemyManager = eManager;
        gameManager = gManager;
        playerHealth = player.GetComponent<PlayerHealth>();
        currentHealth = 100;
        UpdateHPBarUI();
    }
    void Update()
    {
        if (gameManager.GameOn)
        {
            transform.LookAt(player);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

            if (!playerInAttackRange) ChasePlayer();
            else Attack();
        }
    }
    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
    }
    private void Attack()
    {
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            // ATTACK

            anim.Play();
            if (playerInAttackCollider)
                playerHealth.TakeDamage(10);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        UpdateHPBarUI();
        if (currentHealth <= 0)
            Death();
    }
    private void UpdateHPBarUI()
    {
        hpBar.fillAmount = currentHealth / health;
    }
    private void Death()
    {
        enemyManager.KillPointsAward(pointsForKill);
        enemyManager.BackToPool(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        playerInAttackCollider = true;
    }
    private void OnTriggerExit(Collider other)
    {
        playerInAttackCollider = false;
    }
}
