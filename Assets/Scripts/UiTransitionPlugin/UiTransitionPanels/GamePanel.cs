using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataGamePanel
{
    //Сюда можно вставить доп.данные
}

public class GamePanel : Panel
{
    private int idLevel;
    public GamePanel(DataPanel dataPanel, DataGamePanel menuPanel, PanelTypes panelTypes) : base(dataPanel, panelTypes)
    {
        try
        {
            AddButtonListener();
            ShowPanel();
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onSetInfoPanel?.Invoke("Game");
        base.ShowPanel();
    }
}

