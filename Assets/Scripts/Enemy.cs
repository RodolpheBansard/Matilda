using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Transform> waypoints;
    public GameObject particlePrefab;
    public float moveSpeed = 3;
    public Item item = null;
    public AudioClip deathSound;

    private int currentIndex = 0;
    private bool launch = false;


    void Start()
    {
        
        transform.position = waypoints[currentIndex].position;
        currentIndex++;
        UpdateScale();
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    void Update()
    {        
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIndex].position, moveSpeed * Time.deltaTime);
        if (transform.position == waypoints[currentIndex].position)
        {
            currentIndex++;
            
            if (currentIndex == waypoints.Count)
            {
                currentIndex = 0;
            }
            UpdateScale();
        }    
    }

    private void UpdateScale()
    {
        if(GetComponent<HornetShoot>() == null)
        {
            if (transform.position.x - waypoints[currentIndex].position.x > 0.1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            else if (transform.position.x - waypoints[currentIndex].position.x < -0.1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        } 
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position + new Vector3(0,0,5),1);
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        if (item != null)
        {
            item.GuardianKilled();   
            
        }
    }
}
