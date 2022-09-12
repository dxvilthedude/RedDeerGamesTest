using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private GameManager gameManager;
    private float currentHealth;

    private void Start()
    {
        currentHealth = health;
        HealthBarUpdateUI();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerDeath();
        }        
        HealthBarUpdateUI();
    }
    private void HealthBarUpdateUI()
    {
        healthBar.fillAmount = currentHealth / health;
        healthBarText.text = ((currentHealth / health)*100).ToString() + "%";
    }
    private void PlayerDeath()
    {
        gameManager.PlayerDeath();
    }
}
