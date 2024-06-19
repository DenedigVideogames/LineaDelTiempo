using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;

    [SerializeField] public TextMeshProUGUI yearLabel;
    public int year;

    public bool Correct = false;

    [SerializeField] public Image image;
    

    private void Start()
    {
        yearLabel.text = year.ToString();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Entro");

        if (!item) ////////////////////////////////////////      AQUI SE CHECA LA VERIFICACION DE LAS CARTAS
        {
            item = DragHandler.objBeingDraged;
            item.transform.SetParent(transform);
            if ( item.GetComponent<Ficha>().year == year){
                Correct = true;
            }
            else {
                Correct = false;
            }
            item.transform.position = transform.position;
        }
        
    }

    private void Update()
    {
        // Si hay un objeto y no está en la ranura actual
        if (item != null && item.transform.parent != transform)
        {
            Debug.Log("Remover");

            // Elimina el objeto de la ranura
            item = null;
            // Establece el objeto como hijo del objeto que está siendo arrastrado
            item = DragHandler.objBeingDraged;
            if (item == null) return;
            item.transform.SetParent(DragHandler.objBeingDraged.transform);
        }
    }

    public void InitializeFicha(GameObject ficha)
    {
        item = ficha;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero; // Ajustar posición local para centrar en el slot
        if (yearLabel != null)
        {
            yearLabel.text = year.ToString();
        }
    }
}
