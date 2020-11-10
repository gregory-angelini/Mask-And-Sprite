using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ShapeView shapeView;
   
    float clicked = 0;
    float clickTime = 0;
    float clickDelay = 0.5f;

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
        clicked++;

        if (clicked == 1) 
            clickTime = Time.time;

        if (clicked > 1 && Time.time - clickTime < clickDelay)
        {
            clicked = 0;
            clickTime = 0;

            Debug.Log("OnPointerClick");
            shapeView.SetRandomColor();
        }
        else if (clicked > 2 || Time.time - clickTime > clickDelay) 
            clicked = 0;
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
