using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

    private SpriteRenderer spriteR;
    

    public Sprite[] sprites;
    public Transform[] spawn_P = new Transform[3];
    public GameObject[] buttonsPrefab;
    [SyncVar] public int CurrentSel = 0;

    
    //Just for clarification on meanings.
    enum Choice { Rock = 0, Paper = 1, Scissors = 2 }
    private float sqrRadius;

    void Start()
    {      
        spriteR = gameObject.GetComponent<SpriteRenderer>();
       
        //for (int i = 0; i < 3; i++)
        //{
        //   GameObject Go = Instantiate(buttonsPrefab[i], spawn_P[i].position, Quaternion.identity);
        //   Go.GetComponent<Button_behaviour>().setSel_val(i);
        //}

    }

    /// <summary>
    /// public method for the buttons to call
    /// </summary>
    /// <param name="sel"></param>
    public void Selection( int sel)
    {

        if (hasAuthority == false)
        {
            Debug.Log("We don't have control!");
            return;
        }
        if (sel == (int)Choice.Rock){
            Change(sprites[0]);
            CurrentSel = 0;
        }else if (sel == (int)Choice.Paper)
        {
            Change(sprites[1]);
            CurrentSel = 1;
        }
        else
        {
            Change(sprites[2]);
            CurrentSel = 2;
        }
    }

    /// <summary>
    /// Change in of the sprite
    /// </summary>
    /// <param name="differentSprite"></param>
    private void Change(Sprite differentSprite)
    {
        
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = differentSprite;
    }

    // Update is called once per frame
    void Update () {
        
    }



    float distance_2d(Vector3 a, Vector3 b)
    {
        Vector2 dist =new Vector2((a.x - b.x), (a.y-b.y));
        float sqrDist = (dist.x * dist.x) + (dist.y * dist.y);
        return sqrDist;
    }
}
