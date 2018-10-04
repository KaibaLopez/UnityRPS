using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Network_Behaviour : NetworkBehaviour
{
    private GameObject[] buttons = new GameObject[3];
    public int CurrentSel = 0;
    Camera cam;
    // Use this for initialization
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Options");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SelectionChanged(int sel)
    {
        if (hasAuthority)
        {
            CmdUpdateSprite(sel);
            CurrentSel = sel;
        }
    }

    [Command]
    void CmdUpdateSprite(int val)
    {
        //I am on server
        gameObject.GetComponentInParent<PlayerScript>().Selection(val);


        //tell the clients the sprite this player has!
        RpcUpdateSprite(val);
    }
    [ClientRpc]
    void RpcUpdateSprite(int val)
    {

        if (hasAuthority)
        {
            //if we are here, that means that this is MY client so I already know my selection no point in resetting.
            //Selection(val);
            gameObject.name = "Player [" + CurrentSel + "]";
            gameObject.GetComponentInParent<PlayerScript>().Selection(CurrentSel);
            transform.position = new Vector3(cam.transform.position.x - 2.5f, transform.position.y, transform.position.z);
            return;
        }
        //This is the other client's player, so I need the server to tell me what the sprite should be.
        gameObject.name = "Player [" + val + "]";
        gameObject.GetComponentInParent<PlayerScript>().Selection(val);
        transform.position = new Vector3(cam.transform.position.x + 4f, transform.position.y, transform.position.z);
    }
}
//    void OnEnable()
//    {
//        GamePlay_Behaviour.ActiveButtons += ToggleActive;
//        GamePlay_Behaviour.DeactiveButtons += ToggleDeActive;
//        GamePlay_Behaviour.DestroyButtons += GameOver;
//    }
//    void ToggleActive()
//    {
//        foreach (var currBut in buttons)
//        {
//            if (!currBut.activeSelf)
//                currBut.SetActive(true);
//        }
//    }
//    void ToggleDeActive()
//    {
//        foreach (var currBut in buttons)
//        {
//            if (currBut.activeSelf)
//                currBut.SetActive(false);
//        }
//    }
//    void GameOver()
//    {
//        foreach (var currBut in buttons)
//        {
//            Destroy(currBut);
//        }
//    }
//    void OnDisable()
//    {
//        GamePlay_Behaviour.ActiveButtons += ToggleActive;
//        GamePlay_Behaviour.DeactiveButtons += ToggleDeActive;
//        GamePlay_Behaviour.DestroyButtons += GameOver;
//        GameOver();
//    }
//}
