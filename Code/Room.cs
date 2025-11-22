using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    string objFilePath; // OBJ 檔案的路徑
    string path = "/storage/emulated/0/stairsFullCornerOuter.obj";
    public Material mat;
    public Material mat1;
    private Vector3 lastMousePosition;
    GameObject fin;
   
   

    float distance = 10f; // 相機與物體之間的距離
    float height = 10f; // 相機與物體之間的高度

    public Texture2D normalMap1;

	public TextMeshProUGUI display_user_obj;



    private float rotationY = 0;
    private float rotationX = 0;
    public bool t = false;
    GameObject CloneFolder3;
    public Vector3 Scale;
    GameObject button;
    GameObject ColorList;
    public Vector3 Position;
    public Quaternion q;
    public GameObject ButtonPrefab;



    Camera camera0;
    Camera camera1;
    Camera camera2;
    Camera camera3;
    Camera camera4;


	public void Start()
	{
		display_user_obj.text = Scene2.scene2.user_obj;
        LoadOBJFile();
        camera0 = GameObject.Find("Camera0").GetComponent<Camera>();
        camera1 = GameObject.Find("Camera1").GetComponent<Camera>(); // 使用相應的名稱或其他方式來找到相機
        camera2 = GameObject.Find("Camera2").GetComponent<Camera>();
        camera3 = GameObject.Find("Camera3").GetComponent<Camera>();
        camera4 = GameObject.Find("Camera4").GetComponent<Camera>();
	}

    public void Update()
    {
        

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
        string objText = readfile(objFilePath);//PC
        //string objText = readfile(path);//Android
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
        // 讀取 OBJ 檔案的內容
        //string[] objFileLines = System.IO.File.ReadAllLines(path);

        // 儲存頂點、三角形面和紋理座標的列表
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();


        List<int> wallTriangles = new List<int>();
        List<int> floorTriangles = new List<int>();

        foreach (string line in objFileLines)
        {
            string[] tokens = line.Split(' ');

            // 處理頂點資料
            if (tokens[0] == "v")
            {
                float x = float.Parse(tokens[1]);
                float y = float.Parse(tokens[2]);
                float z = float.Parse(tokens[3]);
                vertices.Add(new Vector3(x, y, z));
            }
           
            // 處理三角形面資料
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

                // 計算三角形的法向量
                Vector3 faceNormal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

                // 取得相鄰三角形的法向量
                Vector3 prevFaceNormal = Vector3.zero;
                if (triangleIndices.Count >= 3)
                {
                    Vector3 prevVertex1 = vertices[triangleIndices[triangleIndices.Count - 3]];
                    Vector3 prevVertex2 = vertices[triangleIndices[triangleIndices.Count - 2]];
                    Vector3 prevVertex3 = vertices[triangleIndices[triangleIndices.Count - 1]];
                    prevFaceNormal = Vector3.Cross(prevVertex2 - prevVertex1, prevVertex3 - prevVertex1).normalized;
                }

                // 計算法向量的角度差
                float angleDiff = Vector3.Angle(faceNormal, prevFaceNormal);
                bool isAdjacentWall = prevFaceNormal.y > 0.55f;
                // 假設法向量的 y 分量大於 0.1 並且法向量的角度差小於某個閾值時視為牆壁
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
            uvs.Add(new Vector2(vertices[i].x * 0.5f, vertices[i].z * 0.5f));
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

        // 新增 MeshFilter 和 MeshRenderer 元件
        MeshFilter wallMeshFilter = wallObj.AddComponent<MeshFilter>();
        MeshRenderer wallMeshRenderer = wallObj.AddComponent<MeshRenderer>();
        MeshFilter floorMeshFilter = floorObj.AddComponent<MeshFilter>();
        MeshRenderer floorMeshRenderer = floorObj.AddComponent<MeshRenderer>();

        // 設定 Mesh
        wallMeshFilter.mesh = wallMesh;
        floorMeshFilter.mesh = floorMesh;



        wallObj.GetComponent<MeshRenderer>().material = mat;
        floorObj.GetComponent<MeshRenderer>().material = mat1;
        Renderer renderer = wallObj.GetComponent<Renderer>();


        Vector3 objectSize = renderer.bounds.size;
        Vector3 consant = new Vector3(13.38f, 5.94f, 9.81f);
        Vector3 scale = new Vector3(
                objectSize.x / consant.x,
                objectSize.y / consant.y,
                objectSize.z / consant.z
         );
        GlobalValue.factor = scale;
        Material material = renderer.material;
        material.SetTexture("_BumpMap", normalMap1);
        material.EnableKeyword("_NORMALMAP");
        wallObj.GetComponent<MeshRenderer>().material.color = Color.white;

        Renderer renderer1 = floorObj.GetComponent<Renderer>();
        Material material1 = renderer1.material;
        material1.SetTexture("_BumpMap", normalMap1);
        material1.EnableKeyword("_NORMALMAP");
        floorObj.GetComponent<MeshRenderer>().material.color = Color.white;


        // 將牆壁和地板作為子物件加入父物件
        GameObject obj = new GameObject("RoomObject");
        MeshRenderer room = obj.AddComponent<MeshRenderer>();
        wallObj.transform.parent = obj.transform;
        floorObj.transform.parent = obj.transform;
        Debug.Log("ssss");

        

        wallObj.tag = "Floor";
        floorObj.tag = "Wall";

        //obj.GetComponent<MeshRenderer>().material.color = Color.gray;

        // 將物體放置在場景中
        obj.transform.position = Vector3.zero;

        // 呼叫 CalculatePoints 函式
        Vector3 centerPoint;
        Vector3[] boundaryPoints;
        float scaleFactor;
        CalculatePoints(floorObj, out centerPoint, out boundaryPoints, out scaleFactor);
        

        // 列印結果
        //Debug.Log("Center Point: " + centerPoint);

        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            Debug.Log("Boundary Point " + i + ": " + boundaryPoints[i]);
        }

         GlobalValue.Position = centerPoint;
         GlobalValue.Position.y = 0.0f;


        // 建立相機物件
        Camera cameraPrefab = Camera.main; // 這裡使用一個範例相機，你可以依需求替換成其他相機
        Camera camera0 = Instantiate(cameraPrefab, centerPoint, Quaternion.identity);
        camera0.name = "Camera0";
        AttachCameraRotation(camera0);

        Camera camera1 = Instantiate(cameraPrefab, boundaryPoints[0], Quaternion.identity);
        camera1.name = "Camera1";
        AttachCameraRotation(camera1);

        Camera camera2 = Instantiate(cameraPrefab, boundaryPoints[1], Quaternion.identity);
        camera2.name = "Camera2";
        AttachCameraRotation(camera2);

        Camera camera3 = Instantiate(cameraPrefab, boundaryPoints[2], Quaternion.identity);
        camera3.name = "Camera3";
        AttachCameraRotation(camera3);

        Camera camera4 = Instantiate(cameraPrefab, boundaryPoints[3], Quaternion.identity);
        camera4.name = "Camera4";
        AttachCameraRotation(camera4);
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // 如果有多於一個 Audio Listener，則停用多餘的那些
        if (audioListeners.Length > 1)
        {
            for (int i = 1; i < audioListeners.Length; i++)
            {
                audioListeners[i].enabled = false;
            }
        }
        


        if (obj != null)
        {
            // 取得物體的高度
            float objectHeight = obj.GetComponent<Renderer>().bounds.size.y;

            // 設定相機位置為物體中心上方
            //Vector3 cameraPosition = obj.transform.position + Vector3.up * ((objectHeight+10f) / 2f) + Vector3.left*5 ; ;
            Vector3 cameraPosition = new Vector3(0.4f, 0.4f, 4.6f);

            // 設定相機的旋轉，使其朝向物體
            Camera.main.transform.position = cameraPosition;
            Camera.main.transform.LookAt(obj.transform.position);
        }
        fin = obj;
    }
    /*public void OnPointerClick(PointerEventData eventData)
    {
         //Debug.Log(CloneFolder3.name);

        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log( clickedObject.name);
        t = !t;
        if (t == false)
        {
            button = GameObject.FindGameObjectWithTag("Button");
            Destroy(button);
        }
        else
        {
           
            Position =  clickedObject.transform.position;
            button = Instantiate(ButtonPrefab, Position, q);
            button.transform.parent =   clickedObject.transform;
            button.transform.localScale = Scale;
            button.SetActive(true);

            ColorList = button.transform.GetChild(0).gameObject;
            ColorList.SetActive(false);
            ColorList = button.transform.GetChild(2).gameObject;
            ColorList.SetActive(false);
            ColorList = button.transform.GetChild(3).gameObject;
            ColorList.SetActive(false);
            
           // CloneFolder3 = GameObject.FindGameObjectWithTag(funiture);
           
        }
    }*/

    public void SwitchToCamera1()
    {
        DisableAllCameras();
        camera1.enabled = true;
    }

    public void SwitchToCamera2()
    {
        DisableAllCameras();
        camera2.enabled = true;
    }

    public void SwitchToCamera3()
    {
        DisableAllCameras();
        camera3.enabled = true;
    }

    public void SwitchToCamera4()
    {
        DisableAllCameras();
        camera4.enabled = true;
    }

    public void SwitchToDefaultCamera()
    {
        DisableAllCameras();
        camera0.enabled = true;
    }


    void AttachCameraRotation(Camera cameraObject)
    {
        // 檢查是否已經有 CameraRotation 腳本，避免重複新增
        if (cameraObject.GetComponent<CameraRotation>() == null)
        {
            // 新增 CameraRotation 腳本
            CameraRotation rotationScript = cameraObject.AddComponent<CameraRotation>();

            // 設定腳本的參數（如果有的話）
            rotationScript.rotationSpeed = 1.5f;
        }
    }

    void CalculatePoints(GameObject obj, out Vector3 center, out Vector3[] boundaries, out float scale)
    {
        // 取得物體的 MeshFilter
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

        // 確保物體具有 MeshFilter 元件
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found on the object.");
            center = Vector3.zero;
            boundaries = new Vector3[0];
            scale = 1f;
            return;
        }

        // 取得物體的 Mesh
        Mesh mesh = meshFilter.mesh;

        // 取得物體的包圍盒
        Bounds bounds = mesh.bounds;

        // 計算中心點
        center = bounds.center;

        // 計算四個邊界點
        boundaries = new Vector3[4];
        boundaries[0] = new Vector3(center.x - bounds.extents.x/1.2f + obj.GetComponent<Renderer>().bounds.size.x / bounds.extents.x, obj.GetComponent<Renderer>().bounds.size.y, center.z); // 左
        boundaries[1] = new Vector3(center.x + bounds.extents.x / 1.2f - obj.GetComponent<Renderer>().bounds.size.x / bounds.extents.x,center.y, center.z / 1.2f); // 右
        boundaries[2] = new Vector3(center.x, center.y, center.z / 1.2f - bounds.extents.z + obj.GetComponent<Renderer>().bounds.size.z / bounds.extents.z); // 前
        boundaries[3] = new Vector3(center.x, center.y, center.z / 1.2f + bounds.extents.z - obj.GetComponent<Renderer>().bounds.size.z/ bounds.extents.z); // 後

        // 計算縮放因子
        scale = Mathf.Max(bounds.extents.x, bounds.extents.z);
    }

    private void DisableAllCameras()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera0.enabled = false;
    }
}
