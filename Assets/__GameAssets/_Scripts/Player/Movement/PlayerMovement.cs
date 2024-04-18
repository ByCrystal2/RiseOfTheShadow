using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public bool isGround;
    public float jumpForce = 8;
    public float moveSpeed = 2.0f; // Hareket hýzý
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
       
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Move()
    {
        Vector2 moveDirection = new Vector2(1, 0); // Ýleri doðru yönlendirme
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetJumpForce()
    {
        return jumpForce;
    }
    public bool GetIsGround()
    {
        return isGround;
    }

    public void SetIsGround(bool isGround)
    {
        this.isGround = isGround;
    }

    public void AddorReductionMoveSpeed(float amount, bool add)
    {
        if (add) this.moveSpeed += amount;
        else this.moveSpeed -= amount;        
    }

    public void AddorReductionJumpForce(float amount, bool add)
    {
        if (add) this.jumpForce += amount;
        else this.jumpForce -= amount;
    }

    public void SetJumpForce(float value)
    {
        this.jumpForce = value;
    }

    public void SetMoveSpeed(float value)
    {
        this.moveSpeed = value;
    }





}
