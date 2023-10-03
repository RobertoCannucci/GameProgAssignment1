using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject Player;
    public GameObject particleSystemToSpawn;
    public Text scoreText;
    public Text finalScoreText;
    public int pointsValue = 0;
    public string points;
    public static int previouseScore = 0;
    public GameObject[] powerUps;
    public GameObject[] coins;
    public int arrayCountC;
    public int arrayCountP;

    private void Awake(){
        Instance = this;
        DontDestroyOnLoad(Instance);
        //previouseScore = 0;
        powerUps = new GameObject[10];
        coins = new GameObject[20];
        arrayCountP = 0;
        arrayCountC = 0;
        if (SceneManager.GetActiveScene().buildIndex == 4){
            points = "" + previouseScore;
            finalScoreText.text = points;
        }
    }

    public void AddPowerUp(GameObject powerUp){
        powerUps[arrayCountP++] = powerUp;
    }

    public void AddCoins(GameObject coin){
        coins[arrayCountC++] = coin;
    }

    public void reActivatePowerUps(){
        for (int i = 0; i < powerUps.Length; i++){
            if (powerUps[i] != null){
                powerUps[i].SetActive(true);
            }
        }
        for (int i = 0; i < coins.Length; i++){
            if (coins[i] != null){
                coins[i].SetActive(true);
            }
        }
        powerUps = new GameObject[10];
        coins = new GameObject[20];
        arrayCountP = 0;
        arrayCountC = 0;
    }

    public void setScore(){
        Debug.Log(pointsValue);
        previouseScore = pointsValue;       
    }

    public void resetScore(){
        pointsValue = previouseScore;
        points = "" + pointsValue;
        scoreText.text = points;
    }

    public void IncrementScore(){
        pointsValue += 50;
        points = "" + pointsValue;
        scoreText.text = points;
    }

    public void RestartScore(){
        previouseScore = 0;
        pointsValue = 0;
    }

    public void explode(){
        
        GameObject particleSystem = Instantiate(particleSystemToSpawn, Player.transform.position + new Vector3(0, 2, 0), Player.transform.rotation);
        Destroy(particleSystem, 3.0F);
    }


}
