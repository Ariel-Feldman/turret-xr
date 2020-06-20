using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject ProgressBar;
    public Image barFillImage;

    //
    public bool fadeInEffect;
    public bool fadeOutEffect;

    //
    public bool wipeDownEffect;
    public bool wipeUpEffect;
    
    //
    public bool loadingBarEffect;
    public bool addetiveLoad;

    public GameObject[] objectsToDestroy;
    //
    private Animator fadeAnimator;
    private string _newScene;

    //
    private string str;
    private Scene active_Scene;

    //
    private bool isloading;

    //
    AsyncOperation asyncOperation;

    void Start()
    {
        isloading = false;
        
        if (loadingBarEffect) 
            ProgressBar.SetActive(false);
        
        // Hold active scene
        active_Scene = SceneManager.GetActiveScene(); 
        fadeAnimator = GetComponent<Animator>();

        if (fadeInEffect == true)
        {
            fadeAnimator.Play("ScreenFadeInAnimation");
        }

        else if (wipeDownEffect == true)
        {
            fadeAnimator.Play("ScreenWipeDownAnimation");
        }
    }

    public void LoadScene(string newScene)
    {
        if (!isloading)
        {
            isloading = true;
            _newScene = newScene;
            Debug.Log("start load: " + _newScene);

            if (loadingBarEffect) // In case of Loading Bar
            {
                ProgressBar.SetActive(true);
                StartCoroutine(LoadingBarStart());
                return;
            }
            
            // In case Additive Load W/O Loading Bar
            if (addetiveLoad) 
            {
                if (fadeOutEffect == true)
                {
                    StartCoroutine(AdditiveWithFade());
                    return;
                }

                else
                {
                    foreach (GameObject gameobject in objectsToDestroy)
                    {
                        Destroy(gameobject);
                    }

                    SceneManager.LoadScene(_newScene, LoadSceneMode.Additive);
                }
            }
            //In case Not Additive and not Loading bar
            else 
            {
                StartCoroutine(FireScene());
            }
        }
    }

    private IEnumerator LoadingBarStart()
    {
        if (addetiveLoad) //Incase of additve load- no QA yet
        {
            foreach (GameObject gameobject in objectsToDestroy)
            {
                Destroy(gameobject);
            }

            asyncOperation = SceneManager.LoadSceneAsync(_newScene, LoadSceneMode.Additive);
        }
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync(_newScene);
        }

        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.isDone == false)
        {
            //Debug.Log(slider.value);
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            barFillImage.fillAmount = progress;

            //str = System.Math.Round(progress * 100f, 0).ToString();
            //text.text = str + "%";
            //Debug.Log(str);

            if (progress == 1)
            {
                if (fadeOutEffect == true)
                {
                    fadeAnimator.Play("ScreenFadeOutAnimation");
                }

                if (wipeUpEffect == true)
                {
                    fadeAnimator.Play("ScreenWipeUpAnimation");
                }

                yield return new WaitForSeconds(1);

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator FireScene()
    {
        if (fadeOutEffect == true)
        {
            fadeAnimator.Play("ScreenFadeOutAnimation");
        }

        if (wipeUpEffect == true)
        {
            fadeAnimator.Play("ScreenWipeUpAnimation");
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_newScene);
    }

    private IEnumerator AdditiveWithFade()
    {
        if (fadeOutEffect == true)
        {
            fadeAnimator.Play("ScreenFadeOutAnimation");
        }

        if (wipeUpEffect == true)
        {
            fadeAnimator.Play("ScreenWipeUpAnimation");
        }

        yield return new WaitForSeconds(1);

        foreach (GameObject gameobject in objectsToDestroy)
        {
            Destroy(gameobject);
        }

        SceneManager.LoadScene(_newScene, LoadSceneMode.Additive);
    }
}

