using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);

        
        UnloadPreviousScene();
    }

    
    private void UnloadPreviousScene()
    {
      
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(currentSceneName);
    }
}
