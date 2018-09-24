using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Network_Behaviour : NetworkBehaviour {
    private GameObject[] buttons = new GameObject[3];
    [SyncVar] public int CurrentSel = 0;
    // Use this for initialization
    void Start () {
        buttons = GameObject.FindGameObjectsWithTag("Options");
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
    void OnEnable()
    {
        GamePlay_Behaviour.ActiveButtons += ToggleActive;
        GamePlay_Behaviour.DeactiveButtons += ToggleDeActive;
        GamePlay_Behaviour.DestroyButtons += GameOver;
    }
    void ToggleActive()
    {
        foreach (var currBut in buttons)
        {
            if (!currBut.activeSelf)
                currBut.SetActive(true);
        }
    }
    void ToggleDeActive()
    {
        foreach (var currBut in buttons)
        {
            if (currBut.activeSelf)
                currBut.SetActive(false);
        }
    }
    void GameOver()
    {
        foreach (var currBut in buttons)
        {
            Destroy(currBut);
        }
    }
    void OnDisable()
    {
        GamePlay_Behaviour.ActiveButtons += ToggleActive;
        GamePlay_Behaviour.DeactiveButtons += ToggleDeActive;
        GamePlay_Behaviour.DestroyButtons += GameOver;
        GameOver();
    }
}
