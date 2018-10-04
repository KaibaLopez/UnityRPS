using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_behaviour : MonoBehaviour {
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    private int Sel_Val=0;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Go in players)
            Go.GetComponent<Player_Network_Behaviour>().SelectionChanged(Sel_Val);

        if (OnClicked != null)
            OnClicked();

    }

    

    
    public int getSel_val()
    {
        return Sel_Val;
    }
    public void setSel_val(int newVal)
    {
        Sel_Val = newVal;
    }
}
