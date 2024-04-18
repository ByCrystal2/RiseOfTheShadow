using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>(); // Oyuncu envanteri 
    public Item equippedItem; // Giyilen eþya

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

        // Oyuncunun eþya özelliklerini güncelle
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

        // Oyuncunun eþya özelliklerini varsayýlan deðerlere döndür
        ResetPlayerStats();
    }
    private void UpdatePlayerStats()
    {
        if (equippedItem != null)
        {
            // Eþya özelliklerini oyuncu özelliklerine yansýt
            AddItemStatsInPlayer(equippedItem);
        }
    }

    private void ResetPlayerStats()
    {
        // Oyuncu özelliklerini varsayýlan deðerlere döndür
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
