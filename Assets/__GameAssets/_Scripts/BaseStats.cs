using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseStats:MonoBehaviour
{
    private int baseAttackDamage;
    private float baseAttackSpeed;
    private float baseHealth;

    public float currentHealth;
    public  float maxHealth;
    public int currentAttackDamage;

    private float currentAttackSpeed;


    public bool isMonster;
    public void SetBaseStats(int baseAttackDamage,float baseAttackSpeed,float baseHealth)
    {
        this.baseAttackDamage = baseAttackDamage;
        this.baseAttackSpeed = baseAttackSpeed;
        this.baseHealth = baseHealth;
        
        
    }
    public void SetBaseStats(int baseAttackDamage, float baseAttackSpeed, float baseHealth, bool player)
    {
        this.baseAttackDamage = baseAttackDamage;
        this.baseAttackSpeed = baseAttackSpeed;
        this.baseHealth = baseHealth;

        if (player)
        {
            SetAllStats(baseHealth, baseAttackSpeed, baseAttackDamage,500);
        }
        else
            SetAllStats(baseHealth, baseAttackSpeed, baseAttackDamage, 200);
    }

    public void SetAllStats(float health, float attackSpeed, int attackDamage , float maxHealth)
    {
        this.currentHealth = health;
        this.currentAttackDamage = attackDamage;
        this.currentAttackSpeed = attackSpeed;
        this.maxHealth = maxHealth;


        //GameData.instance.SetAllPlayerStatsPrefs(attackDamage, attackSpeed, health);
    }

    public void SetMaxHeal(float amount)
    {
        this.maxHealth = amount;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }


    public int GetCurrentAttackDamage()
    {
        return currentAttackDamage;
    }
    public float GetCurrentAttackSpeed()
    {
        return currentAttackSpeed;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetBaseAttackDamage()
    {
        return baseAttackDamage;
    }

    public float GetBaseAttackSpeed()
    {
        return baseAttackSpeed;
    }

    public float GetBaseHealth()
    {
        return baseHealth;
    }

    
    public void AddorReductionCurrentAttackDamage(int amount, bool add)
    {
        if (add)        
            this.currentAttackDamage += amount;        
        else
            this.currentAttackDamage -= amount;
    }

    public void AddorReductionCurrentAttackSpeed(int amount, bool add)
    {
        if (add)
            this.currentAttackSpeed += amount;
        else
            this.currentAttackSpeed -= amount;
    }

    public void AddorReductionCurrentHealth(int amount, bool add)
    {
        if (add)
            this.currentHealth += amount;
        else
        {
            this.currentHealth -= amount;

            if (this.currentHealth <= 0 && !isMonster && GameData.instance.GetCurrentPlayerStats().GetIsDead())
            {
                //GameData.instance.GameOver();
                //Debug.Log("Oyuncu Öldü!");
                //return;
            }
            else if (this.currentHealth <= 0 && isMonster)
            {
                //EnemyStats es = gameObject.GetComponent<EnemyStats>();
                //es.SetIsDead(true);
                //EnemySoundManager eSound = gameObject.GetComponent<EnemySoundManager>();
                //GameData.instance.GetCurrentPlayerStats().SetEarnMoney(Random.Range(5, 7) * es.GetEnemyLevel());                
                //eSound.GetEnemyAudio().PlayOneShot(eSound.GetEnemyDeathClip());
                //Destroy(gameObject,1f);
            }
            
        }
    }

    public void ResetCurrentStats()
    {
        this.currentAttackDamage = this.baseAttackDamage;
        this.currentAttackSpeed = this.baseAttackSpeed;
        this.currentHealth = this.baseHealth;
    }

    

}
