using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public string SaveName;

    public int playerScore; // Oyuncu puaný
    public int playerLevel; // Oyuncu seviyesi
    public int treasureCount; // Hazine sayýsý
    public int playerMoney; // Oyuncu parasý

    public int baseAttackDamage;
    public float baseAttackSpeed;
    public float baseHealth;

    public float currentHealth;
    public float maxHealth;
    public int currentAttackDamage;

    public float currentAttackSpeed;

    public int currentItemID;
}
