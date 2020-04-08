using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health = 10;
    public BossMovement boss;
    public GameObject healthBar;
    public Gradient gradientHealthBar;
    public Image fillHealthBar;
    public AudioClip hurtSound;
    public Animator playerAnimator;

    private bool untouchable = false;
    private Slider slider;

    private void Start()
    {
        playerAnimator.SetTrigger("GetDoubleJump");
        playerAnimator.SetBool("hasDoubleJump", true);
        slider = healthBar.GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;
        fillHealthBar.color = gradientHealthBar.Evaluate(slider.normalizedValue);
    }      

    private void CheckPhase()
    {
        if(health == 5)
        {
            boss.Transition1();
        }
        else if(health == 2)
        {
            boss.Transition2();
        }
        else if(health == 0)
        {
            boss.Death();
        }
    }

    public void TakeHit()
    {
        if (!untouchable)
        {
            AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
            health--;
            slider.value = health;
            fillHealthBar.color = gradientHealthBar.Evaluate(slider.normalizedValue);
            CheckPhase();
            StartCoroutine(Untouchable());
        }
        
    }

    IEnumerator Untouchable()
    {
        untouchable = true;
        yield return new WaitForSeconds(0.5f);
        untouchable = false;
    }
}
