using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene3 : MonoBehaviour
{
    string objFilePath; // OBJ文件的路径
    string path = "/storage/emulated/0/stairsFullCornerOuter.obj";
    public Material mat;
    public Material mat1;
    private Vector3 lastMousePosition;
    GameObject fin;
   
   

    float distance = 10f; // 相机与物体之间的距离
    float height = 10f; // 相机与物体之间的高度

    public Texture2D normalMap1;

	public TextMeshProUGUI display_user_obj;

	public void Start()
	{
		display_user_obj.text = Scene2.scene2.user_obj;
        LoadOBJFile();
	}

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
                float rotationX = -deltaMousePosition.y * 2.5f * Time.deltaTime;
                float rotationY = deltaMousePosition.x * 2.5f * Time.deltaTime;

                transform.RotateAround(fin.transform.position, Vector3.up, rotationY);
                transform.RotateAround(fin.transform.position, transform.right, rotationX);

                lastMousePosition = Input.mousePosition;
            }

    }

	private void changepath (string temp)
    {
       
        if (temp.Length == 0)
        {
            //Text.text = "please input file";
        }
        objFilePath = "Assets/" + temp + ".obj";
        path = "/storage/emulated/0/" + temp + ".obj";
    }
    private string readfile(string ppath)
    {
        string Texto = "";
        if (File.Exists(ppath))
        {
            Texto = File.ReadAllText(ppath);
            //Debug.Log("File content: " + fileContent);
        }
        else
        {

            Debug.LogError("File not found: " + path);
        }
        return Texto;
    }

    public void LoadOBJFile()
    {

        changepath(display_user_obj.text);
        string objText = readfile(objFilePath);
        Debug.Log(objText);
        objText = objText.Replace(" ", " ");
        
        if (objText.Length == 0)
        {
            //Text.text = "file not found";
        }
        else
        {
            //Text.text = objText;
        }
        
        string[] objFileLines = objText.Split('\n');
        // 读取OBJ文件的内容
        //string[] objFileLines = System.IO.File.ReadAllLines(path);

        // 存储顶点、三角形面和纹理坐标的列表
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();


        List<int> wallTriangles = new List<int>();
        List<int> floorTriangles = new List<int>();

        foreach (string line in objFileLines)
        {
            string[] tokens = line.Split(' ');

            // 处理顶点数据
            if (tokens[0] == "v")
            {
                float x = float.Parse(tokens[1]);
                float y = float.Parse(tokens[2]);
                float z = float.Parse(tokens[3]);
                vertices.Add(new Vector3(x, y, z));
            }
           
            // 处理三角形面数据
            else if (tokens[0] == "f")
            {
                List<int> triangleIndices = new List<int>();
                for (int i = 1; i < tokens.Length; i++)
                {
                    int vertexIndex = int.Parse(tokens[i].Split('/')[0]) - 1;
                    triangleIndices.Add(vertexIndex);
                }

                Vector3 vertex1 = vertices[triangleIndices[0]];
                Vector3 vertex2 = vertices[triangleIndices[1]];
                Vector3 vertex3 = vertices[triangleIndices[2]];

                // 计算三角形的法向量
                Vector3 faceNormal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

                // 获取相邻三角形的法向量
                Vector3 prevFaceNormal = Vector3.zero;
                if (triangleIndices.Count >= 3)
                {
                    Vector3 prevVertex1 = vertices[triangleIndices[triangleIndices.Count - 3]];
                    Vector3 prevVertex2 = vertices[triangleIndices[triangleIndices.Count - 2]];
                    Vector3 prevVertex3 = vertices[triangleIndices[triangleIndices.Count - 1]];
                    prevFaceNormal = Vector3.Cross(prevVertex2 - prevVertex1, prevVertex3 - prevVertex1).normalized;
                }

                // 计算法向量的角度差
                float angleDiff = Vector3.Angle(faceNormal, prevFaceNormal);
                bool isAdjacentWall = prevFaceNormal.y > 0.55f;
                // 假设法向量的y分量大于0.1并且法向量的角度差小于某个阈值的视为墙壁
                if (faceNormal.y > 0.94f && angleDiff < 70f || isAdjacentWall)
                {
                    wallTriangles.AddRange(triangleIndices);
                }
                else
                {
                    floorTriangles.AddRange(triangleIndices);
                }
            }
        }
        for (int i = 0; i < vertices.Count; i++)
        {
            uvs.Add(new Vector2(vertices[i].x * 2.5f, vertices[i].z * 2.5f));
        }
        Mesh wallMesh = new Mesh();
        wallMesh.SetVertices(vertices);
        wallMesh.SetTriangles(wallTriangles, 0);
        wallMesh.SetUVs(0, uvs);

        //wallMesh.vertices = vertices.ToArray();
        //wallMesh.triangles = wallTriangles.ToArray();

        Mesh floorMesh = new Mesh();
        floorMesh.SetVertices(vertices);
        floorMesh.SetTriangles(floorTriangles, 0);
        floorMesh.SetUVs(0, uvs);

        //floorMesh.vertices = vertices.ToArray();
        //floorMesh.triangles = floorTriangles.ToArray();

        GameObject wallObj = new GameObject("WallObject");
        GameObject floorObj = new GameObject("FloorObject");

        // 添加MeshFilter和MeshRenderer組件
        MeshFilter wallMeshFilter = wallObj.AddComponent<MeshFilter>();
        MeshRenderer wallMeshRenderer = wallObj.AddComponent<MeshRenderer>();
        MeshFilter floorMeshFilter = floorObj.AddComponent<MeshFilter>();
        MeshRenderer floorMeshRenderer = floorObj.AddComponent<MeshRenderer>();

        // 設置Mesh
        wallMeshFilter.mesh = wallMesh;
        floorMeshFilter.mesh = floorMesh;



        wallObj.GetComponent<MeshRenderer>().material = mat;
        floorObj.GetComponent<MeshRenderer>().material = mat1;
        Renderer renderer = wallObj.GetComponent<Renderer>();
        Material material = renderer.material;
        material.SetTexture("_BumpMap", normalMap1);
        material.EnableKeyword("_NORMALMAP");
        wallObj.GetComponent<MeshRenderer>().material.color = Color.black;

        Renderer renderer1 = floorObj.GetComponent<Renderer>();
        Material material1 = renderer1.material;
        material1.SetTexture("_BumpMap", normalMap1);
        material1.EnableKeyword("_NORMALMAP");
        floorObj.GetComponent<MeshRenderer>().material.color = Color.gray;


        // 將牆壁和地板作為子物件加入父物件
        GameObject obj = new GameObject("RoomObject");
        MeshRenderer room = obj.AddComponent<MeshRenderer>();
        wallObj.transform.parent = obj.transform;
        floorObj.transform.parent = obj.transform;
        Debug.Log("ssss");

        /*obj.GetComponent<MeshRenderer>().material = mat;



        Renderer renderer = obj.GetComponent<Renderer>();
        Material material = renderer.material;
        material.SetTexture("_BumpMap", normalMap1);
        material.EnableKeyword("_NORMALMAP");
        Debug.Log("ssss");


        obj.GetComponent<MeshRenderer>().material.color = Color.gray;*/

        // 将物体放置在场景中
        obj.transform.position = Vector3.zero;

        if (obj != null)
        {
            // 计算相机的目标位置
            Vector3 targetPosition = obj.transform.position + new Vector3(60f, height, 0f) - obj.transform.forward * distance;

            // 设置相机的位置和旋转
            transform.position = targetPosition;

            // 调整相机的角度，使其斜着看
            float angle = 60f; // 设置斜视角的角度
            transform.RotateAround(obj.transform.position, Vector3.up, angle);

            Vector3 scaleMultiplier = new Vector3(5f, 5f, 5f);
            obj.transform.localScale = Vector3.Scale(transform.localScale, scaleMultiplier);
            Camera mainCamera = Camera.main; ;

            // 获取物体的高度
            float objectHeight = obj.GetComponent<Renderer>().bounds.size.y;

            // 设置相机的位置为物体的侧面上方
            Vector3 cameraPosition = obj.transform.position + Vector3.up * (objectHeight + 3f) - obj.transform.forward * (distance / 2f);
            mainCamera.transform.position = cameraPosition;

            // 设置相机的旋转，使其朝向物体
            mainCamera.transform.LookAt(obj.transform.position);

        }
        fin = obj;
}
}
