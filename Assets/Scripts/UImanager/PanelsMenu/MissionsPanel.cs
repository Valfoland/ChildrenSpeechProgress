using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MissionsButtons
{
    [SerializeField] private string nameSection;
    public Button[] GoToLvlBtn;
}

[System.Serializable]
public class MissionsCount
{
    [SerializeField] private string nameSection;
    public int CountMissions;
}

[System.Serializable]
public class DataSetMissionsPanel
{
    public GameObject MissionsPanelObject;
    public Button BackToMissionsBtn;
    public Button GoToSectionsBtn;
    [Header("Кол-во и виды кнопок в зав. от раздела")]
    public List<MissionsButtons> GoToLvlBtns;
    [Header("Кол-во миссий в зав. от раздела")]
    public List<MissionsCount> CountMissionsIdSection;
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
        int maxCountMissions = missionsPanel
            .CountMissionsIdSection[DataTasks.IdSelectSection]
            .CountMissions;
        int capcityMissions = missionsPanel
            .GoToLvlBtns[DataTasks.IdSelectSection]
            .GoToLvlBtn.Length;
        
        for (int i = 0; i < capcityMissions; i++)
        {
            if (i < maxCountMissions)
                missionsPanel.GoToLvlBtns[DataTasks.IdSelectSection].GoToLvlBtn[i].gameObject.SetActive(true);
            else
                missionsPanel.GoToLvlBtns[DataTasks.IdSelectSection].GoToLvlBtn[i].gameObject.SetActive(false);
        }
        missionsPanel.GoToLvlBtns[DataTasks.IdSelectSection].GoToLvlBtn[0].onClick.AddListener(
            () => HideSectionPanel(0));
        missionsPanel.GoToLvlBtns[DataTasks.IdSelectSection].GoToLvlBtn[1].onClick.AddListener(
            () => HideSectionPanel(1));
        
        panelObject.SetActive(true);
    }

    public override void HidePanel()
    {
        missionsPanel.GoToLvlBtns[DataTasks.IdSelectSection].GoToLvlBtn[0].onClick.RemoveAllListeners();
        missionsPanel.GoToLvlBtns[DataTasks.IdSelectSection].GoToLvlBtn[1].onClick.RemoveAllListeners();
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
        DataTasks.IdSelectMission= idMission;
    }
}

