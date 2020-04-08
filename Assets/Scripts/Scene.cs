using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public Animator fadeAnimator;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Developper Screen")
        {
            StartCoroutine(WaitDevelopperScreen());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Main Menu")
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            LoadMenu();
        }
        StartCoroutine(Fade(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void ReloadScene()
    {
        StartCoroutine(Fade(SceneManager.GetActiveScene().buildIndex));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BossKilled()
    {
        StartCoroutine(WaitAndReload());
    }

    IEnumerator WaitDevelopperScreen()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(Fade(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator Fade(int index)
    {
        fadeAnimator.SetBool("isFading", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }

    IEnumerator WaitAndReload()
    {
        yield return new WaitForSeconds(2);
        LoadMenu();
    }
}
