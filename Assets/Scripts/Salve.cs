﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salve : MonoBehaviour
{
    public GameObject dartPrefab;
    public List<Transform> spawnPoints;
    public AudioClip salveSound;

    

    public void LaunchSalve(int repetition)
    {
        StartCoroutine(Wave(repetition));
    }

    IEnumerator Wave(int repetition)
    {
        for(int i = 0; i < repetition; i++)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                AudioSource.PlayClipAtPoint(salveSound, Camera.main.transform.position + new Vector3(0, 0, 10), 1);
                Instantiate(dartPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(0.05f);
            }
        }
        
    }
}
