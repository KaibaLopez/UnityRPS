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
        //need to make sure we select Local player ONLY
        GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject Go in players)
            Go.GetComponentInParent<PlayerScript>().Selection(Sel_Val);

        if (OnClicked != null)
            OnClicked();
        Debug.Log("I was clicked! ");
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
