using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start(){

    }

    void Update(){
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SettingsScene"); 
    }

    public void Load_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
    #else
        Application.Quit();
    #endif 
    }
}
