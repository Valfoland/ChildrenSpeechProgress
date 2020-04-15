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
    public Button BtnOther;
    public Text TxtOther;
}

public class EndGamePanel : Panel
{
    private DataSetEndGamePanel dataSetEndGamePanel;
    public EndGamePanel(DataSetEndGamePanel dataSetEndGamePanel) : base(dataSetEndGamePanel.EndGamePanelObject)
    {
        try
        {
            dataSetEndGamePanel.BtnBackToMenu.onClick.AddListener(BackToMenu);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);   
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

