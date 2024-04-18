using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{    
    
    public GameObject[] ammoPrefabs;

    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawn;

    public float enemySayac;
    public float playerSayac;

    PlayerInventory inventory;
    PlayerSoundManager sound;
    public int currentPlayerAmmoId;

    GameObject player;   
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player.GetComponent<PlayerInventory>() == null)
        {
            Debug.Log("Null Envanter");
        }
        if (gameObject.transform.parent.gameObject.CompareTag("Player"))
        {
            inventory = player.GetComponent<PlayerInventory>();
            sound = player.GetComponent<PlayerSoundManager>();
        }
    }
    private void Update()
    {
        if (gameObject.transform.parent.gameObject.CompareTag("Player"))
        {
        // Fare sol t�klama alg�land���nda ate� et
        if (Input.GetButtonDown("Fire1"))
        {
                Shot(true,player);
                sound.GetPlayerAudio().PlayOneShot(sound.GetPlayerShotClip());
        }
        }
    }
    public void Shot(bool player, GameObject who)
    {
        playerSayac -= Time.deltaTime;
        if (player && enemySayac <= 0)
        {
            Debug.Log("Player Ate� Ediyor");
            GameObject Player = who;           
            // Mermiye h�z ve y�n ver
           
            GameObject NewAmmo = Instantiate(ammoPrefabs[currentPlayerAmmoId], ammoSpawn.position, Quaternion.identity); 
            AmmoManager newAmmoManager = NewAmmo.GetComponent<AmmoManager>();

            newAmmoManager.speedMultiply = 15f;
            newAmmoManager.who = who;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 fireDirection = (mousePosition - ammoSpawn.position).normalized;
            Rigidbody2D rb = NewAmmo.GetComponent<Rigidbody2D>();
            rb.velocity = fireDirection * newAmmoManager.speedMultiply;

            Destroy(NewAmmo, 2f); // �rne�in 2 saniye sonra mermiyi yok et.
            playerSayac = 1f;
        }
        //audioSource.PlayOneShot(ammoSound,0.5f);
        else
        {
            GameObject enemy = who;
            enemySayac -= Time.deltaTime;
            if (enemySayac <= 0)
            {
                WaitShot(enemy);
                enemySayac = 1f;
            }
        }

        
        
    }

   
        

        
     

        
    
    public void WaitShot(GameObject enemy)
    {
        
        EnemyStats enemyStats = gameObject.GetComponent<EnemyStats>();
        EnemySoundManager enemySound = gameObject.GetComponent<EnemySoundManager>();
        GameObject newAmmo = Instantiate(ammoPrefab, ammoSpawn.position, Quaternion.identity);
        AmmoManager newAmmoManager = newAmmo.GetComponent<AmmoManager>();
        newAmmoManager.who = enemy;
        switch (newAmmoManager.selectedType)
        {
            case AmmoManager.AmmoType.Shotgun:
                enemySound.GetEnemyAudio().PlayOneShot(enemySound.GetEnemyShotgunClip());
                break;
            case AmmoManager.AmmoType.Magic:
                enemySound.GetEnemyAudio().PlayOneShot(enemySound.GetEnemyMagicClip());
                break;
            case AmmoManager.AmmoType.Arrow:
                enemySound.GetEnemyAudio().PlayOneShot(enemySound.GetEnemyArrowClip());
                break;
            case AmmoManager.AmmoType.SmallShot:
                break;
            default:
                break;
        }

        // Mermi y�n�n� belirle
        float ammoSpeed = enemyStats.GetCurrentAttackSpeed();
        int ammoDamage = enemyStats.GetCurrentAttackDamage();
        if (gameObject.GetComponent<EnemyMotor>().isFacingRight)
        {
            ammoSpeed = Mathf.Abs(ammoSpeed); // Sa�a do�ru at��
        }
        else
        {
            ammoSpeed = -Mathf.Abs(ammoSpeed); // Sola do�ru at��
        }

        // Mermiye h�z� ver
        newAmmo.GetComponent<Rigidbody2D>().velocity = new Vector2(ammoSpeed * 20, 0);
        newAmmo.GetComponent<AmmoManager>().SetTargetAndSpeed(ammoDamage,0,newAmmo.transform);
        Debug.Log("Ate� eden d��man: " + newAmmoManager.who);
        Destroy(newAmmo, 3f);
        
    }
}
