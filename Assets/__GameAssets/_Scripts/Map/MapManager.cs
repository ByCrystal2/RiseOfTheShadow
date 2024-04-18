using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] mapPrefabs;
    public float mapSpawnDistance = 3000f;
    public int initialMapCount = 2;
    public int offsetForSpawn;
    public int currentMapIndex;

    public Transform mapPanel; // Panel referansý

    private Transform lastSpawnedMap;
    [SerializeField] Transform SpawnStart;

    ObstraclesManager obstraclesManager;

    private void Awake()
    {
        
    }


    private void Start()
    {

        lastSpawnedMap = SpawnStart;
        for (int i = 0; i < initialMapCount; i++)
        {
            SpawnMap();
        }
    }

    private void Update()
    {
        if (player.position.x > lastSpawnedMap.position.x - mapSpawnDistance && mapPrefabs.Length > currentMapIndex)
        {
            SpawnMap();
        }
    }

    void SpawnMap()
    {
        if (mapPrefabs.Length < currentMapIndex)
        {
            return;
        }
        GameObject mapPrefab = mapPrefabs[currentMapIndex];
        currentMapIndex++;

        Vector3 offset = lastSpawnedMap.position + new Vector3(mapSpawnDistance, 0f, 0f); // Önceki haritanýn sonundan mapSpawnDistance kadar saða kaydýr
        offset.y = 0;
        Transform newMap = Instantiate(mapPrefab, offset, Quaternion.identity, mapPanel).transform;
        
        
        lastSpawnedMap = newMap.GetChild(4).transform;

        

        if (newMap.gameObject.GetComponent<ObstraclesManager>() == null)
            return;
        
        obstraclesManager = newMap.gameObject.GetComponent<ObstraclesManager>();
        GameObject[] lastObstracles = new GameObject[obstraclesManager.GetObstraclesPrefabs().Length];
        
        
        List<int> spawnX = new List<int>();
 
        int oStart = (int)(obstraclesManager.obstraclesStart.position.x);
        int oEnd = (int)obstraclesManager.obstraclesEnd.position.x;

        
        
        
        for (int i = oStart; i <= oEnd; i++)
        {
            spawnX.Add(i);
        }
        Debug.Log("Spawnx Count: " + spawnX.Count);
        foreach (var obs in obstraclesManager.GetObstraclesPrefabs())
        {
           
            int spawnXLocation = UnityEngine.Random.Range(0,spawnX.Count);
            
            Debug.Log("Engelin kontorlü: " + spawnXLocation);
            Vector3 spawnPoint = new Vector3(spawnX[spawnXLocation], -3.31f, 0f);
            if (currentMapIndex == 4)
            {
                spawnPoint.y = -3.61f;
            }
            GameObject newObs = Instantiate(obs, spawnPoint, Quaternion.identity);

            for (int i = spawnXLocation + offsetForSpawn; i >= spawnXLocation - offsetForSpawn; i--)
            {
                if (i < 0 || i >= spawnX.Count)
                {
                    continue;
                }
                if (spawnX.Count == 0) break;
                spawnX.RemoveAt(i);
                Debug.Log(i);
            }
            if (spawnX.Count == 0) break;
        }

        


    }


}
