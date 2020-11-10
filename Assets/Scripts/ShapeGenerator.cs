using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] Material material;
    GameObject shape;
    [SerializeField] float shapeScale = 2f;

    void Awake()
    {
        shape = new GameObject("Shape", 
            typeof(MeshFilter), 
            typeof(MeshRenderer), 
            typeof(PolygonCollider2D),
            typeof(Shape),
            typeof(ShapeView),
            typeof(ShapeController));

        shape.GetComponent<MeshRenderer>().material = material;
        shape.transform.localScale = new Vector3(shapeScale, shapeScale, shapeScale);

        CreateCube();
    }

    public void OnCubeButton()
    {
        CreateCube();
    }
    public void OnHexButton()
    {
        CreateHex();
    }

    void CreateHex()
    {
        shape.GetComponent<MeshFilter>().mesh = GetHexMesh();
        GenerateHexCollider();
    }
    void CreateCube()
    {
        shape.GetComponent<MeshFilter>().mesh = GetCubeMesh();
        GenerateCubeCollider();
    }

    void GenerateCubeCollider()
    {
        PolygonCollider2D collider = shape.GetComponent<PolygonCollider2D>();

        Vector2[] array1 = shape.GetComponent<MeshFilter>().mesh.vertices.ToVector2Array();
        Vector2 v0 = shape.GetComponent<MeshFilter>().mesh.vertices[0].ToVector2();
        Vector2[] array2 = new Vector2[] { v0 };

        Vector2[] points = array1.Concat(array2).ToArray();

        collider.points = points;
    }
    void GenerateHexCollider()
    {
        PolygonCollider2D collider = shape.GetComponent<PolygonCollider2D>();


        Vector2[] array1 = shape.GetComponent<MeshFilter>().mesh.vertices
            .Take(shape.GetComponent<MeshFilter>().mesh.vertices.Count() - 1)
            .ToArray()
            .ToVector2Array();

        Vector2 v0 = shape.GetComponent<MeshFilter>().mesh.vertices[0].ToVector2();
        Vector2[] array2 = new Vector2[] { v0 };

        Vector2[] points = array1.Concat(array2).ToArray();

        collider.points = points;
    }

    Mesh GetHexMesh()
    {
        Vector3 center = Vector3.zero;
        float size = 1;

        Vector3[] vertices = new Vector3[]
        {
            GetHexVertex(center, size, 0),
            GetHexVertex(center, size, 1),
            GetHexVertex(center, size, 2),
            GetHexVertex(center, size, 3),
            GetHexVertex(center, size, 4),
            GetHexVertex(center, size, 5),
            Vector3.zero
        };

        Vector2[] uv = new Vector2[]
        {
            vertices[0],
            vertices[1],
            vertices[2],
            vertices[3],
            vertices[4],
            vertices[5],
            vertices[6]
        };

        int[] triangles = new int[]
        {
            1, 0, 6,
            2, 1, 6,
            3, 2, 6,
            4, 3, 6,
            5, 4, 6,
            0, 5, 6
        };


        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        return mesh;
    }
    Vector3 GetHexVertex(Vector3 center, float size, int i)
    {
        var angle_deg = 60 * i + 30;
        var angle_rad = Mathf.PI / 180 * angle_deg;
        return new Vector3(center.x + size * Mathf.Cos(angle_rad), center.y + size * Mathf.Sin(angle_rad));
    }

    Mesh GetCubeMesh()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1f, 1f),// top left
            new Vector3(1f, 1f),// top right
            new Vector3(1f, -1f),// bottom right
            new Vector3(-1f, -1f)// bottom left
        };

        Vector2[] uv = new Vector2[]
        {
            new Vector2(0, 1),// top left
            new Vector2(1, 1),// top right
            new Vector2(1, 0),// bottom right
            new Vector2(0, 0)// bottom left
        };

        int[] triangles = new int[]
        {
            0, 1, 3,
            3, 1, 2
        };


        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        return mesh;
    }
}
