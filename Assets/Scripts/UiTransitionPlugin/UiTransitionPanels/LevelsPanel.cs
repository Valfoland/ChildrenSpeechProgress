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
            foreach (var data in dataPanel.DataPanelBtns)
            {
                data.BtnPanel.onClick.AddListener(() => OnClickBtn(this, data.ItemPanelTypes));
            }
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onSetInfoPanel?.Invoke("Levels");
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
        int maxCountLvls = DataGame.CountSections[DataGame.IdSelectSection]
            .CountMissions[DataGame.IdSelectMission]
            .CountLevels;
        var completedLvls = Child.CurrentChildrenData.CompletedLevels
            [$"{DataGame.IdSelectSection}{DataGame.IdSelectMission}"];

        for (int i = 0; i < levelsPanel.GoToGameBtns.Length; i++)
        {
            if (i < maxCountLvls)
            {
                var i1 = i;
                
                levelsPanel.GoToGameBtns[i].gameObject.SetActive(true);
                levelsPanel.GoToGameBtns[i].onClick.AddListener(() => GoToGame(i1));
                
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

