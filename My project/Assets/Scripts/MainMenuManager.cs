using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameManager manager;

    public void OnGameStart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        manager = GameManager.Instance;
        manager.RestartScore();
    }

    public void OnGameStop(){
        Application.Quit();
    }
}
