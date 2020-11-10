using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeView : MonoBehaviour
{
    MeshRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void SetRandomColor()
    {
        renderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
