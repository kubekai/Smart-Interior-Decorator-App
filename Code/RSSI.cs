using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RSSI : MonoBehaviour
{
    private AndroidJavaObject activityContext = null;
    private AndroidJavaObject wifiManager = null;
    private AndroidJavaObject wifiInfo = null;
    // Start is called before the first frame update
    List<int> rssi = new List<int>();
    void Start()
    {
        //StartCoroutine(GetRSSI());
        
    }

    IEnumerator GetRSSI()
    {
        while (true)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                // 在 Android 平台上執行相關程式碼
                AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
                wifiManager = activityContext.Call<AndroidJavaObject>("getSystemService", "wifi");
                wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
                int r = wifiInfo.Call<int>("getRssi");
                rssi.Add(r);



            }

            yield return new WaitForSeconds(0.7f); //每秒获取一次RSSI
        }
    }
    public void OnButtonClick()
    {
        GameObject obj = this.transform.parent.gameObject;
        Debug.Log(obj.name);

        Transform record = obj.transform.parent;//查找父物件




        /*int rsum = 0;
        int count = 0;
        for (int i = rssi.Count - 1; i >= rssi.Count - 3; i--)
        {
            count++;
            rsum += rssi[i];
        }
        int avgrssi = rsum / count;
        */
        SplitObject(record.gameObject, 2, -54);

        rssi.Clear();


        // 在按钮点击时执行的操作
        Debug.Log("Button clicked!" + obj.name);
        // ...
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
                Debug.Log(wi[0]);
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
}