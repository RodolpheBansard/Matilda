using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health = 10;
    public Enemy enemy;
    public HornetShoot hornetShoot;
    public GameObject healthBar;
    public Gradient gradientHealthBar;
    public Image fillHealthBar;

    private bool untouchable = false;
    private Slider slider;

    private void Start()
    {
        slider = healthBar.GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;
        fillHealthBar.color = gradientHealthBar.Evaluate(slider.normalizedValue);
        Phase1();
    }      

    private void CheckPhase()
    {
        if(health == 5)
        {
            Phase2();
        }
        else if(health == 2)
        {
            Phase3();
        }
        else if(health == 0)
        {
            Death();
        }
    }

    private void Phase1()
    {
        enemy.SetMoveSpeed(5);
        hornetShoot.SetFireRate(2);
    }

    private void Phase2()
    {
        enemy.SetMoveSpeed(10);
        hornetShoot.SetFireRate(2);
    }

    private void Phase3()
    {
        enemy.SetMoveSpeed(10);
        hornetShoot.SetFireRate(1);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void TakeHit()
    {
        if (!untouchable)
        {
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
