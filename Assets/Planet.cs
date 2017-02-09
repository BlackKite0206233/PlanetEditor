using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Planet : MonoBehaviour {

    public float Radious;
    public int   Resoluation;
    
    const string MATERIAL_NAME      = "Standard";
    const string MESH_NAME          = "PlanetMesh";
    const string OBJ_NAME           = "planet";
    const float  DEFLAUT_RADIUOS    = 10;
    const int    DEFLAUT_RESOLUTION = 3;
    const int    THETA              = 360;
    const int    METALLIC           = 1;
    const int    GLOSSINESS         = 0;
    const bool   CREATE_AT_ORIGIN   = true;

    private enum AnchorPoint
    {
        TopLeft,
        TopHalf,
        TopRight,
        RightHalf,
        BottomRight,
        BottomHalf,
        BottomLeft,
        LeftHalf,
        Center
    }

    private struct TriangleIndices
    {
        public int v1;
        public int v2;
        public int v3;

        public TriangleIndices(int v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }

    static private Camera      cam;
    static private Camera      lastUsedCam;
    static private float       preRadious;
    static private int         preResoluation;
    static private AnchorPoint anchor = AnchorPoint.Center;

    void Start () {
        preRadious     = DEFLAUT_RADIUOS;
        preResoluation = DEFLAUT_RESOLUTION;
        
        Mesh         mesh = new Mesh();
        GameObject   obj  = GameObject.Find(OBJ_NAME);
        MeshRenderer mr   = obj.AddComponent<MeshRenderer>();
        MeshFilter   mf   = obj.AddComponent<MeshFilter>();

        Radious     = DEFLAUT_RADIUOS;
        Resoluation = DEFLAUT_RESOLUTION;

        mr.material = new Material(Shader.Find(MATERIAL_NAME));
        mr.material.SetInt("_Metallic", METALLIC);
        mr.material.SetInt("_Glossiness", GLOSSINESS);

        cam = Camera.current;

        if (!cam)
            cam = lastUsedCam;
        else
            lastUsedCam = cam;

        if (!CREATE_AT_ORIGIN && cam)
            obj.transform.position = cam.transform.position + cam.transform.forward * 5.0f;
        else
            obj.transform.position = Vector3.zero;

        //createMesh(mesh, mf, this);
        createIcoSphere(mesh, mf, this);

            obj.AddComponent<MasterHand>();
    }
	
	void Update () {
        GameObject obj = GameObject.Find(OBJ_NAME);

        float radious     = Radious;
        int   resoluation = Resoluation;

        if(radious == 0 || resoluation == 0 || Mathf.Abs(resoluation) > 6)
        {
            Radious     = preRadious;
            Resoluation = preResoluation;
        }
        else if (radious != preRadious || resoluation != preResoluation)
        {
            Mesh       mesh = new Mesh();
            MeshFilter mf   = obj.GetComponent<MeshFilter>();

            Radious     = radious;
            Resoluation = resoluation;

            //createMesh(mesh, mf, this);
            createIcoSphere(mesh, mf, this);
        }
    }

    void createIcoSphere(Mesh mesh, MeshFilter mf, Planet p)
    {
        mesh.name = MESH_NAME;
        mf.mesh   = mesh;
        mesh.Clear();

        float radius = Mathf.Abs(p.Radious);

        List<Vector3>         vertList              = new List<Vector3>();
        Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();

        // create 12 vertices of a icosahedron
        float t = (1f + Mathf.Sqrt(5f)) / 2f;

        vertList.Add(new Vector3(-1f,  t, 0f).normalized * radius);
        vertList.Add(new Vector3( 1f,  t, 0f).normalized * radius);
        vertList.Add(new Vector3(-1f, -t, 0f).normalized * radius);
        vertList.Add(new Vector3( 1f, -t, 0f).normalized * radius);

        vertList.Add(new Vector3(0f, -1f,  t).normalized * radius);
        vertList.Add(new Vector3(0f,  1f,  t).normalized * radius);
        vertList.Add(new Vector3(0f, -1f, -t).normalized * radius);
        vertList.Add(new Vector3(0f,  1f, -t).normalized * radius);

        vertList.Add(new Vector3( t, 0f, -1f).normalized * radius);
        vertList.Add(new Vector3( t, 0f,  1f).normalized * radius);
        vertList.Add(new Vector3(-t, 0f, -1f).normalized * radius);
        vertList.Add(new Vector3(-t, 0f,  1f).normalized * radius);


        // create 20 triangles of the icosahedron
        List<TriangleIndices> faces = new List<TriangleIndices>();

        // 5 faces around point 0
        faces.Add(new TriangleIndices(0, 11,  5));
        faces.Add(new TriangleIndices(0,  5,  1));
        faces.Add(new TriangleIndices(0,  1,  7));
        faces.Add(new TriangleIndices(0,  7, 10));
        faces.Add(new TriangleIndices(0, 10, 11));

        // 5 adjacent faces
        faces.Add(new TriangleIndices( 1,  5, 9));
        faces.Add(new TriangleIndices( 5, 11, 4));
        faces.Add(new TriangleIndices(11, 10, 2));
        faces.Add(new TriangleIndices(10,  7, 6));
        faces.Add(new TriangleIndices( 7,  1, 8));

        // 5 faces around point 3
        faces.Add(new TriangleIndices(3, 9, 4));
        faces.Add(new TriangleIndices(3, 4, 2));
        faces.Add(new TriangleIndices(3, 2, 6));
        faces.Add(new TriangleIndices(3, 6, 8));
        faces.Add(new TriangleIndices(3, 8, 9));

        // 5 adjacent faces
        faces.Add(new TriangleIndices(4, 9,  5));
        faces.Add(new TriangleIndices(2, 4, 11));
        faces.Add(new TriangleIndices(6, 2, 10));
        faces.Add(new TriangleIndices(8, 6,  7));
        faces.Add(new TriangleIndices(9, 8,  1));


        // refine triangles
        for (int i = 0; i < Resoluation; i++)
        {
            List<TriangleIndices> faces2 = new List<TriangleIndices>();
            foreach (var tri in faces)
            {
                // replace triangle by 4 triangles
                int a = getMiddlePoint(tri.v1, tri.v2, ref vertList, ref middlePointIndexCache, radius);
                int b = getMiddlePoint(tri.v2, tri.v3, ref vertList, ref middlePointIndexCache, radius);
                int c = getMiddlePoint(tri.v3, tri.v1, ref vertList, ref middlePointIndexCache, radius);

                faces2.Add(new TriangleIndices(tri.v1, a, c));
                faces2.Add(new TriangleIndices(tri.v2, b, a));
                faces2.Add(new TriangleIndices(tri.v3, c, b));
                faces2.Add(new TriangleIndices(     a, b, c));
            }
            faces = faces2;
        }

        mesh.vertices = vertList.ToArray();

        List<int> triList = new List<int>();
        for (int i = 0; i < faces.Count; i++)
        {
            triList.Add(faces[i].v1);
            triList.Add(faces[i].v2);
            triList.Add(faces[i].v3);
        }
        mesh.triangles = triList.ToArray();

        var nVertices = mesh.vertices;
        Vector2[] UVs = new Vector2[nVertices.Length];

        for (var i = 0; i < nVertices.Length; i++)
        {
            var unitVector = nVertices[i].normalized;
            Vector2 ICOuv  = new Vector2(0, 0);

            ICOuv.x = (Mathf.Atan2(unitVector.x, unitVector.z) + Mathf.PI) / Mathf.PI / 2;
            ICOuv.y = (Mathf.Acos(unitVector.y) + Mathf.PI) / Mathf.PI - 1;
            UVs[i]  = new Vector2(ICOuv.x, ICOuv.y);
        }

        mesh.uv = UVs;

        Vector3[] normales = new Vector3[vertList.Count];
        for (int i = 0; i < normales.Length; i++)
            normales[i] = vertList[i].normalized;

        mesh.normals = normales;

        mesh.RecalculateBounds();
        MeshUtility.Optimize(mesh);

        preRadious     = p.Radious;
        preResoluation = p.Resoluation;

    }

    private static int getMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius)
    {
        // first check if we have it already
        bool firstIsSmaller = p1 < p2;
        long smallerIndex   = firstIsSmaller ? p1 : p2;
        long greaterIndex   = firstIsSmaller ? p2 : p1;
        long key            = (smallerIndex << 32) + greaterIndex;

        int ret;
        if (cache.TryGetValue(key, out ret))
        {
            return ret;
        }

        // not in cache, calculate it
        Vector3 point1 = vertices[p1];
        Vector3 point2 = vertices[p2];
        Vector3 middle = new Vector3
        (
            (point1.x + point2.x) / 2f,
            (point1.y + point2.y) / 2f,
            (point1.z + point2.z) / 2f
        );

        // add vertex makes sure point is on unit sphere
        int i = vertices.Count;
        vertices.Add(middle.normalized * radius);

        // store it, return index
        cache.Add(key, i);

        return i;
    }

    void createMesh(Mesh mesh, MeshFilter mf, Planet p)
    {
        mesh.name = MESH_NAME;
        mf.mesh   = mesh;
        mesh.Clear();

        float radius = Mathf.Abs(p.Radious);

        int nbLong = THETA / Mathf.Abs(p.Resoluation); // Longitude |||
        int nbLat  = THETA / Mathf.Abs(p.Resoluation); // Latitude ---

        #region Vertices
        Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];

        float _pi  = Mathf.PI;
        float _2pi = _pi * 2f;

        vertices[0] = Vector3.up * radius;
        for (int lat = 0; lat < nbLat; lat++)
        {
            float a1   = _pi * (float)(lat + 1) / (nbLat + 1);
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= nbLong; lon++)
            {
                float a2   = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
            }
        }
        vertices[vertices.Length - 1] = Vector3.up * -radius;
        #endregion

        #region Normales		
        Vector3[] normales = new Vector3[vertices.Length];
        for (int n = 0; n < vertices.Length; n++)
            normales[n] = vertices[n].normalized;
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        uvs[0] = Vector2.up;
        uvs[uvs.Length - 1] = Vector2.zero;
        for (int lat = 0; lat < nbLat; lat++)
            for (int lon = 0; lon <= nbLong; lon++)
                uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
        #endregion

        #region Triangles
        int nbFaces     = vertices.Length;
        int nbTriangles = nbFaces * 2;
        int nbIndexes   = nbTriangles * 3;
        int[] triangles = new int[nbIndexes];

        //Top Cap
        int i = 0;
        for (int lon = 0; lon < nbLong; lon++)
        {
            triangles[i++] = lon + 2;
            triangles[i++] = lon + 1;
            triangles[i++] = 0;
        }

        //Middle
        for (int lat = 0; lat < nbLat - 1; lat++)
        {
            for (int lon = 0; lon < nbLong; lon++)
            {
                int current = lon + lat * (nbLong + 1) + 1;
                int next = current + nbLong + 1;

                triangles[i++] = current;
                triangles[i++] = current + 1;
                triangles[i++] = next + 1;

                triangles[i++] = current;
                triangles[i++] = next + 1;
                triangles[i++] = next;
            }
        }

        //Bottom Cap
        for (int lon = 0; lon < nbLong; lon++)
        {
            triangles[i++] = vertices.Length - 1;
            triangles[i++] = vertices.Length - (lon + 2) - 1;
            triangles[i++] = vertices.Length - (lon + 1) - 1;
        }
        #endregion

        mesh.vertices  = vertices;
        mesh.normals   = normales;
        mesh.uv        = uvs;
        mesh.triangles = triangles;

        preRadious     = p.Radious;
        preResoluation = p.Resoluation;
    }
}
