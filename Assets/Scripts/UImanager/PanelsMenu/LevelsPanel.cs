using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataSetLevelsPanel
{
    public GameObject LevelsPanelObject;
    public Button GoToMissionsBtn;
    public Button[] GoToGameBtns;
}

public class LevelsPanel : Panel, IinfOfPanel
{
    public static System.Action<AsyncOperation> onStartLoadScene;
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
        int maxCountLvls = DataGame.CountSections[DataGame.IdSelectSection]
            .CountMissions[DataGame.IdSelectMission]
            .CountLevels;
        var completedLvls = Child.CurrentChildrenData.CompletedLevels
            [$"{DataGame.IdSelectSection}{DataGame.IdSelectMission}"];

        for (int i = 0; i < levelsPanel.GoToGameBtns.Length; i++)
        {
            if (i < maxCountLvls)
            {
                levelsPanel.GoToGameBtns[i].gameObject.SetActive(true);
                var i1 = i;
                levelsPanel.GoToGameBtns[i].onClick.AddListener(() => HideLevelPanel(i1));
                
                if (i > 0)
                {
                    levelsPanel.GoToGameBtns[i].interactable = completedLvls[i - 1];
                }
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
    
    private void HideLevelPanel(int idLevel)
    {
        this.idLevel = idLevel;
        SetInfoPanel();
        HidePanel();
        GoToNextScene();
    }

    private void GoToNextScene()
    {
        onStartLoadScene?.Invoke(SceneManager.LoadSceneAsync("Game"));
    }

    public void SetInfoPanel()
    {
        DataGame.IdSelectLvl = idLevel;
    }
}

