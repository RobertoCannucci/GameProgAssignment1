using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedTrapBehaviour : MonoBehaviour
{
    Vector3 ogplayerPosition;
    public GameObject Player;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        ogplayerPosition = Player.transform.position;
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("You died");
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("IN here");
            RestartGame();
            manager.resetScore();
            manager.reActivatePowerUps();
        }
    }

    public void RestartGame()
    {
        // Debug.Log(Player.transform.position);
        // Debug.Log(ogplayerPosition);
        //Player.transform.position = ogplayerPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
