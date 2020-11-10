using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static Vector2[] ToVector2Array(this Vector3[] v3Array)
    {
        return System.Array.ConvertAll<Vector3, Vector2>(v3Array, 
            (v3) =>
            {
                return new Vector2(v3.x, v3.y);
            });
    }

    public static Vector2 ToVector2(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
}
