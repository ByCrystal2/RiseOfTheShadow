using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseStats
{
    private string SaveName;

    private bool isDead; // Olme durumu

    private int playerScore; // Oyuncu puaný
    private int playerLevel; // Oyuncu seviyesi
    private int treasureCount; // Hazine sayýsý
    public int playerMoney; // Oyuncu parasý

    private int currentItemID; // Oyuncu item ID'si

    public void SetBasePlayerStats(int playerScore, int playerLevel, int treasureCount, int playerMoney, int itemID)
    {
        this.playerScore = playerScore;
        this.playerLevel = playerLevel;
        this.treasureCount = treasureCount;
        this.playerMoney = playerMoney;
        this.currentItemID = itemID;
        //GameData.instance.SetPlayerStatsPrefs(playerScore, playerLevel, treasureCount, playerMoney);
    }
    public void SetSaveName(string saveName)
    {
        this.SaveName = saveName;
    }
    public void SetCurrentItemID(int itemID)
    {
        this.currentItemID = itemID;
    }
    public void SetIncreaseScore(int amount)
    {
        playerScore += amount;
        Debug.Log("Puan: " + playerScore);
    }

    public void SetLevelUp()
    {
        playerLevel++;
        Debug.Log("Seviye: " + playerLevel);
    }

    public void SetCollectTreasure()
    {
        treasureCount++;
        Debug.Log("Toplanan Hazine Sayýsý: " + treasureCount);
    }

    public void SetEarnMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Kazanýlan Para: " + playerMoney);
    }

    public void SetSellMoney(int amount)
    {
        playerMoney -= amount;
        Debug.Log("Eksilen Para: " + playerMoney);
    }

    public void SetIsDead(bool dead)
    {
        isDead = dead;
    }

    public string GetSaveName()
    {
        return SaveName;
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }
    public int GetCollectTreasure()
    {
        return treasureCount;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public int GetCurrentItemID()
    {
        return currentItemID;
    }
}
