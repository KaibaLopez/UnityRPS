using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GamePlay_Behaviour : MonoBehaviour {

    public delegate void ToggleActiveButtons();
    public delegate void StartGame();

    public static event ToggleActiveButtons ActiveButtons;
    public static event ToggleActiveButtons DeactiveButtons;
    public static event ToggleActiveButtons DestroyButtons;

    public static event StartGame TimerStarter;


 
    
    // Use this for initialization
    void Start() { 
    }
	// Update is called once per frame
	void Update () {
    }

    

    void EndGame()
    {
        
        StopCoroutine("LoseTime");
        if (DestroyButtons != null)
            DestroyButtons();
        
        Debug.Log("game over reached!");

    }
    void LastChanceChange()
    {
        if (ActiveButtons != null)
        {
            ActiveButtons();
        }
    }
    void OnEnable()
    {
        Button_behaviour.OnClicked += GameStarter;
        Timer_Behaviour.TimeUp += EndGame;
        Timer_Behaviour.OnesecLeft += LastChanceChange;
    }
    void GameStarter()
    {
        //StartCoroutine("LoseTime");
        if (DeactiveButtons != null)
        {
            DeactiveButtons();
        }
        if (TimerStarter != null)
        {
            TimerStarter();
        }

    }

    void OnDisable()
    {
        Button_behaviour.OnClicked -= GameStarter;
        Timer_Behaviour.TimeUp -= EndGame;
        Timer_Behaviour.OnesecLeft -= LastChanceChange;
    }
}
