using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public List<Enemy> guardians;
    public bool heart = false;
    public bool doubleJump = false;
    public AudioClip powerupSound;
    public Animator playerAnimator;

    private int guardiansLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Destroy(gameObject);

            if (heart)
            {
                collision.GetComponent<Player>().Healing();
                AudioSource.PlayClipAtPoint(powerupSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
            }
            else if (doubleJump)
            {
                collision.GetComponent<PlayerMovement>().canDoubleJump = true;
                AudioSource.PlayClipAtPoint(powerupSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
                playerAnimator.SetTrigger("GetDoubleJump");
                playerAnimator.SetBool("hasDoubleJump", true);
            }
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
        guardiansLeft = guardians.Count;
    }

    public void GuardianKilled()
    {
        guardiansLeft--;
        if(guardiansLeft == 0)
        {
            gameObject.SetActive(true);
        }
    }
}
