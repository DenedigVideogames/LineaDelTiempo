using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject objBeingDraged;

    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;
    private Transform itemDraggerParent;

    private void Start() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemDraggerParent = GameObject.FindGameObjectWithTag("ItemDraggerParent").transform;
    }

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound2D("selecCarta");
        Debug.Log("OnBeginDrag");
        objBeingDraged = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(itemDraggerParent);

        canvasGroup.blocksRaycasts = false;

    }
    // Se llama mientras se arrastra el objeto
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound2D("dejarCarta");
        Debug.Log("OnEndDrag");
        objBeingDraged = null;

        canvasGroup.blocksRaycasts = true;
        if (transform.parent == itemDraggerParent)
        {
            // Devuelve el objeto a su posición inicial y su padre original
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }

}
