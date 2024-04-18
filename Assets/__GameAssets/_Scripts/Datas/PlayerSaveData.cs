using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public string SaveName;

    public int playerScore; // Oyuncu puan�
    public int playerLevel; // Oyuncu seviyesi
    public int treasureCount; // Hazine say�s�
    public int playerMoney; // Oyuncu paras�

    public int baseAttackDamage;
    public float baseAttackSpeed;
    public float baseHealth;

    public float currentHealth;
    public float maxHealth;
    public int currentAttackDamage;

    public float currentAttackSpeed;

    public int currentItemID;
}
