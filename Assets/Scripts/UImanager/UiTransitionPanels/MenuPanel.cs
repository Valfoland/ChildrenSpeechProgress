using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataMenuPanel
{
    public Button GoToSectionsButton;
    public Button LogInButton;
    public Button ShareButton;
    public Button RateButton;
}

public class MenuPanel : Panel
{
    private DataMenuPanel dataMenuPanel;
    
    public MenuPanel(DataPanel dataPanel, DataMenuPanel dataMenuPanel, PanelTypes panelTypes) : base(dataPanel, panelTypes)
    {
        this.dataMenuPanel = dataMenuPanel;
        this.dataMenuPanel.LogInButton.onClick.AddListener(OnClickLoginButton);
        this.dataMenuPanel.RateButton.onClick.AddListener(OnClickRateButton);
        this.dataMenuPanel.ShareButton.onClick.AddListener(OnClickShareButton);
        AddButtonListener();
        GetBtnSection();
    }

    private void OnClickLoginButton()
    {
        if (UserNetworkService.IsLoggedIn == false)
        {
            OpenLogInPanel();
        }
    }
    
    private void GetBtnSection()
    {
        dataMenuPanel.GoToSectionsButton.onClick.AddListener(GoToSections);
    }

    private void OpenLogInPanel()
    {
        OnDirectionTransition(this, "LogInPanel");
    }

    private void GoToSections()
    {
        OnDirectionTransition(this, "SectionsPanel");
    }

    private void OnClickShareButton()
    {
        new NativeShare()
            .SetText($"Диагностическое приложение для развития речи у детей! \n" + 
                     $"https://play.google.com/store/apps/details?id={Application.identifier}")
            .Share();
    }

    private void OnClickRateButton()
    {
        Application.OpenURL($"https://play.google.com/store/apps/details?id={Application.identifier}");
    }
}

