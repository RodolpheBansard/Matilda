using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetShoot : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform firePoint;
    public Animator animator;

    public float fireRate = 2;
    private bool running=false;
    private IEnumerator shootCoroutine;

    private void Start()
    {
        shootCoroutine = Shoot();
    }

    private void Update()
    {
        if (FindObjectOfType<Player>().transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (FindObjectOfType<Player>().transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetFireRate(float fireRate)
    {
        this.fireRate = fireRate;
    }

    public void ShootPlayer(bool attackPlayer)
    {
        if (attackPlayer)
        {
            if (!running)
            {
                StartCoroutine(shootCoroutine);
                running = true;
            }
            
        }
        else
        {
            StopCoroutine(shootCoroutine);
            running = false;
        }
    }



    IEnumerator Shoot()
    {
        while (true)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.2f);
            Instantiate(dartPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(fireRate);
        }
        
    }
}
