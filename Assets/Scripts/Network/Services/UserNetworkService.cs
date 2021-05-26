using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Networking;

public class UserModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserNetworkService: BaseNetworkService
{
    public static bool IsLoggedIn;
    private const string LOGIN_ROUTE = "Account/Login";
    private const string LOGOUT_ROUTE = "Account/Logout";
    private const string IS_LOGGED_IN_ROUTE = "Account/IsLoggedIn";

    public void Login<T>(T formData, Action<bool> onGetResponse = null)
    {
        StartCoroutine(LoginCoroutine(formData, onGetResponse));
    }
    
    public void Logout(Action onGetResponse = null)
    {
        PlayerPrefs.DeleteKey("AuthCookie");
        UnityWebRequest.ClearCookieCache();
        IsLoggedIn = false;
    }

    public void BackgroundCheckLogin(Action<AuthenticationTypes, bool> onGetResponse = null)
    {
        StartCoroutine(BackgroundCheckLoginCoroutine(onGetResponse));
    }
    
    public void CheckLogin(Action<bool> onGetResponse = null)
    {
        StartCoroutine(CheckLoginCoroutine(onGetResponse));
    }

    private IEnumerator BackgroundCheckLoginCoroutine(Action<AuthenticationTypes, bool> onGetResponse = null)
    {
        while (true)
        {
            using (var req = UnityWebRequest.Get(URL + IS_LOGGED_IN_ROUTE))
            {
                if(PlayerPrefs.HasKey("AuthCookie"))
                    req.SetRequestHeader("Set-Cookie", PlayerPrefs.GetString("AuthCookie"));
                yield return req.SendWebRequest();
                
                var data = JsonConvert.DeserializeObject<Dictionary<string, bool>>(req.downloadHandler.text);

                if (data != null && data.ContainsKey("status"))
                {
                    IsLoggedIn = data["status"];
                    onGetResponse?.Invoke(AuthenticationTypes.LOGIN_NOTICE, IsLoggedIn);
                }
            }

            yield return new WaitForSeconds(10);
        }
    }

    private IEnumerator CheckLoginCoroutine(Action<bool> onGetResponse = null)
    {
        using (var req = UnityWebRequest.Get(URL + IS_LOGGED_IN_ROUTE))
        {
            if(PlayerPrefs.HasKey("AuthCookie"))
                req.SetRequestHeader("Set-Cookie", PlayerPrefs.GetString("AuthCookie"));
            yield return req.SendWebRequest();
            
            var data = JsonConvert.DeserializeObject<Dictionary<string, bool>>(req.downloadHandler.text);

            
            if (data != null && data.ContainsKey("status"))
            {
                IsLoggedIn = data["status"];
                onGetResponse?.Invoke(IsLoggedIn);
            }
            else
            {
                
                IsLoggedIn = false;
                onGetResponse?.Invoke(IsLoggedIn);
            }

        }
    }

    private IEnumerator LoginCoroutine<T>(T formData, Action<bool> onGetResponse = null)
    {
        string json = JsonConvert.SerializeObject(formData);

        var req = new UnityWebRequest(URL + LOGIN_ROUTE, "POST")
        {
            uploadHandler = new UploadHandlerRaw(new UTF8Encoding().GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (!req.isNetworkError)
        {
            IsLoggedIn = req.responseCode == 200;
            if (req.GetResponseHeaders().ContainsKey("Set-Cookie"))
            {
                PlayerPrefs.SetString("AuthCookie", req.GetResponseHeaders()["Set-Cookie"]);
            }
            onGetResponse?.Invoke(IsLoggedIn);
        }
    }
}
