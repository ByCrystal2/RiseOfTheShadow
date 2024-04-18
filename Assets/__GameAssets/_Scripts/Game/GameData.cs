using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;

public class GameData : MonoBehaviour
{
    public static GameData instance; // Singleton instance

    private string pathToDatasFile;
    private const string DatasFile = "RiseOfTheShadowDatas.json";
    PlayerStats ps;
    PlayerInventory pi;
    public PlayerMovement pm;
    public PlayerMotor pmtr;
    PlayerSaveData saveData;

    ItemDatabase itemDB;
    public bool First;
    public bool FinishLevel;

    private AudioSource[] allAudioSources;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        First = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerStats>();
        pi = player.GetComponent<PlayerInventory>();
        pm = player.GetComponent<PlayerMovement>();
        pmtr = player.GetComponent<PlayerMotor>();
        itemDB = GetComponent<ItemDatabase>();
        pathToDatasFile = Application.persistentDataPath + "/" + DatasFile;
    }
    private void Start()
    {
       
        // Oyunu ilk defa açýp açmadýðýný kontrol et
        if (File.Exists(pathToDatasFile)) //Check the save name is exist. Dosya ismi olcuak bu sende tek save olacagi icin sadece Save falan desen yine olur. coklu save icin siim kontrolu yaptim ben burda
        {
            Debug.Log("This name of save is already exist. Save will overwrite.");
            Debug.Log("Oyuncu daha önce oynamýþ.");
            LoadGame(ps);
        }
        else
        {
            Debug.Log("This name of save is free. Will create a new save file.");
            
            ps.SetBaseStats(10, 5f, 100f, true);
            ps.SetAllStats(100f, 5f, 10, 500);
            ps.SetBasePlayerStats(0, 1, 0, 0,1);            
            
            Save(DatasFile);
        }
        pi.EquipItem(itemDB.GetItem(ps.GetCurrentItemID()));
        
    }

    public void LoadGame(PlayerStats ps)
    {
        string jsonString = File.ReadAllText(pathToDatasFile); // read the json file from the file system
        PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(jsonString); // de-serialize the data to your myData object
        ps.SetSaveName(DatasFile);
        ps.SetAllStats(data.currentHealth, data.currentAttackSpeed, data.currentAttackDamage, 500);
        ps.SetBasePlayerStats(data.playerScore, data.playerLevel, data.treasureCount, data.playerMoney, data.currentItemID);
        ps.SetBaseStats(data.baseAttackDamage, data.baseAttackSpeed, data.baseHealth);
    }
    public void Save(string saveName)
    {
        PlayerSaveData savedata = new PlayerSaveData();

        if (File.Exists(Application.persistentDataPath + "/" + saveName)) //Check the save name is exist. Dosya ismi olcuak bu sende tek save olacagi icin sadece Save falan desen yine olur. coklu save icin siim kontrolu yaptim ben burda
        {
            Debug.Log("This name of save is already exist. Save will overwrite.");
        }
        else
        {
            Debug.Log("This name of save is free. Will create a new save file.");
        }
        savedata.SaveName = ps.GetSaveName();
        savedata.currentAttackDamage = ps.GetCurrentAttackDamage();
        savedata.currentAttackSpeed = ps.GetCurrentAttackSpeed();
        savedata.currentHealth = ps.GetCurrentHealth();

        savedata.playerScore = ps.GetPlayerScore();
        savedata.playerLevel = ps.GetPlayerLevel();
        savedata.playerMoney = ps.GetMoney();
        savedata.treasureCount = ps.GetCollectTreasure();
        savedata.currentItemID = ps.GetCurrentItemID();

        savedata.baseAttackDamage = ps.GetBaseAttackDamage();
        savedata.baseAttackSpeed = ps.GetBaseAttackSpeed();
        savedata.baseHealth = ps.GetBaseHealth();
        savedata.maxHealth = ps.GetMaxHealth();

        string jsonString = JsonUtility.ToJson(savedata); // this will give you the json (i.e serialize the data)
        
            File.WriteAllText(Application.persistentDataPath + "/" + saveName, jsonString); // this will write the json to the specified path
        Debug.Log("Game saved!");
        Debug.Log("Game Save Location: " + Application.persistentDataPath + "/" + saveName);
    }

    public void Save()
    {
        saveData = new PlayerSaveData();

        if (File.Exists(Application.persistentDataPath + "/" + ps.GetSaveName())) //Check the save name is exist. Dosya ismi olcuak bu sende tek save olacagi icin sadece Save falan desen yine olur. coklu save icin siim kontrolu yaptim ben burda
        {
            Debug.Log("This name of save is already exist. Save will overwrite.");
        }
        else
        {
            Debug.Log("This name of save is free. Will create a new save file.");
        }
        saveData.SaveName = ps.GetSaveName();
        saveData.currentAttackDamage = ps.GetCurrentAttackDamage();
        saveData.currentAttackSpeed = ps.GetCurrentAttackSpeed();
        saveData.currentHealth = ps.GetCurrentHealth();

        saveData.playerScore = ps.GetPlayerScore();
        saveData.playerLevel = ps.GetPlayerLevel();
        saveData.playerMoney = ps.GetMoney();
        saveData.treasureCount = ps.GetCollectTreasure();
        saveData.currentItemID = ps.GetCurrentItemID();

        saveData.baseAttackDamage = ps.GetBaseAttackDamage();
        saveData.baseAttackSpeed = ps.GetBaseAttackSpeed();
        saveData.baseHealth = ps.GetBaseHealth();
        saveData.maxHealth = ps.GetMaxHealth();

        string jsonString = JsonUtility.ToJson(saveData.SaveName); // this will give you the json (i.e serialize the data)

        File.WriteAllText(Application.persistentDataPath + "/" + saveData.SaveName, jsonString); // this will write the json to the specified path
        Debug.Log("Game saved!");
        Debug.Log("Game Save Location: " + Application.persistentDataPath + "/" + saveData.SaveName);
    }

    public void GameOver()
    {
        pmtr.gameObject.GetComponent<Animator>().SetBool("Dead", true);
        ps.SetIsDead(true);
        pm.SetMoveSpeed(0);
        pm.SetJumpForce(0);

        
        SetAllAudioSourcesClose();
        pmtr.healthBar.gameObject.SetActive(false);
        GetGameOverLevel();
    }

    public void GetFinishLevel()
    {
        
        UIManager ui = GameObject.FindObjectOfType<UIManager>();
        StartCoroutine(ui.Win());

    }

    public void GetGameOverLevel()
    {

        UIManager ui = GameObject.FindObjectOfType<UIManager>();
        StartCoroutine(ui.GameOver());

    }

    public PlayerStats GetCurrentPlayerStats()
    {
        return ps;
    }

    public void CompleteLevel()
    {
        
        int currentLevelIndex = ps.GetPlayerLevel();
        

        if (currentLevelIndex <= LevelManager.instance.levelNames.Length)
        {

            ps.SetLevelUp();
        }
        else
        {
            Debug.Log("Oyun bitti!");
        }
    }

    public void SetAllAudioSourcesClose()
    {
        allAudioSources = Object.FindObjectsOfType<AudioSource>();

        foreach (var audioSoruce in allAudioSources)
        {
            audioSoruce.Stop();
            GameObject enemy = audioSoruce.gameObject.transform.parent.gameObject;
            if (enemy.CompareTag("Enemy"))
            {
                audioSoruce.enabled = false;
                enemy.GetComponent<EnemySoundManager>().enabled = false;
            }
        }
    }

}
