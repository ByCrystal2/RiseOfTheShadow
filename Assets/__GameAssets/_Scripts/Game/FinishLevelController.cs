using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelController : MonoBehaviour
{
    public bool endGame;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();
            PlayerStats ps = col.gameObject.GetComponent<PlayerStats>();
            
            GameData.instance.CompleteLevel();
            GameData.instance.Save(ps.GetSaveName());
            GameData.instance.SetAllAudioSourcesClose();
            GameData.instance.First = false;
            GameData.instance.FinishLevel = true;

            pm.SetJumpForce(0);
            pm.SetMoveSpeed(0);
            col.gameObject.GetComponent<Animator>().enabled = false;          

            GameData.instance.GetFinishLevel();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Level Bitti");
        }
    }
}
