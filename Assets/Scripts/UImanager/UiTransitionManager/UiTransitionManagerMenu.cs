using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTransitionManagerMenu : UiTransitionManager
{
    [SerializeField] private UiTransitionManagerMenuData uiTransitionManagerMenuData;
    [SerializeField] private AuthenticationManager authenticationManager;
    
    private static int countLaunchScene;
    
    protected override void Start()
    {
        base.Start();
        countLaunchScene++;
        Panel.onSetInfoPanel += SetTextInfo;
        authenticationManager.onLoginSuccess += OpenLogInPanel;
        uiTransitionManagerMenuData.LogInPanel.onSuccessResult += OnHideLogInPanel;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Panel.onSetInfoPanel -= SetTextInfo;
        authenticationManager.onLoginSuccess -= OpenLogInPanel;
        uiTransitionManagerMenuData.LogInPanel.onSuccessResult -= OnHideLogInPanel;
    }

    private void SetTextInfo(string textInfo = "")
    {
        iInfoPanel = (ITextPanel) uiTransitionManagerMenuData.InfoPanel;
        iInfoPanel.SetTextInfo(textInfo);
    }

    private void OpenLogInPanel(bool isLogIn)
    {
        if (isLogIn == false && countLaunchScene <= 1)
        {
            uiTransitionManagerMenuData.LogInPanel.ShowPanel();
        }
    }

    private void OnHideLogInPanel()
    {
        authenticationManager.OnCheckLogIn(true);
    }
}
