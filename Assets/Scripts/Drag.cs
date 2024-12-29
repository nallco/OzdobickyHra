using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Image thisImage;
    public Vector3 startPosition;
    void Start()
    {
        thisImage = GetComponent<Image>();
        startPosition = transform.position;
        gameObject.layer = 5;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        thisImage.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        gameObject.layer = 7;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition; 
        gameObject.layer = 5;
        thisImage.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("klik");
    }
}
