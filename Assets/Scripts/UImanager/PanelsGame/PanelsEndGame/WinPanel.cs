using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : PanelEndGameDecorator
{
    public WinPanel(Panel panel, DataSetEndGamePanel endGamePanel) : base(panel, endGamePanel)
    {
        dataSetEndGamePanel.BtnOther.onClick.AddListener(ShowResults);
    }
        
    public override void ShowPanel()
    {
        //dataSetEndGamePanel.TextEndGame.text = "Вы выиграли";
        panel.ShowPanel();
    }

    private  void ShowResults()
    {
        SceneManager.LoadScene("Menu");
    }
}


