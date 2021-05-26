using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DataLogInPanel
{
    public UserNetworkService UserNetworkService;
    public Button RegistrationButton;
    public Button LogInButton;
    public GameObject ErrorLoginObject;
    public InputField LoginField;
    public InputField PasswordField;
}

public class LogInPanel : Panel
{
    private DataLogInPanel dataLogInPanel;
    
    public LogInPanel(DataPanel dataPanel, DataLogInPanel dataLogInPanel, PanelTypes panelTypes) : base(dataPanel, panelTypes)
    {
        this.dataLogInPanel = dataLogInPanel;
        dataLogInPanel.RegistrationButton.onClick.AddListener(GoToRegistrationWebSite);
        dataLogInPanel.LogInButton.onClick.AddListener(CheckLogIn);
        AddButtonListener();
    }

    private void GoToRegistrationWebSite()
    {
        Application.OpenURL(AuthenticationConfig.CSP_WEB_SITE);
    }

    private void CheckLogIn()
    {
        dataLogInPanel.UserNetworkService.Login(new UserModel
        {
            Email = dataLogInPanel.LoginField.text,
            Password = dataLogInPanel.PasswordField.text
        }, OnGetLogInResult);
    }

    public override void ShowPanel()
    {
        dataLogInPanel.ErrorLoginObject.SetActive(false);
        base.ShowPanel();
    }

    private void OnGetLogInResult(bool result)
    {
        if (result == true)
        {
            HidePanel();
            onSuccessResult?.Invoke();
        }
        else
        {
            dataLogInPanel.ErrorLoginObject.SetActive(true);
        }
    }
}
