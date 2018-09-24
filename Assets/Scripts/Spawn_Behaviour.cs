using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn_Behaviour : NetworkBehaviour {
    public GameObject playerPrefab;
    public GameObject canvasPrefab;

    GameObject canvasObject;
    public GameObject[] buttonsPrefab;
    public Transform[] spawn_P = new Transform[3];
    // Use this for initialization
    void Start () {
        if (!isLocalPlayer){
            Debug.Log("We open this!");
            return;
        }
        canvasObject = Instantiate(canvasPrefab);
        for (int i = 0; i < 3; i++)
        {
            GameObject Go = Instantiate(buttonsPrefab[i], spawn_P[i].position, Quaternion.identity);
            Go.GetComponent<Button_behaviour>().setSel_val(i);
        }
        CmdSpawn();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        
    }
    [Command]
    void CmdSpawn()
    {
        var screenPos = Camera.main.ScreenToViewportPoint(new Vector3(0, 0, 0));
        screenPos.x = 1.0f;
        screenPos.y = 0.8f;
        var realPos = Camera.main.ViewportToWorldPoint(screenPos);
        realPos.z = 0;

        GameObject go = Instantiate(playerPrefab, screenPos, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
    void OnDisable()
    {
        Destroy(canvasObject);
    }
}
