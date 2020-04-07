using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetMenuPanel
{
    public GameObject MenuPanelObject;
    public Button ChildrenToMenuBtn;
    public Button SectionsToMenuBtn;
    public Button StatsToMenuBtn;
    public Button GoToStatsBtn;
    public Button GoToSectionsBtn;
    public Button GoToChildrenBtn;
}

public class MenuPanel : Panel
{
    public MenuPanel(DataSetMenuPanel menuPanel) : base(menuPanel.MenuPanelObject)
    {
        try
        {
            menuPanel.ChildrenToMenuBtn.onClick.AddListener(ShowPanel);
            menuPanel.SectionsToMenuBtn.onClick.AddListener(ShowPanel);
            menuPanel.StatsToMenuBtn.onClick.AddListener(ShowPanel);
            menuPanel.GoToStatsBtn.onClick.AddListener(HidePanel);
            menuPanel.GoToSectionsBtn.onClick.AddListener(HidePanel);
            menuPanel.GoToChildrenBtn.onClick.AddListener(HidePanel);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    protected override void HidePanel()
    {
        onHidePanel?.Invoke();
        if (PlayerPrefs.HasKey("countChild"))
        {
            base.HidePanel();
        }
    }
}

