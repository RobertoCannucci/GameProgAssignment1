using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{

    public GameManager manager;
    // Start is called before the first frame update
    public void OnGameRestart(){
        SceneManager.LoadScene(1);
        manager = GameManager.Instance;
        manager.setScore();
    }
}
