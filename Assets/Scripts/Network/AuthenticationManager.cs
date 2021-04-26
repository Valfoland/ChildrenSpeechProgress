using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    [SerializeField] private InternetChecker internetChecker;
    [SerializeField] private UserNetworkService userNetworkService;
    [SerializeField] private Button buttonLoggedIn;
    [SerializeField] private List<Sprite> spritesLogin;
    [SerializeField] private GameObject BlockObject;
    public Action<bool> onLoginSuccess;
    public Action onInternetFail;

    private void Start()
    {
        buttonLoggedIn.onClick.AddListener(OnClickLoginButton);
        BlockObject.SetActive(true);
        StartAuthenticationPipeLine();
    }

    private void StartAuthenticationPipeLine()
    {
        internetChecker.FastCheckInternet(OnCheckInternet);
    }

    private void OnCheckInternet(bool result)
    {
        if (result)
        {
            userNetworkService.CheckLogin(OnCheckLogIn);
        }
        else
        {
            onInternetFail?.Invoke();
        }
    }

    public void OnCheckLogIn(bool result)
    {
        buttonLoggedIn.image.sprite = result ? spritesLogin[0] : spritesLogin[1];
        onLoginSuccess?.Invoke(result);
    }

    private void OnClickLoginButton()
    {
        if (UserNetworkService.IsLoggedIn)
        {
            userNetworkService.Logout();
            buttonLoggedIn.image.sprite = spritesLogin[1];
        }
    }
}
