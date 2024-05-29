using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Disable : MonoBehaviour
{
    public float time;
    public float limit;
    public bool _timmerIsRunning = false;
    public TextMeshProUGUI contador;

    public GameObject parent;
    public void Start(){
        
        _timmerIsRunning = true;
        StartCoroutine(Off());
    }

    void Update()
    {
        
        if (_timmerIsRunning == true){
        if (time > limit)
        {
            time -= Time.deltaTime;
            Display(time);
        }
        else {
            _timmerIsRunning = false;
            time = limit;
        }
        }
        
    }

    private void Display(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time%60);
        contador.text = string.Format(seconds.ToString());
        if (seconds <= 0)
        {
            contador.text = "";
        }
    }

    public IEnumerator Off(){
        yield return new WaitForSeconds(3);
        parent.SetActive(false);
    }
}
