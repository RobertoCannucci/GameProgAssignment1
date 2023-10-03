using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        manager.resetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
