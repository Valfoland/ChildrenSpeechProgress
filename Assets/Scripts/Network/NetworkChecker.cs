using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkChecker : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }
    
    private IEnumerator CheckInternetConnection()
    {
        const string echoServer = "http://google.com";

        bool result = false;

        while (true)
        {
            using (var request = UnityWebRequest.Head(echoServer))
            {
                request.timeout = 5;
                yield return request.SendWebRequest();
                result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
                Debug.Log(result);
            }
        }
    }
}
