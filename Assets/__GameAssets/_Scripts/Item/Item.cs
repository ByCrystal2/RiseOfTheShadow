using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID;
    public int AmmoID;
    public string Name;
    public string Description;
    public Sprite Icon;
    public Dictionary<string, int> Stats = new Dictionary<string, int>();

    public Item(int id,int ammoId, string name, string description,Sprite icon, Dictionary<string,int> stats)
    {
        this.ID = id;
        this.AmmoID = ammoId;
        this.Name = name;
        this.Description = description;
        this.Icon = icon;
        this.Stats = stats;
    }

    public Item(int id, int ammoId, string name, string description, Dictionary<string, int> stats)
    {
        this.ID = id;
        this.AmmoID = ammoId;
        this.Name = name;
        this.Description = description;        
        this.Stats = stats;
    }
}
