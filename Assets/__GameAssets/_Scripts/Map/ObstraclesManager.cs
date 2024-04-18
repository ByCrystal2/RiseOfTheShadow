using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstraclesManager : MonoBehaviour
{
    [SerializeField] public Transform obstraclesStart;
    [SerializeField] public Transform obstraclesEnd;

    [SerializeField] GameObject[] obstraclesPrefabs;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] GetObstraclesPrefabs()
    {
        return obstraclesPrefabs;
    }

    
}
