using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                levelsPanel.GoToGameBtns[i].onClick.AddListener(() => HideSectionPanel(i));
            }
            else
            {
                levelsPanel.GoToGameBtns[i].gameObject.SetActive(false);
            }
        }
        panelObject.SetActive(true);
    }

    public override void HidePanel()
    {
        levelsPanel.GoToGameBtns.ToList().ForEach(x => x.onClick.RemoveAllListeners());
        base.HidePanel();
    }
    
    private void HideSectionPanel(int idLevel)
    {
        this.idLevel = idLevel;
        SetInfoPanel();
        HidePanel();
        GoToNextScene();
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

