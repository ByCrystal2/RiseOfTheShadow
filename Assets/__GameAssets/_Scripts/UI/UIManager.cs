using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Game Over Paneli")]
    [SerializeField] GameObject pnlEnd;
    [SerializeField] GameObject imgGameOver;
    [SerializeField] GameObject btnRestartGame;
    [SerializeField] GameObject btnMainMenu;
    [SerializeField] GameObject btnQuit;

    [Header("Level Finish Paneli")]
    [SerializeField] GameObject pnlFinish;
    [SerializeField] GameObject imgWin;
    
    [SerializeField] GameObject FbtnMainMenu;
    [SerializeField] GameObject FbtnQuit;

    private void Awake()
    {       
        
    }

    void Start()
    {
        SetAllObjActivation(false,true);
        SetAllObjActivation(false,false);
        AwakeObjAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RestartGame() // Restart Game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMainMenu() // Go Main Menu
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() // Quit Game
    {
        Application.Quit();
    }

    public void SetAllObjActivation(bool active, bool end)
    {
        if (end)
        {
        pnlEnd.SetActive(active); 
        imgGameOver.SetActive(active); 
        btnRestartGame.SetActive(active);
        btnMainMenu.SetActive(active);
        btnQuit.SetActive(active);
        }
        else
        {
            pnlFinish.SetActive(active);
            imgWin.SetActive(active);
            
            FbtnMainMenu.SetActive(active);
            FbtnQuit.SetActive(active);
        }
    }

    public void AwakeObjAlpha()
    {
        pnlEnd.GetComponent<CanvasGroup>().alpha = 0f;
        imgGameOver.GetComponent<CanvasGroup>().alpha = 0f;
        btnRestartGame.GetComponent<CanvasGroup>().alpha = 0f;
        btnMainMenu.GetComponent<CanvasGroup>().alpha = 0f;
        btnQuit.GetComponent<CanvasGroup>().alpha = 0f;

        pnlFinish.GetComponent<CanvasGroup>().alpha = 0f;
        imgWin.GetComponent<CanvasGroup>().alpha = 0f;
        
        FbtnMainMenu.GetComponent<CanvasGroup>().alpha = 0f;
        FbtnQuit.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public IEnumerator GameOver()
    {
        Debug.Log("Oyun Bitti!");
        SetAllObjActivation(true,true);
        yield return new WaitForSeconds(1f);
        pnlEnd.GetComponent<CanvasGroup>().DOFade(1, 2f);
        yield return new WaitForSeconds(1f);
        imgGameOver.GetComponent<CanvasGroup>().DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        imgGameOver.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        imgGameOver.SetActive(false);
        yield return new WaitForSeconds(1f);
        btnRestartGame.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        btnMainMenu.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        btnQuit.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        GameObject.FindWithTag("Player").gameObject.SetActive(false);
        foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            e.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        Invoke("SetTimeScale", 2f);
    }

    public IEnumerator Win()
    {
        Debug.Log("Leveli Kazandý!");
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMotor>().healthBar.gameObject.SetActive(false);
        player.GetComponent<PlayerSoundManager>().GetPlayerAudio().enabled = false;
        SetAllObjActivation(true,false);
        yield return new WaitForSeconds(1f);
        pnlFinish.GetComponent<CanvasGroup>().DOFade(1, 2f);
        yield return new WaitForSeconds(1f);
        imgWin.GetComponent<CanvasGroup>().DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        imgWin.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        imgWin.SetActive(false);
        yield return new WaitForSeconds(1f);        
        FbtnMainMenu.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        FbtnQuit.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        GameObject.FindWithTag("Player").gameObject.SetActive(false);
        foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            e.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        Invoke("SetTimeScale", 2f);
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void SetTimeScale()
    {
        Time.timeScale = 0.01f;
    }
}
