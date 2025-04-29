using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    private bool isSceneLoaded = false;

    void Start()
    {
    
        StartCoroutine(LoadMenuScene());
    }

    IEnumerator LoadMenuScene()
    {
        
        AsyncOperation asyncLoadMenu = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        asyncLoadMenu.allowSceneActivation = true;

        while (!asyncLoadMenu.isDone)
        {
            yield return null; 
        }

      
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
      
        AsyncOperation asyncLoadMain = SceneManager.LoadSceneAsync("TickingFighter");
        asyncLoadMain.allowSceneActivation = false;

        
        loadingScreen.SetActive(true);

        
        while (!asyncLoadMain.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoadMain.progress / 0.9f);
            slider.value = progress;

           
            if (asyncLoadMain.progress >= 0.9f && !isSceneLoaded)
            {
                isSceneLoaded = true;

               
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

                
                asyncLoadMain.allowSceneActivation = true;
            }

            yield return null;
        }

       
        loadingScreen.SetActive(false);
    }

    private bool AreMobsReady()
    {
        return MobSpawner.isReady;
    }
}
