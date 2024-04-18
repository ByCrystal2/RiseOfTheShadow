using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>(); // Oyuncu envanteri 
    public Item equippedItem; // Giyilen e�ya

    private PlayerStats playerStats;
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public Item GetEquipItem()
    {
        return equippedItem;
    }

    public void EquipItem(Item item)
    {
        equippedItem = item;

        // Oyuncunun e�ya �zelliklerini g�ncelle
        UpdatePlayerStats();
        Debug.Log("Giyilen item ID'si:" + GetEquipItem().ID);
        GameObject gun = transform.GetChild(0).gameObject;
        GunManager gunManager = gun.GetComponent<GunManager>();
        gunManager.currentPlayerAmmoId = GetEquipItem().AmmoID;
        gameObject.GetComponent<SpriteRenderer>().sprite = GetEquipItem().Icon;        
    }

    public void UnequipItem()
    {
        equippedItem = null;

        // Oyuncunun e�ya �zelliklerini varsay�lan de�erlere d�nd�r
        ResetPlayerStats();
    }
    private void UpdatePlayerStats()
    {
        if (equippedItem != null)
        {
            // E�ya �zelliklerini oyuncu �zelliklerine yans�t
            AddItemStatsInPlayer(equippedItem);
        }
    }

    private void ResetPlayerStats()
    {
        // Oyuncu �zelliklerini varsay�lan de�erlere d�nd�r
        ResetItemStatsInPlayer();
    }

    public void AddItemStatsInPlayer(Item item)
    {
        playerStats.AddorReductionCurrentAttackDamage(item.Stats["Attack Power"],true);
        playerStats.AddorReductionCurrentAttackSpeed(item.Stats["Attack Speed"], true);
        playerStats.AddorReductionCurrentHealth(item.Stats["Health"], true);
    }
    public void ReductionItemStatsInPlayer(Item item)
    {
        playerStats.AddorReductionCurrentAttackDamage(item.Stats["Attack Power"],false);
        playerStats.AddorReductionCurrentAttackSpeed(item.Stats["Attack Speed"],false);
        playerStats.AddorReductionCurrentHealth(item.Stats["Health"], false);
    }
    private void ResetItemStatsInPlayer()
    {
        playerStats.ResetCurrentStats();
    }
}
