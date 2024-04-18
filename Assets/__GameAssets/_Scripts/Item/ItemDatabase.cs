using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    List<Item> items = new List<Item>();
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    private void Awake()
    {
        BuildItemDatabase();        
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.ID == id);        
    }

    public Item GetItem(string name)
    {
        return items.Find(item => item.Name == name);
    }
    private void BuildItemDatabase()
    {
        items = new List<Item>()
        {
            new Item(1,0, "Base Gun", "Oyun ba�lang�c�nda verilen ba�lang�� silah�.",sprites[0],
            new Dictionary<string, int>
            {
                { "Attack Speed", 3 },
                { "Attack Power", 5 },
                { "Health", 7 }
            }),
            
            new Item(2,1, "Level2 Gun", "Level2'de kilidi a��lan ba�lang�� seviye silah",sprites[1],
            new Dictionary<string, int>
            {
                { "Attack Speed", 6 },
                { "Attack Power", 10 },
                { "Health", 14 }
            }),
            
            new Item(3,2, "Level3 Gun", "Level3'de kilidi a��lan orta seviye silah.",sprites[2],
            new Dictionary<string, int>
            {
                { "Attack Speed", 12 },
                { "Attack Power", 20 },
                { "Health", 28 }
            }),
            
            new Item(4,3, "Level4 Gun", "Level4'de kilidi a��lan y�ksek seviye silah.",sprites[3],
            new Dictionary<string, int>
            {
                { "Attack Speed", 14 },
                { "Attack Power", 25 },
                { "Health", 35 }
            }),
            
            new Item(5,4, "Level5 Gun", "Level4'de kilidi a��lan maksimum seviye silah.",sprites[4],
            new Dictionary<string, int>
            {
                { "Attack Speed", 16 },
                { "Attack Power", 30 },
                { "Health", 50 }
            })
        
    };


}
}
