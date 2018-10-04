using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GamePlay_Behaviour : NetworkBehaviour {

    public delegate void ToggleActiveButtons();
    public delegate void StartGame();

    public static event ToggleActiveButtons ActiveButtons;
    public static event ToggleActiveButtons DeactiveButtons;
    public static event ToggleActiveButtons DestroyButtons;

    public static event StartGame TimerStarter;

    [SyncVar(hook = "OnPlayerNameChanged")]
    public int PlayerStatus = -1;

    void Start() {
        if (!isLocalPlayer)
            return;
        Debug.Log("my address is: " + Network.player.ipAddress);
    }
	void Update () {
    }
    /// <summary>
    /// Cheap Way of doing the change, Command cannot be called from button behavior
    /// </summary>
    /// <param name="val"></param>
    public void ChangingValue(int val)
    {
        if (!isLocalPlayer)
            return;

        //RpcChangePlayerSel(val);
        CmdChangePlayerSelection(val);
    }

    /// <summary>
    /// Change Selection value through the server.
    /// </summary>
    /// <param name="curSel"></param>
    [Command]
    void CmdChangePlayerSelection(int curSel)
    {
        //Mid-point before we update the clients of any change that has happened, Server can review it and apply or rollback the change.
        PlayerStatus = curSel;
    }
    /// <summary>
    /// We change the value and inform ALL  the clients that it has been changed.
    /// </summary>
    /// <param name="curSel"></param>
    void OnPlayerNameChanged(int curSel)
    {
        
        Debug.Log("Old select is going to be"+" New sel is: "+curSel);

        // WARNING:  If you use a hook on a SyncVar, then our local value does NOT get automatically updated,
        // so we explicitly do it here.
        PlayerStatus = curSel;

        //I need to make sure the player object knows what to print.
        //But I need only MY (client) player.
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //foreach (GameObject Go in players)
        //    Go.GetComponent<PlayerScript>().SelectionChanged(curSel);

        gameObject.name = "Gameplay_Manager [" + curSel + "]";

    }

    /// <summary>
    /// RPCs are special functions that only get executed on the clients.
    /// </summary>
    [ClientRpc]
    void RpcChangePlayerSel(int curSel)
    {
        //Debug.Log("asked to change the player selection on a particular player connection Object" + curSel);

    }

    //Events that are triggered by buttons.

    void OnEnable()
    {
        Button_behaviour.OnClicked += GameStarter;
        Timer_Behaviour.TimeUp += EndGame;
        Timer_Behaviour.OnesecLeft += LastChanceChange;
    }
    void GameStarter()
    {
        if (DeactiveButtons != null)
        {
            DeactiveButtons();
        }
        if (TimerStarter != null)
        {
            TimerStarter();
        }

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
    void OnDisable()
    {
        Button_behaviour.OnClicked -= GameStarter;
        Timer_Behaviour.TimeUp -= EndGame;
        Timer_Behaviour.OnesecLeft -= LastChanceChange;
    }
}
