using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using Ping = System.Net.NetworkInformation.Ping;
using UnityEngine.Networking;


public class OK : MonoBehaviour
{
    GameObject obj;
    private AndroidJavaObject activityContext = null;
    private AndroidJavaObject wifiManager = null;
    private AndroidJavaObject wifiInfo = null;
    List<int> rssi = new List<int>();

    string gooAddress = "www.google.com";
    string youAddress = "www.Youtube.com";
    string facAddress = "www.facebook.com";

    // Start is called before the first frame update
     [System.Obsolete]
    public void OKOK()
    {
        obj = transform.parent.gameObject;
        //Debug.Log(obj.name);

        Transform record = obj.transform.parent;//查找父物件
        string ip = GetLocalIPAddress();
        float delayg = MeasureLatency(gooAddress);
        float delayy = MeasureLatency(youAddress);
        float delayf = MeasureLatency(facAddress);
        //SplitObject(record.gameObject, 2,  -56);//測試用
        int rs = GetRSSI();
        
        SplitObject(record.gameObject, 2,  rs);

        
        
        obj = obj.transform.parent.gameObject;
        obj.GetComponent<Data>().rssi = rs;
        obj.GetComponent<Data>().ping1 =delayg;
        obj.GetComponent<Data>().ping2 =delayy;
        obj.GetComponent<Data>().ping3 =delayf;
        obj.GetComponent<Data>().ipaddress =ip;

        obj.GetComponent<ObjectGenerator>().Lock = true;
    }




   
    // Start is called before the first frame update
    
    void Start()
    {
        //StartCoroutine(GetRSSI());
        
    }

     public int  GetRSSI()
    {
       int rssi = 0;
       if (Application.platform == RuntimePlatform.Android)
       {
           // 在 Android 平台上執行相關程式碼
           AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
           activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
           wifiManager = activityContext.Call<AndroidJavaObject>("getSystemService", "wifi");
           wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
           rssi = wifiInfo.Call<int>("getRssi");
           

           
            
       }
       return rssi;
           
        
    }
    private void SplitObject(GameObject record, int ca, int avgrssi)
    {
        // 遍历大物体的子物体
        foreach (Transform child in record.transform)
        {
            Debug.Log(child.name);
            // 处理子物体
            if (child.name == "wifi" && ca == 1)//創建的時候
            {
                child.gameObject.SetActive(false);
            }
            else if (child.name == "wifi" && ca == 2)//被點擊時
            {
                child.gameObject.SetActive(true);
                int cnt = 1;
                List<GameObject> wi = new List<GameObject>();
                foreach (Transform wifichild in child.gameObject.transform)
                {
                    if (cnt >= 1 && cnt <= 4)
                    {
                        if (wifichild.name == "Sphere")
                        {
                            GameObject w1 = wifichild.gameObject;
                            wi.Add(w1);
                        }
                        else
                        {
                            GameObject w1 = wifichild.transform.GetChild(0).gameObject;
                            wi.Add(w1);
                        }

                    }
                    else
                    {
                        break;
                    }
                    cnt++;



                }
                //Debug.Log(wi[0]);
                if (avgrssi >= -50)
                {
                    wi[0].GetComponent<MeshRenderer>().material.color = Color.green;
                    wi[1].GetComponent<MeshRenderer>().material.color = Color.green;
                    wi[2].GetComponent<MeshRenderer>().material.color = Color.green;
                    wi[3].GetComponent<MeshRenderer>().material.color = Color.green;

                }
                else if (avgrssi >= -60 && avgrssi < -50)
                {
                    wi[0].GetComponent<MeshRenderer>().material.color = Color.gray;
                    wi[1].GetComponent<MeshRenderer>().material.color = Color.blue;
                    wi[2].GetComponent<MeshRenderer>().material.color = Color.blue;
                    wi[3].GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else if (avgrssi >= -70 && avgrssi < -60)
                {

                    wi[0].GetComponent<MeshRenderer>().material.color = Color.gray;
                    wi[1].GetComponent<MeshRenderer>().material.color = Color.gray;
                    wi[2].GetComponent<MeshRenderer>().material.color = Color.yellow;
                    wi[3].GetComponent<MeshRenderer>().material.color = Color.yellow;

                }
                else if (avgrssi < -70)
                {
                    wi[0].GetComponent<MeshRenderer>().material.color = Color.gray;
                    wi[1].GetComponent<MeshRenderer>().material.color = Color.gray;
                    wi[2].GetComponent<MeshRenderer>().material.color = Color.gray;
                    wi[3].GetComponent<MeshRenderer>().material.color = Color.red;
                }

            }
        }

    }
      public int GetNetworkStatus(string target)
    {
        int  ti = 0 ;
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface ni in interfaces)
        {
            if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            {
                IPInterfaceProperties properties = ni.GetIPProperties();
                GatewayIPAddressInformationCollection gateways = properties.GatewayAddresses;

                //Debug.Log("IP" + gateways.ToString());
                foreach (GatewayIPAddressInformation gateway in gateways)
                {
                    Ping pingSender = new Ping();
                    PingReply reply = pingSender.Send(target);

                    if (reply.Status == IPStatus.Success)
                    {
                        //Debug.Log("Interface: " + ni.Name);
                        //Text.text = reply.RoundtripTime.ToString() + "ms";
                        //Text.text = "ok";
                        //Debug.Log("Signal Strength: " + reply.RoundtripTime + "%");
                        //Debug.Log("IP" + reply.Address.ToString());
                        ti = (int)reply.RoundtripTime ;
                        Debug.Log("Time : " + reply.RoundtripTime);
                        break;
                    }
                }
            }
        }
        return ti;

    }



     private string GetLocalIPAddress()
    {
        string localIP = string.Empty;

        // 获取所有网络接口
        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface networkInterface in networkInterfaces)
        {
            if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                // 获取网络接口的IP属性
                IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                // 获取每个IP属性中的单独IP地址
                UnicastIPAddressInformationCollection unicastIPs = ipProperties.UnicastAddresses;
                foreach (UnicastIPAddressInformation unicastIP in unicastIPs)
                {
                    // 选择IPv4地址
                    if (unicastIP.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = unicastIP.Address.ToString();
                    }
                }
            }
        }
        return localIP;
    }
     [System.Obsolete]
    private float  MeasureLatency(string target)
    {
        float startTime = Time.realtimeSinceStartup;

        UnityWebRequest request = UnityWebRequest.Get(target);
        request.SendWebRequest();

        // 等待请求完成或超时
        while (!request.isDone)
        {
            if (Time.realtimeSinceStartup - startTime > 5.0f) // 5秒超时
            {
                Debug.Log("Request timed out");
                break;
            }
        }
        float latency = -1000f;
        if (!request.isNetworkError && !request.isHttpError)
        {
            float endTime = Time.realtimeSinceStartup;
            latency = (endTime - startTime) * 100f; // 计算延迟（毫秒）
            Debug.Log("Estimated Latency: " + latency + " ms");
        }
        else
        {
            Debug.Log("Error measuring latency: " + request.error);
        }
        return latency;
    }
}
