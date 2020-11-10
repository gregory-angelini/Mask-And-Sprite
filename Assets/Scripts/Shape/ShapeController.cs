using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ShapeView shapeView;

    void Awake()
    {
        shapeView = GetComponent<ShapeView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            shapeView.SetRandomColor();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
}
