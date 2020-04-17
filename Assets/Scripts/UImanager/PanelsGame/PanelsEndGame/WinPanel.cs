using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class DataSetWinPanel
{
    public string TextWin;
}

public class WinPanel : PanelEndGameDecorator
{
    private DataSetWinPanel dataSetWinPanel;
    public WinPanel(Panel panel, DataSetEndGamePanel endGamePanel, DataSetWinPanel dataSetWinPanel) : base(panel, endGamePanel)
    {
        try
        {
            this.dataSetWinPanel = dataSetWinPanel;
        }
        catch (Exception e)
        {
            // ignored
        }
    }
        
    public override void ShowPanel()
    {
        dataSetEndGamePanel.BtnOther.onClick.AddListener(ShowResults);
        dataSetEndGamePanel.TxtOther.text = dataSetWinPanel.TextWin;
        panel.ShowPanel();
    }

    private  void ShowResults()
    {
        SceneManager.LoadScene("Menu");
    }
}


