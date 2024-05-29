using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidaciónFicha : MonoBehaviour
{
    [SerializeField] private int _correctIndex;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<FichaData>().Year == _correctIndex) {
            //Globals.Score +=1;
        }
    }
}
