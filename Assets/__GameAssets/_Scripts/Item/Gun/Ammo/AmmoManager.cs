using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.Examples.TMP_ExampleScript_01;

public class AmmoManager : MonoBehaviour
{
    int Damage;
    public Transform Target;

    public float playerpeed;
    public float enemySpeed = 1000;
    public float speedMultiply;
    public GameObject who;

    [SerializeField]
    public AmmoType selectedType;


    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
                  
    }

   

    public void SetTargetAndSpeed(int damage, float _speed, Transform _target)
    {
        Damage = damage;              
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        



    }

    public enum AmmoType
    {
        Shotgun,
        Magic,
        Arrow,
        SmallShot
    }



}
