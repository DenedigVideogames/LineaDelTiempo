using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float delay;
    public float limitTime;
    public bool _timmerIsRunning = false;
    public TextMeshProUGUI timer;

    void Start()
    {
        Globals.Score = 100;
        _timmerIsRunning = true;
    }

    void Update()
    {
        DisplayTime(delay);
        if (_timmerIsRunning == true)
        {
            if (delay < limitTime)
            {
                delay += Time.deltaTime;
                Globals.Score -= Time.deltaTime;
            }
            else 
            {
                _timmerIsRunning = false;
                delay = limitTime;
            }
        }
    }

    private void DisplayTime(float timeToDisplay) 
    {
        timeToDisplay -= 1;
        float minutes = Mathf.FloorToInt(delay / 60);
        float seconds = Mathf.FloorToInt(delay % 60);
        if (delay < 0) 
        { 
            timer.text = ("00:00"); 
        }
        else
        {
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
