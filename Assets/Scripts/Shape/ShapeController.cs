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

    public void Stop()
    {
        iTween.Stop(gameObject);
    }
    public void MoveTo(Vector3 target, float duration, bool isLocal = true)
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", target.x, "y", target.y, "islocal", isLocal, "easeType", "easeOutCirc", "time", duration));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("OnPointerClick");
            shapeView.SetRandomColor();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
    }
}
