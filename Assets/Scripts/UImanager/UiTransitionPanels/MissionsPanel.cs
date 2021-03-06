﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataMissionsPanel
{
    public Text SectionName;
    public Button[] GoToLvlBtns;
}

public class MissionsPanel : Panel
{
    private int idMission;
    private DataMissionsPanel dataMissionsPanel;

    public MissionsPanel(DataPanel dataPanel, DataMissionsPanel dataMissionsPanel, PanelTypes panelType) :
        base(dataPanel, panelType)
    {
        this.dataMissionsPanel = dataMissionsPanel;
        AddButtonListener();
    }

    public override void ShowPanel()
    {
        var txtInfo = DataGame
            .SectionDataList[DataGame.IdSelectSection]
            .TextSection;
        onSetInfoPanel?.Invoke(txtInfo);
        GetActiveBtnMission();
        SetNameSelectSection();
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
        int maxCountMissions = DataGame.SectionDataList[DataGame.IdSelectSection].MissionDataList.Count;
        
        for (int i = 0; i < dataMissionsPanel.GoToLvlBtns.Length; i++)
        {
            var i1 = i;
            dataMissionsPanel.GoToLvlBtns[i].gameObject.SetActive(i < maxCountMissions);
            dataMissionsPanel.GoToLvlBtns[i1].onClick.AddListener(() => GoToLevels(i1));
        }
    }

    private void SetNameSelectSection()
    {
        dataMissionsPanel.SectionName.text = DataGame.SectionDataList[DataGame.IdSelectSection].NameSection;
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

