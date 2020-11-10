using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] Material shapeMaterial;
    GameObject shape;
    GameObject background;
    [SerializeField] float shapeScale = 2f;
    [SerializeField] Material backgroundMaterial;

    [SerializeField] Material shapeMaterialMask;
    [SerializeField] Material backgroundMaterialMask;
    bool isMaskEnabled = false;


    void Awake()
    {
        CreateBackground();

        shape = new GameObject("Shape", 
            typeof(MeshFilter), 
            typeof(MeshRenderer), 
            typeof(PolygonCollider2D),
            typeof(Shape),
            typeof(ShapeView),
            typeof(ShapeController));

        shape.GetComponent<MeshRenderer>().material = shapeMaterial;
        shape.transform.localScale = new Vector3(shapeScale, shapeScale, shapeScale);
        shape.transform.localPosition = new Vector3(0, 0, -1);

        CreateHex();

        shape.GetComponent<ShapeView>().SetRandomColor();
    }

    void CreateBackground()
    {
        background = new GameObject("Background",
            typeof(MeshFilter),
            typeof(MeshRenderer),
            typeof(PolygonCollider2D));

        background.GetComponent<MeshRenderer>().material = backgroundMaterial;

        var scale = background.transform.localScale;
        scale.x = scale.y * Camera.main.aspect;
        background.transform.localScale = scale;

        background.GetComponent<MeshFilter>().mesh = GetBackgroundMesh(
            10, 
            10);

        background.layer = LayerMask.NameToLayer("BACKGROUND");
       
        GenerateSquareCollider(background);
    }

    public void OnMaskButton()
    {
        if (!isMaskEnabled)
        {
            background.GetComponent<MeshRenderer>().material = backgroundMaterialMask;
            shape.GetComponent<MeshRenderer>().material = shapeMaterialMask;

            shape.transform.localPosition = new Vector3(
                shape.transform.localPosition.x, 
                shape.transform.localPosition.y, 
                1);

            shape.GetComponent<ShapeController>().Stop();
            isMaskEnabled = true;
        }
        else
        {
            background.GetComponent<MeshRenderer>().material = backgroundMaterial;
            shape.GetComponent<MeshRenderer>().material = shapeMaterial;

            shape.transform.localPosition = new Vector3(
                shape.transform.localPosition.x, 
                shape.transform.localPosition.y, 
                -1);

            shape.GetComponent<ShapeController>().Stop();
            isMaskEnabled = false;
        }
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
        GenerateHexCollider(shape);
    }
    void CreateCube()
    {
        shape.GetComponent<MeshFilter>().mesh = GetCubeMesh();
        GenerateSquareCollider(shape);
    }

    void GenerateSquareCollider(GameObject go)
    {
        PolygonCollider2D collider = go.GetComponent<PolygonCollider2D>();

        Vector2[] array1 = go.GetComponent<MeshFilter>().mesh.vertices.ToVector2Array();
        Vector2 v0 = go.GetComponent<MeshFilter>().mesh.vertices[0].ToVector2();
        Vector2[] array2 = new Vector2[] { v0 };

        Vector2[] points = array1.Concat(array2).ToArray();

        collider.points = points;
    }
    void GenerateHexCollider(GameObject go)
    {
        PolygonCollider2D collider = go.GetComponent<PolygonCollider2D>();


        Vector2[] array1 = go.GetComponent<MeshFilter>().mesh.vertices
            .Take(go.GetComponent<MeshFilter>().mesh.vertices.Count() - 1)
            .ToArray()
            .ToVector2Array();

        Vector2 v0 = go.GetComponent<MeshFilter>().mesh.vertices[0].ToVector2();
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


    Mesh GetBackgroundMesh(float width, float height)
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-width/2, height/2),// top left
            new Vector3(width/2, height/2),// top right
            new Vector3(width/2, -height/2),// bottom right
            new Vector3(-width/2, -height/2)// bottom left
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(
                Camera.main.ScreenPointToRay(Input.mousePosition), 100f, 1 << LayerMask.NameToLayer("BACKGROUND"));
   
            if (hit.collider)
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, shape.transform.localPosition.z));

                shape.GetComponent<ShapeController>().MoveTo(
                    worldPoint,
                    1);
            }
        }
    }
}
