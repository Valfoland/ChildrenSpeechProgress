using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataSetEndGamePanel
{
    public GameObject EndGamePanelObject;
    public Button BtnBackToMenu;
}

public class EndGamePanel : Panel
{
    private DataSetEndGamePanel dataSetEndGamePanel;
    public EndGamePanel(DataSetEndGamePanel dataSetEndGamePanel) : base(dataSetEndGamePanel.EndGamePanelObject)
    {
        try
        {
            ShowPanel();
            dataSetEndGamePanel.BtnBackToMenu.onClick.AddListener(GoToMenu);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);   
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

