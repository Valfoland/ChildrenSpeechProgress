using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataSetLevelsPanel
{
    public Button[] GoToGameBtns;
}

public class LevelsPanel : Panel
{
    public static System.Action<AsyncOperation> onStartLoadScene;
    private int idLevel;
    private DataSetLevelsPanel levelsPanel;

    public LevelsPanel(DataPanel dataPanel, DataSetLevelsPanel levelsPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            this.levelsPanel = levelsPanel;
            SetInfoPanel();
            AddButtonListener();
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        var textInfo = DataGame
                .SectionDataList[DataGame.IdSelectSection]
                .MissionDataList[DataGame.IdSelectMission]
                .TextMission;
        onSetInfoPanel?.Invoke(textInfo);
        GetActiveBtnsLvl();
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        levelsPanel.GoToGameBtns.ToList().ForEach(x => x.onClick.RemoveAllListeners());
        base.HidePanel();
    }

    private void GetActiveBtnsLvl()
    {
        int maxCountLvls = DataGame.SectionDataList[DataGame.IdSelectSection]
            .MissionDataList[DataGame.IdSelectMission]
            .LevelDataList.Count;

        var completedLvls = Child.CurrentChildData.CompletedLevels
            [$"{DataGame.IdSelectSection}{DataGame.IdSelectMission}"];

        for (int i = 0; i < levelsPanel.GoToGameBtns.Length; i++)
        {
            var i1 = i;
            levelsPanel.GoToGameBtns[i].gameObject.SetActive(i < maxCountLvls);
            levelsPanel.GoToGameBtns[i].onClick.AddListener(() => GoToGame(i1));

            
            if (i > 0 && i < maxCountLvls)
            {
                levelsPanel.GoToGameBtns[i].interactable = completedLvls[i - 1];
            }
        }
    }

    private void SetInfoPanel()
    {
        DataGame.IdSelectLvl = idLevel;
    }
    
    private void GoToGame(int idLevel)
    {
        this.idLevel = idLevel;
        SetInfoPanel();
        onStartLoadScene?.Invoke(SceneManager.LoadSceneAsync("Game"));
    }
}

