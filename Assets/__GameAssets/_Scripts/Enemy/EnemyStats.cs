using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : BaseStats
{
    [SerializeField] int EnemyLevel;
    [SerializeField] int EnemyMaxCountInLevel;

    private bool isDead;
    
    public bool GetIsDead()
    {
        return isDead;
    }
    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }
    public int GetEnemyLevel()
    {
        return EnemyLevel;
    }
    public int GetEnemyMaxCountInLevel()
    {
        return EnemyMaxCountInLevel;
    }
    
    }
