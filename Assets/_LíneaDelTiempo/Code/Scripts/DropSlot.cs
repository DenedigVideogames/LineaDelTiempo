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

        if (!item)
        {
            item = DragHandler.objBeingDraged;
            item.transform.SetParent(transform);
            item.transform.position = transform.position;
        }
        // Llamar a la verificación al soltar
        CheckCorrect();
    }

    private void CheckCorrect()
    {
        if (item != null && item.GetComponent<Ficha>().year == year)
        {
            Correct = true;
        }
        else
        {
            Correct = false;
        }
    }

    private void Update()
    {
        // Llamar a la verificación en cada frame
        CheckCorrect();

        // Restante lógica de Update...
        if (item != null && item.transform.parent != transform)
        {
            Debug.Log("Remover");
            item = null;
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
