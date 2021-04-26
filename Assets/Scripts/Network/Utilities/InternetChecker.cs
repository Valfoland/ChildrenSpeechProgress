using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    private bool result;
    private const string echoServer = "https://www.yandex.ru";

    public void BackgroundCheckInternet(Action<AuthenticationTypes, bool> onCheckInternet = null)
    {
        StartCoroutine(CheckingInternet(onCheckInternet));
    }

    public void FastCheckInternet(Action<bool> onCheckInternet)
    {
        StartCoroutine(OneTimeCheckInternet(onCheckInternet));
    }

    private IEnumerator CheckingInternet(Action<AuthenticationTypes, bool> onCheckInternet = null)
    {
        bool result = false;

        while (true)
        {
            using (var request = UnityWebRequest.Head(echoServer))
            {
                request.timeout = 15;

                yield return request.SendWebRequest();

                result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
            }

            this.result = result;
            onCheckInternet?.Invoke(AuthenticationTypes.NETWORK_NOTICE, this.result);

            yield return new WaitForSeconds(10);
        }
    }

    private IEnumerator OneTimeCheckInternet(Action<bool> onCheckInternet)
    {
        using (var request = UnityWebRequest.Head(echoServer))
        {
            request.timeout = 5;

            yield return request.SendWebRequest();

            result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
            onCheckInternet?.Invoke(result);
        }
    }
}
