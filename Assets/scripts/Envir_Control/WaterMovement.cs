using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterMovement : MonoBehaviour
{

    public float size = 1f;
    public int GridSize = 16;

    private MeshFilter filter;


    //; Start is called before the first frame update
    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();


    }

    private Mesh GenerateMesh()
    {
        Mesh m = new Mesh();

        var verticies = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for (int x = 0; x < GridSize + 1; x++)

        {
            for (int y = 0; y < GridSize + 1; y++)
            {
                verticies.Add(new Vector3(-size * 0.5f + size * (x / ((float)GridSize)), 0, -size * 0.5f + size * (y / ((float)GridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)GridSize, y / (float)GridSize));
            }
        }

        var triangles = new List<int>();
        var vertcount = GridSize + 1;
        for (int i = 0; i < vertcount * vertcount - vertcount; i++)
        {
            if ((i + 1) % vertcount == 0)
            {
                continue;
            }
            triangles.AddRange(new List<int>()
          {
              i+1+vertcount, i+vertcount, i,
              i, i+1, i+vertcount+1

          });

        }

        m.SetVertices(verticies);
        m.SetNormals(normals);
        m.SetUVs(0, uvs);
        m.SetTriangles(triangles, 0);

        return m;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
