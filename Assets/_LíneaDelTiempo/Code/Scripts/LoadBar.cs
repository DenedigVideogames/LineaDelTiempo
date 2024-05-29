using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadBar : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI leaderBoardText;
    
    // Update is called once per frame
    void Update()
    {
        if (leaderBoardText.text != "")
        {
            gameObject.SetActive(false);
        }
    }
}
