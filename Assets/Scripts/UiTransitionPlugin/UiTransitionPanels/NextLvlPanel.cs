using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataNextLvlPanel
{
    public string TextNext;
    public static System.Action onNextLvl;
}
public class NextLvlPanel : PanelEndGameDecorator
{
    private DataNextLvlPanel dataNextLvlPanel;
    
    public NextLvlPanel(Panel panel,  DataPanel dataPanel, DataEndGamePanel endGamePanel, DataNextLvlPanel dataNextLvlPanel) : 
        base(panel, dataPanel, endGamePanel)
    {
        try
        {
            this.dataNextLvlPanel = dataNextLvlPanel;
        }
        catch (System.NullReferenceException)
        {

        }
    }
    
    public override void ShowPanel()
    {
        dataPanel.DataPanelBtns[1].BtnPanel.onClick.AddListener(NextLvl);
        dataEndGamePanel.TxtOther.text = dataNextLvlPanel.TextNext;
        panel.ShowPanel();
    }

    public override void HidePanel()
    {
        dataPanel.DataPanelBtns[1].BtnPanel.onClick.RemoveListener(NextLvl);
        base.HidePanel();
    }

    private void NextLvl()
    {
        if (DataGame.IdSelectLvl < 
            DataGame.CountSections[DataGame.IdSelectSection]
            .CountMissions[DataGame.IdSelectMission].CountLevels - 1)
        {
            DataGame.IdSelectLvl++;
            DataNextLvlPanel.onNextLvl?.Invoke();
            HidePanel();
        }
    }
}

