using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataExitPanel
{
    public Button ExitBtn;
}

public class ExitPanel : Panel
{
    private DataExitPanel dataExitPanel;

    public ExitPanel(DataPanel dataPanel, DataExitPanel dataExitPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            AddButtonListener();
            this.dataExitPanel = dataExitPanel;
            dataExitPanel.ExitBtn.onClick.AddListener(ExitApp);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        PanelObject.SetActive(true);
    }

    private void ExitApp()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Application.Quit();
        }
    }
}

