using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    
    public GameObject fade;

    public void StartCor(int scene)
    {
        StartCoroutine(Cambio(scene));
    }

   public IEnumerator Cambio(int scene)
   {
        fade.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
   }
}
