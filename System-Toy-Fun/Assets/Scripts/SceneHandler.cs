using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    int currentSceneIndex;

    void ReloadGameScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void QuitGameOnCommand()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
            //Application.Quit();
        }
    }

    void RestartSceneOnCommand()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ReloadGameScene();   
        }
    }
    void Start()
    {   
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {

        QuitGameOnCommand();
        RestartSceneOnCommand();
    }
}
