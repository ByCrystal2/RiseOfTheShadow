using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMotor : MonoBehaviour
{
    EnemyStats es;
    EnemyMovement em;
    EnemySoundManager eSound;
    GunManager gunManager;
    PlayerStats playerStats;
    PlayerMotor playerMotor;
    PlayerMovement playerMovement;

    SpriteRenderer spriteR;
    [SerializeField] Material healMaterial;
    [SerializeField] Material damageMaterial;
    private Material DefaultMaterial;


    public Transform player; // Oyuncu referans�
    public float detectionRange = 5.0f; // Alg�lama menzili
    public float rotationSpeed = 5.0f; // D�n�� h�z�

    

    public bool isFacingRight = true; // Sa�a do�ru bak�l�yor mu?

    public bool isShotting;
    public bool audioControl = false;


    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
        DefaultMaterial = spriteR.material;
        es = GetComponent<EnemyStats>();
        em = GetComponent<EnemyMovement>();
        eSound = GetComponent<EnemySoundManager>();
        gunManager = GetComponent<GunManager>();
        GameObject p = GameObject.FindWithTag("Player");
        player = p.gameObject.transform;
        playerStats = p.gameObject.GetComponent<PlayerStats>();
        playerMotor = p.gameObject.GetComponent<PlayerMotor>();
        playerMovement = p.gameObject.GetComponent<PlayerMovement>();
    }
    void Start()
    {
        SetCurrentEnemyLevelStats(es.GetEnemyLevel());        
    }

    
    void Update()
    {
        if (es.GetCurrentHealth()<= 0)
        {
            es.SetIsDead(true);
        }
        if (es.GetIsDead())
        {            
            eSound = gameObject.GetComponent<EnemySoundManager>();
            GameData.instance.GetCurrentPlayerStats().SetEarnMoney(Random.Range(5, 7) * es.GetEnemyLevel());
            eSound.GetEnemyAudio().PlayOneShot(eSound.GetEnemyDeathClip());
            Destroy(gameObject);
            return;
        }
        // Oyuncu ile d��man aras�ndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isShotting = true;
            // D��man oyuncunun range'ine girdi

            // Hedef y�n� hesapla
            Vector2 directionToPlayer = player.position - transform.position;

            // Hedef y�n� normalle�tir ve y eksenini dikkate almadan �evir
            float targetRotation = Mathf.Atan2(directionToPlayer.x, directionToPlayer.y) * Mathf.Rad2Deg;

            // D��man� yava��a hedef y�n�ne �evir
            float newRotation = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, newRotation);

            // D��man�n y�z� do�ru y�nde mi?
            if ((isFacingRight && directionToPlayer.x < 0) || (!isFacingRight && directionToPlayer.x > 0))
            {
                Flip();
            }
        }
        if (isShotting)
        {
            gunManager.Shot(false,gameObject);
            isShotting = false;
        }
    }

    private void SetCurrentEnemyLevelStats(int level)
    {
        switch (level)
        {
            case 1:
                // 1 level Enemy i�in kod 
                es.SetBaseStats(15, 2, 80, false);
                break;
            case 2:
                es.SetBaseStats(20, 5, 90, false);
                // 2 level Enemy i�in kod
                break;
            case 3:
                es.SetBaseStats(25, 8, 110, false);
                // 3 level Enemy i�in kod
                break;
            case 4:
                es.SetBaseStats(30, 10, 130, false);
                // 4 level Enemy i�in kod
                break;
            case 5:
                es.SetBaseStats(35, 12, 150, false);
                // 5 level Enemy i�in kod
                break;
            default: break;
        }
    }

    private void Flip()
    {
        // D��man�n y�z�n� d�nd�r
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            playerStats.AddorReductionCurrentHealth(5, false);
            playerMovement.Jump(10);
            playerMovement.SetIsGround(true);
            StartCoroutine(playerMotor.SetMaterial(false, 1f));
        }        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ammo") && !(LayerMask.LayerToName(col.gameObject.layer) == "Enemy"))
        {
            es.AddorReductionCurrentHealth(playerStats.GetCurrentAttackDamage(), false);
            SetMaterial(false, 0.2f);
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
        else spriteR.material = damageMaterial;
        yield return new WaitForSeconds(duration);
        spriteR.material = DefaultMaterial;
    }
    public IEnumerator WaitAudioControl(float duration)
    {
        
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
        audioControl = true;
    }
}
