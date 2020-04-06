using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerTrigger : MonoBehaviour
{
    public HornetShoot hornet;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<Player>() != null && hornet !=null)
        {
            hornet.ShootPlayer(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Player>() != null && hornet != null)
        {
            hornet.ShootPlayer(false);
        }
    }
}
