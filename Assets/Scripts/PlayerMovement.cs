﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController;
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask enemyLayer;

    public AudioClip attackSound;
    public AudioClip doubleJumpSound;
    public AudioClip HurtSound;

    public bool canDoubleJump = false;
    public float attackRange = 0.5f;
    public float runSpeed = 40f;
    public float doubleJumpForce = 20;

    private float horizontalMove = 0;
    private bool jump = false;
    private bool isAlive = true;
    private bool knockback = false;

    private int compteur = 0;
    
    void Update()
    {
        if (isAlive && !knockback)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;



            if (Input.GetButtonDown("Jump"))
            {
                compteur++;
                if (compteur == 2 && canDoubleJump)
                {
                    GetComponent<Rigidbody2D>().Sleep();
                    GetComponent<Rigidbody2D>().WakeUp();
                    GetComponent<Rigidbody2D>().AddForce(transform.up * doubleJumpForce, ForceMode2D.Impulse);
                    AudioSource.PlayClipAtPoint(doubleJumpSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
                }
                else if(compteur == 1 && !GetComponent<CharacterController2D>().isGrounded() && canDoubleJump)
                {
                    GetComponent<Rigidbody2D>().Sleep();
                    GetComponent<Rigidbody2D>().WakeUp();
                    GetComponent<Rigidbody2D>().AddForce(transform.up * doubleJumpForce, ForceMode2D.Impulse);
                    AudioSource.PlayClipAtPoint(doubleJumpSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
                }
                else
                {
                    jump = true;
                    animator.SetBool("Jump", true);
                }
                
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Attack());
            }

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        if (knockback)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 3);
        }

        if (!isAlive)
        {
            horizontalMove = 0;
            animator.SetBool("Jump", false);
            GetComponent<Rigidbody2D>().Sleep();            
            GetComponent<Rigidbody2D>().gravityScale = 30;
            GetComponent<Rigidbody2D>().WakeUp();
        }
    }

    private void FixedUpdate()
    {
        
        characterController.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
            
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
        compteur = 0;
    }

    public void SetIsAlive(bool alive)
    {
        isAlive = alive;
    }

    public void Knockback()
    {
        StartCoroutine(KnockBack());
    }

    IEnumerator KnockBack()
    {
        AudioSource.PlayClipAtPoint(HurtSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
        knockback = true;
        yield return new WaitForSeconds(0.05f);
        knockback = false;
    }

    IEnumerator Attack()
    {
        
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {           
            if (enemy.GetComponent<Boss>() != null)
                enemy.GetComponent<Boss>().TakeHit();
            else
                Destroy(enemy.gameObject, 0.2f);
            
            
        }
        yield return new WaitForSeconds(0.2f);
        AudioSource.PlayClipAtPoint(attackSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
        hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Boss>() != null)
                enemy.GetComponent<Boss>().TakeHit();
            else
            {
                Destroy(enemy.gameObject);
            }
                
        }
    }
}
