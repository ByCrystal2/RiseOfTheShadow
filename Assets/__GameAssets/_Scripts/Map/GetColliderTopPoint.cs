using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColliderTopPoint : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        
    }

    public float GetObstraclesColliderTopPoint()
    {
        if (boxCollider == null)
        {
            Debug.LogWarning("BoxCollider component not found.");
            return 0;
        }

        
        Vector3 colliderSize = boxCollider.size; // Collider'ýn boyutlarý

        // Collider'ýn üst noktasýný dünya koordinatlarýnda hesaplayýn
        Vector3 colliderTopPoint = transform.position + new Vector3(0f, colliderSize.y * 0.5f, 0f);
        Debug.Log("Collider Top Point: " + colliderTopPoint);
        return colliderTopPoint.y; 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player")) 
        {
            float topPoint = GetObstraclesColliderTopPoint();
            if (topPoint <= 10)
            {
                topPoint = 5;
            }
            PlayerMotor pMtr = col.gameObject.GetComponent<PlayerMotor>();
            PlayerMovement pMove = col.gameObject.GetComponent<PlayerMovement>();
            PlayerStats ps = col.gameObject.GetComponent<PlayerStats>();
            PlayerSoundManager pSound = col.gameObject.GetComponent<PlayerSoundManager>();

            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            ps.AddorReductionCurrentHealth(10, false);
            pMove.Jump(topPoint);
            pSound.GetPlayerAudio().PlayOneShot(pSound.GetPlayerDamageClip());
            StartCoroutine(pMtr.SetMaterial(false,topPoint / 4));
            pMove.SetIsGround(true);
        }
    }
}
