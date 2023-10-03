using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBehaviour : MonoBehaviour
{
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
    }

    public void OnTriggerEnter(Collider other){
        
        if (other.gameObject.tag == "Player"){
            manager.setScore();
            //manager.resetScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
    }


}
