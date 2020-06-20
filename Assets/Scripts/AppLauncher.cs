using UnityEngine;
using UnityEngine.SceneManagement;

// This shall be the first scene in the build,
// act as a buffer between Windows to our scene
public class AppLauncher : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        Debug.Log("Unity Editor State");
        SceneManager.LoadScene(1); // this is straight to game mode
#endif
#if !UNITY_EDITOR
        Debug.Log("Unity Editor State");
        SceneManager.LoadScene(1); // this is straight to game mode
#endif
    }
}



