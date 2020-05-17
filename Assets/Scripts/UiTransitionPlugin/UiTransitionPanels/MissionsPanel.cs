using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataMissionsPanel
{
    public Button[] GoToLvlBtns;
}

public class MissionsPanel : Panel
{
    private int idMission;
    private DataMissionsPanel dataMissionsPanel;

    public MissionsPanel(DataPanel dataPanel, DataMissionsPanel dataMissionsPanel, PanelTypes panelType) : 
        base(dataPanel, panelType)
    {
        try
        {
            this.dataMissionsPanel = dataMissionsPanel;
            foreach (var data in dataPanel.DataPanelBtns)
            {
                data.BtnPanel.onClick.AddListener(() => OnClickBtn(this, data.ItemPanelTypes));
            }
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onSetInfoPanel?.Invoke("Missions");
        GetActiveBtnMission();
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        foreach (var btn in dataMissionsPanel.GoToLvlBtns)
        {
            btn.onClick.RemoveAllListeners();
        }
        base.HidePanel();
    }

    private void GetActiveBtnMission()
    {
        int maxCountMissions = DataGame.CountSections[DataGame.IdSelectSection].CountMissions.Count;

        for (int i = 0; i < dataMissionsPanel.GoToLvlBtns.Length; i++)
        {
            dataMissionsPanel.GoToLvlBtns[DataGame.IdSelectSection].gameObject
                .SetActive(i < maxCountMissions);
            var i1 = i;
            dataMissionsPanel.GoToLvlBtns[i].onClick.AddListener(() => GoToLevels(i1));
        }
    }

    private void SetInfoPanel()
    {
        DataGame.IdSelectMission = idMission;
    }
    
    private void GoToLevels(int idMission)
    {
        this.idMission = idMission;
        SetInfoPanel();
        OnDirectionTransition(this, "LevelsPanel");
    }
}

