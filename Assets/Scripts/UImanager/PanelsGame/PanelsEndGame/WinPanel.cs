using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinPanel : PanelEndGameDecorator
{
    public static System.Action onStartAnimBank;
    private Image endGameImage;

    public WinPanel(Panel p, DataSetEndGamePanel endGamePanel) : base(p, endGamePanel)
    {
        endGameImage = dataSetEndGamePanel.EndGamePanelObject.GetComponent<Image>();
        dataSetEndGamePanel.BtnBackToMenu.onClick.AddListener(BackToMenu);
        ShowPanel();
    }

    public override void ShowPanel()
    {
        //dataSetEndGamePanel.TextEndGame.text = "Вы выиграли";
        onStartAnimBank?.Invoke();
    }

    protected override void BackToMenu()
    {
        base.BackToMenu();
    }
}


