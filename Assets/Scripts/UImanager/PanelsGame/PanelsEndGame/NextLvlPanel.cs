using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetNextLvlPanel
{
    public string TextNext;
}
public class NextLvlPanel : PanelEndGameDecorator
{
    private DataSetNextLvlPanel dataSetNextLvlPanel;
    public static System.Action onNextLvl;
    public NextLvlPanel(Panel panel, DataSetEndGamePanel endGamePanel, DataSetNextLvlPanel dataSetNextLvlPanel) : base(panel, endGamePanel)
    {
        try
        {
            this.dataSetNextLvlPanel = dataSetNextLvlPanel;
        }
        catch (System.NullReferenceException)
        {

        }
    }

    public override void ShowPanel()
    {
        dataSetEndGamePanel.BtnOther.onClick.AddListener(NextLvl);
        dataSetEndGamePanel.TxtOther.text = dataSetNextLvlPanel.TextNext;
        panel.ShowPanel();
    }

    public override void HidePanel()
    {
        dataSetEndGamePanel.BtnOther.onClick.RemoveAllListeners();
        panel.HidePanel();
    }

    private void NextLvl()
    {
        if (DataTasks.IdSelectLvl < 
            DataTasks.CountSections[DataTasks.IdSelectSection]
            .CountMissions[DataTasks.IdSelectMission].CountLevels - 1)
        {
            DataTasks.IdSelectLvl++;
            onNextLvl?.Invoke();
            HidePanel();
        }
    }
}

