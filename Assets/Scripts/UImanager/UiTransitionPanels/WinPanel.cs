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
    public WinPanel(Panel panel, DataPanel dataPanel, DataEndGamePanel endGamePanel, DataSetWinPanel dataSetWinPanel) : 
        base(panel, dataPanel, endGamePanel)
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
        dataPanel.DataPanelBtns[1].BtnPanel.onClick.AddListener(ShowResults);
        dataEndGamePanel.TxtOther.text = dataSetWinPanel.TextWin;
        panel.ShowPanel();
    }

    private  void ShowResults()
    {
        dataPanel.DataPanelBtns[1].BtnPanel.onClick.RemoveListener(ShowResults);
        SceneManager.LoadScene("Menu"); //Temp
    }
}


