using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LvlCounterFromMissions
{
    [SerializeField] private string nameMission;
    public int CountLvl;
}

[System.Serializable]
public class LvlCounterFromSections
{
    [SerializeField] private string nameSection;
    public List<LvlCounterFromMissions> CountLvlsIdMissions;
}

[System.Serializable]
public class DataSetLevelsPanel
{
    public GameObject LevelsPanelObject;
    public Button GoToMissionsBtn;
    public Button[] GoToGameBtns;
    [Header("Количество уровней в зав. от номеров миссий и разделов")]
    public List<LvlCounterFromSections> CountLvlsIdSections;
}

public class LevelsPanel : Panel, IinfOfPanel
{
    private int idLevel;
    private DataSetLevelsPanel levelsPanel;

    public LevelsPanel(DataSetLevelsPanel levelsPanel) : base(levelsPanel.LevelsPanelObject)
    {
        try
        {
            this.levelsPanel = levelsPanel;
            levelsPanel.GoToMissionsBtn.onClick.AddListener(HidePanel);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onShowPanel?.Invoke("Levels");
        int maxCountLvls = levelsPanel
            .CountLvlsIdSections[InfoOfPanel.IdSelectSection]
            .CountLvlsIdMissions[InfoOfPanel.IdSelectMission]
            .CountLvl;
        for (int i = 0; i < levelsPanel.GoToGameBtns.Length; i++)
        {
            if (i < maxCountLvls)
            {
                levelsPanel.GoToGameBtns[i].gameObject.SetActive(true);
            }
            else
            {
                levelsPanel.GoToGameBtns[i].gameObject.SetActive(false);
            }
        }

        levelsPanel.GoToGameBtns[0].onClick.AddListener(() => HideSectionPanel(0));
        levelsPanel.GoToGameBtns[1].onClick.AddListener(() => HideSectionPanel(1));
        levelsPanel.GoToGameBtns[2].onClick.AddListener(() => HideSectionPanel(1));
        //levelsPanel.GoToGameBtns[3].onClick.AddListener(() => HideSectionPanel(1));
        panelObject.SetActive(true);
    }

    protected override void HidePanel()
    {
        levelsPanel.GoToGameBtns[0].onClick.RemoveAllListeners();
        levelsPanel.GoToGameBtns[1].onClick.RemoveAllListeners();
        levelsPanel.GoToGameBtns[2].onClick.RemoveAllListeners();
        //levelsPanel.GoToGameBtns[3].onClick.RemoveAllListeners();
        base.HidePanel();
    }
    
    private void HideSectionPanel(int idLevel)
    {
        this.idLevel = idLevel;
        SetInfoPanel();
        //HidePanel();
        //GoToNextScene();
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetInfoPanel()
    {
        InfoOfPanel.IdSelectLvl = idLevel;
    }
}

