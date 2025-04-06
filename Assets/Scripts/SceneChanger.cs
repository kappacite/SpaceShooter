using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadNextScene", 5f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("MainScene"); // Remplace "GameScene" par le nom de ta prochaine scène
    }
}
