using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMotor : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerInventory playerInventory;
    PlayerMovement playerMovement;
    PlayerSoundManager playerSoundManager;
    ItemDatabase playerItemDatabase;
    EnemyStats enemyStats;
    public Animator anim;
    SpriteRenderer spriteR;
    public UIManager ui;

    Rigidbody2D rb;

    [SerializeField] public GameObject healthBar;
    [SerializeField] TextMeshProUGUI txtHealthPercent;
    [SerializeField] Material healMaterial; 
    [SerializeField] Material damageMaterial; 
    private Material DefaultMaterial;

    public bool first = true;
    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
        DefaultMaterial = spriteR.material;
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponent<PlayerInventory>();
        playerMovement = GetComponent<PlayerMovement>();
        playerItemDatabase = GameObject.FindObjectOfType<ItemDatabase>();
        playerSoundManager = GetComponent<PlayerSoundManager>();

        
        anim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().buildIndex != 0 || !GameData.instance.First)
        {
            GameData.instance.LoadGame(playerStats); 
            playerStats.SetMaxHeal(playerStats.GetCurrentHealth());
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                ui = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
            healthBar.GetComponent<Slider>().maxValue = playerStats.GetMaxHealth();
            healthBar.GetComponent<Slider>().value = playerStats.GetMaxHealth();
            }
            Debug.Log(playerStats.GetCurrentItemID());
            playerInventory.EquipItem(playerItemDatabase.GetItem(playerStats.GetCurrentItemID()));
           

        }
        first = true;
    }
    void Start()
    {
        Time.timeScale = 1;        
    }

    
    void Update()
    {
        
        if(playerStats.GetCurrentHealth() <= 0){ playerStats.SetIsDead(true);}
        if (playerStats.GetIsDead())
        {
            anim.SetBool("Dead", true);
            
            playerMovement.SetMoveSpeed(0);
            playerMovement.SetJumpForce(0);


            GameData.instance.SetAllAudioSourcesClose();
            healthBar.gameObject.SetActive(false);
            GameData.instance.GetGameOverLevel();
            Debug.Log("Oyuncu Öldü!");

        }
        if (Input.GetKeyDown(KeyCode.Space) && playerMovement.isGround)
        {
            anim.SetBool("Jump", true);
            playerMovement.Jump();
            playerSoundManager.GetPlayerAudio().Stop();
            playerSoundManager.GetPlayerAudio().PlayOneShot(playerSoundManager.GetPlayerJumpClip());
            playerMovement.isGround = false;
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move();        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Jump", false);
            playerSoundManager.GetPlayerAudio().clip = playerSoundManager.GetPlayerRunClip();
            playerSoundManager.GetPlayerAudio().Play();
            playerMovement.isGround = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ammo") && !(LayerMask.LayerToName(col.gameObject.layer) == "Player") && !(col.gameObject.GetComponent<AmmoManager>().who.gameObject.GetComponent<EnemyStats>().GetIsDead()))
        {
            playerStats.AddorReductionCurrentHealth(col.gameObject.GetComponent<AmmoManager>().who.GetComponent<EnemyStats>().GetCurrentAttackDamage(), false);
            UpdateHealthBar();
            SetMaterial(false, 0.5f);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Ammo"))
        {
            Destroy(col.gameObject);
        }
    }

    public IEnumerator SetMaterial(bool heal, float duration)
    {
        if (heal) spriteR.material = healMaterial;        
        else      spriteR.material = damageMaterial;
        yield return new WaitForSeconds(duration);
        spriteR.material = DefaultMaterial;
    }

    private void SetCurrentEnemyLevelStats(int level)
    {
        //switch (level)
        //{
        //    case 1:
        //        // 1 level Player için kod 
        //        playerInventory.EquipItem(playerItemDatabase.GetItem(level));
        //        break;
        //    case 2:
        //        es.SetBaseStats(10, 5, 50, false);
        //        // 2 level Player için kod
        //        break;
        //    case 3:
        //        es.SetBaseStats(12, 8, 70, false);
        //        // 3 level Player için kod
        //        break;
        //    case 4:
        //        es.SetBaseStats(15, 10, 90, false);
        //        // 4 level Player için kod
        //        break;
        //    case 5:
        //        es.SetBaseStats(18, 12, 100, false);
        //        // 5 level Player için kod
        //        break;
        //    default: break;
        //}
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = playerStats.GetCurrentHealth() / playerStats.GetMaxHealth();
        healthBar.GetComponent<Slider>().value = playerStats.GetMaxHealth();
        txtHealthPercent.text = Mathf.RoundToInt(healthPercentage * 100).ToString() + "%";
    }


}
