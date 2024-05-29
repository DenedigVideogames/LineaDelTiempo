using UnityEngine;
using UnityEngine.UI;

public class MakeBig : MonoBehaviour
{
    public GameObject targetObject;
    private Vector3 newSize = new Vector3(1.5f, 1.5f, 1.5f);  

    private Vector3 originalSize;  
    private bool isBig = false;  

    void Start()
    {
        if (targetObject != null)
        {
            originalSize = targetObject.transform.localScale;
        }
    }

    public void ToggleObjectSize()
    {
        if (targetObject != null)
        {
            if (isBig)
            {
                targetObject.transform.localScale = originalSize;
            }
            else
            {
                targetObject.transform.localScale = newSize;
            }

            isBig = !isBig;
        }
    }
}
