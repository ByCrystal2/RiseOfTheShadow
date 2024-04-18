using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] GameObject pnlMarket;
    [SerializeField] GameObject pnlMenu;
    [SerializeField] Text[] txtsGoldCount;
    [SerializeField] GameObject[] btnsBuy;

    [SerializeField] TextMeshProUGUI txtCurrentGold;
    
    GameObject player;

    PlayerStats playerStats;
    private void Awake()
    {
        
    }
    void Start()
    {
        if (GameData.instance.First)
        {
            GameData.instance.LoadGame(GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerStats>());
        }
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendItem(int itemID)
    {
        int sellGold = itemID switch
        {
            (< 3) => int.Parse(txtsGoldCount[0].text),
            (< 4) => int.Parse(txtsGoldCount[1].text),
            (< 5) => int.Parse(txtsGoldCount[2].text),
            (< 6) => int.Parse(txtsGoldCount[3].text),
            _ => 5
        };

        player = GameObject.FindWithTag("Player");
        PlayerStats ps = player.GetComponent<PlayerStats>();
        ps.SetCurrentItemID(itemID);
        ps.SetSellMoney(sellGold);
        txtCurrentGold.text = ps.GetMoney().ToString();
        GameData.instance.Save(ps.GetSaveName());
        BuyButtonsInteractableControl(ps);
    }

    public void OpenMarketPanel()
    {
        pnlMarket.SetActive(true);
        PlayerStats ps = player.GetComponent<PlayerStats>();
        BuyButtonsInteractableControl(ps);
        txtCurrentGold.text = ps.GetMoney().ToString();
        
    }

    public void ExitMarketPanel()
    {
        pnlMarket.SetActive(false);
        pnlMenu.SetActive(true);
    }

    public void BuyButtonsInteractableControl(PlayerStats ps)
    {
        for (int i = 0; i < txtsGoldCount.Length; i++)
        {
            int txtGold = int.Parse(txtsGoldCount[i].text);
            if (txtGold <= ps.GetMoney())
            {
                btnsBuy[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                btnsBuy[i].GetComponent<Button>().interactable = false;
            }
        }
    }



    
    private int GetCurrentLevelIndex(PlayerStats ps)
    {
        return ps.GetPlayerLevel();
    }

    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(LevelManager.instance.levelNames[levelIndex]);
    }

    public void StartGameLevel() // PlayButton in MainScene/Canvas/pnlMainMenu
    {
        
        int savedLevelIndex = GetCurrentLevelIndex(playerStats);
        Debug.Log("OYUNCU LEVELÝ: " + savedLevelIndex);
        gameObject.GetComponent<MainMenuSoundManager>().GetAudioSource().enabled = false;
        LoadLevel(savedLevelIndex);
        GameObject.FindObjectOfType<LevelSoundManager>().PlayCurrentLevelClip(savedLevelIndex);
        Debug.Log("Level baþladý");
    }
}
