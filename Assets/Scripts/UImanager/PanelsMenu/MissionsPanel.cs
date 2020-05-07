using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetMissionsPanel
{
    public GameObject MissionsPanelObject;
    public Button BackToMissionsBtn;
    public Button GoToSectionsBtn;
    [Header("Кол-во и виды кнопок в зав. от раздела")]
    public Button[] GoToLvlBtns;
}

public class MissionsPanel : Panel, IinfOfPanel
{
    private int idMission;
    private DataSetMissionsPanel missionsPanel;

    public MissionsPanel(DataSetMissionsPanel missionsPanel) : base(missionsPanel.MissionsPanelObject)
    {
        try
        {
            this.missionsPanel = missionsPanel;
            missionsPanel.BackToMissionsBtn.onClick.AddListener(ShowPanel);
            missionsPanel.GoToSectionsBtn.onClick.AddListener(HidePanel);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onShowPanel?.Invoke("Missions");
        int maxCountMissions = DataGame.CountSections[DataGame.IdSelectSection].CountMissions.Count;

        for (int i = 0; i < missionsPanel.GoToLvlBtns.Length; i++)
        {
            missionsPanel.GoToLvlBtns[DataGame.IdSelectSection].gameObject
                .SetActive(i < maxCountMissions);
        }
        missionsPanel.GoToLvlBtns[0].onClick.AddListener(
            () => HideSectionPanel(0));
        missionsPanel.GoToLvlBtns[1].onClick.AddListener(
            () => HideSectionPanel(1));
        
        panelObject.SetActive(true);
    }

    public override void HidePanel()
    {
        missionsPanel.GoToLvlBtns[0].onClick.RemoveAllListeners();
        missionsPanel.GoToLvlBtns[1].onClick.RemoveAllListeners();
        base.HidePanel();
    }

    private void HideSectionPanel(int idMission)
    {
        this.idMission = idMission;
        SetInfoPanel();
        HidePanel();
        onHidePanel?.Invoke();
    }

    public void SetInfoPanel()
    {
        DataGame.IdSelectMission = idMission;
    }
}

