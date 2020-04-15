using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LosePanel : PanelEndGameDecorator
{
    public LosePanel(Panel panel, DataSetEndGamePanel endGamePanel) : base(panel, endGamePanel)
    {
        dataSetEndGamePanel.BtnOther.onClick.AddListener(Restart);
    }

    public override void ShowPanel()
    {
        //dataSetEndGamePanel.TextEndGame.text = "Вы проиграли";
        panel.ShowPanel();
    }

    private void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}

