using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LosePanel : PanelEndGameDecorator
{
    private Image endGameImage;
    public LosePanel(Panel p, DataSetEndGamePanel endGamePanel) : base(p, endGamePanel)
    {
        endGameImage = dataSetEndGamePanel.EndGamePanelObject.GetComponent<Image>();
        dataSetEndGamePanel.BtnBackToMenu.onClick.AddListener(BackToMenu);
        ShowPanel();
    }

    public override void ShowPanel()
    {
        //dataSetEndGamePanel.TextEndGame.text = "Вы проиграли";
    }

    protected override void BackToMenu()
    {
        base.BackToMenu();
    }
}

