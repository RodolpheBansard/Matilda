using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public enum Phase
    {
        entrance,
        phase1,
        transition1,
        phase2,
        transition2,
        phase3,
        death
    }

    public Salve salve;
    public HornetShoot hornet;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Player player;

    public Color colorPhase2;
    public Color colorPhase3;
    public GameObject particlePrefab;
    public AudioClip deathSound;
    public AudioClip transitionSound;    

    public Transform transitionPoint;
    public List<Transform> entrance;
    public List<Transform> phase1Phase2;
    public List<Transform> phase3;

    public float moveSpeed;

    private int currentIndex = 0;
    private List<Transform> currentPath;
    private bool stop = false;
    private bool transitioning = false;
    private Phase phase;
    private int nCoroutine = 0;


    void Start()
    {
        phase = Phase.entrance;
        spriteRenderer.color = colorPhase3;
        moveSpeed = 3;
        currentPath = entrance;
        transform.position = currentPath[currentIndex].position;
        currentIndex++;
    }

    

    void Update()
    {
        
        if (!stop)
        {
            if (transitioning)
            {
                transform.position = Vector2.MoveTowards(transform.position, transitionPoint.position, moveSpeed * Time.deltaTime);
                if(transform.position == transitionPoint.position)
                {
                    if(nCoroutine != 1)
                    {
                        if (phase == Phase.transition1)
                        {
                            animator.SetTrigger("Transition1");
                            StartCoroutine(WaitEndAnimationT1());
                            nCoroutine = 1;
                        }
                        else if (phase == Phase.transition2)
                        {
                            animator.SetTrigger("Transition2");
                            StartCoroutine(WaitEndAnimationT2());
                            nCoroutine = 1;
                        }
                        else if (phase == Phase.death)
                        {
                            animator.SetTrigger("Death");
                            StartCoroutine(WaitDeath());
                            nCoroutine = 1;
                        }
                    }    
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPath[currentIndex].position, moveSpeed * Time.deltaTime);
                if (transform.position == currentPath[currentIndex].position)
                {
                    if (currentPath == entrance)
                    {
                        stop = true;
                        StartCoroutine(Entrance());
                        currentIndex = 0;
                    }
                    currentIndex++;

                    if (currentIndex == currentPath.Count)
                    {
                        currentIndex = 0;
                    }
                }
            }
            
        }


        
    }

    

    public void Phase1()
    {
        phase = Phase.phase1;
        hornet.ShootPlayer(true);
        currentPath = phase1Phase2;
        moveSpeed = 5;
        hornet.SetFireRate(2);

    }


    public void Phase2()
    {
        player.SetUntouchable(false);
        nCoroutine = 0;
        transitioning = false;
        currentIndex = 2;
        hornet.ShootPlayer(true);
        moveSpeed = 10;
        hornet.SetFireRate(2);
    }

    public void Phase3()
    {
        player.SetUntouchable(false);
        nCoroutine = 0;
        hornet.ShootPlayer(true);
        transitioning = false;
        currentPath = phase3;
        moveSpeed = 10;
        hornet.SetFireRate(2);
    }

    public void Transition1()
    {
        player.SetUntouchable(true);
        hornet.ShootPlayer(false);
        phase = Phase.transition1;
        moveSpeed = 20;
        transitioning = true;
    }

    public void Transition2()
    {
        player.SetUntouchable(true);
        hornet.ShootPlayer(false);
        phase = Phase.transition2;
        moveSpeed = 20;
        transitioning = true;
    }

    public void Death()
    {
        player.SetUntouchable(true);
        phase = Phase.death;
        transitioning = true;
    }

    IEnumerator Entrance()
    {
        Phase1();
        yield return new WaitForSeconds(0.5f);
        stop = false;
    }

    IEnumerator WaitEndAnimationT1()
    {
        AudioSource.PlayClipAtPoint(transitionSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
        yield return new WaitForSeconds(2);
        salve.LaunchSalve(2);
        Phase2();
    }

    IEnumerator WaitEndAnimationT2()
    {
        AudioSource.PlayClipAtPoint(transitionSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
        yield return new WaitForSeconds(2);
        salve.LaunchSalve(5);
        Phase3();
    }

    IEnumerator WaitDeath()
    {
        hornet.SetStopShoot();
        yield return new WaitForSeconds(2);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position + new Vector3(0, 0, 5), 1);
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        FindObjectOfType<Scene>().BossKilled();
    }

}
