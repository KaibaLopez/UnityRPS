using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer_Behaviour : MonoBehaviour
{
    public int TimeLeft;
    public Text countdownText;
    public delegate void TimerEvents();
    public static event TimerEvents OnesecLeft;
    public static event TimerEvents TimeUp;
    private bool TimeUpSent= false;
    private bool OneSecSent= false;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("Time Left: " + TimeLeft);
        if (TimeLeft == 1)
        {
            if (OnesecLeft != null && !OneSecSent)
            {
                OnesecLeft();
                OneSecSent = true;
            }
                
            else
                return;
        }
        if (TimeLeft < 0)
        {
            StopCoroutine("Lose Time");
            countdownText.text = "Time's up!";
            if (TimeUp != null && !TimeUpSent)
            {
                TimeUp();
                TimeUpSent = true;
            }
            else
                return;
        }
    }
    void startListener()
    {
        StartCoroutine("LoseTime");
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }
    }
    void OnEnable()
    {
        GamePlay_Behaviour.TimerStarter += startListener;
    }
    void OnDisable()
    {
        GamePlay_Behaviour.TimerStarter -= startListener;
    }
}
