using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectOfType<MusicPlayer>() != null)
        {
            Destroy(FindObjectOfType<MusicPlayer>().gameObject);
        }        
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType<BossMusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
