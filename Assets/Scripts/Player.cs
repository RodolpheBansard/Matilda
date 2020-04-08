using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator animator;
    public bool untouchable = false;
    
    

    private void Update()
    {
        if(health == 0)
        {
            animator.SetBool("isAlive", false);
            GetComponent<PlayerMovement>().SetIsAlive(false);
            untouchable = true;
            FindObjectOfType<Scene>().ReloadScene();
        }
        
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                    hearts[i].sprite = fullHeart;
                else
                    hearts[i].sprite = emptyHeart;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeHit(collision);            
    }

    public void TakeHit(Collider2D collision)
    {
        if (!untouchable)
        {
            
            if (collision.GetComponent<Boss>() != null || collision.GetComponent<Enemy>() != null || collision.GetComponent<Player>() != null)
            {
                health--;
                GetComponent<PlayerMovement>().Knockback();
                StartCoroutine(Untouchable());
            }
            
            
        }
    }

    public void Healing()
    {
        this.health = 3;
    }

    IEnumerator Untouchable()
    {
        untouchable = true;
        yield return new WaitForSeconds(0.5f);
        untouchable = false;
    }

    public void SetUntouchable(bool value)
    {
        untouchable = value;
    }

}
