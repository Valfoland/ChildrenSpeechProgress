using System.Collections;
using System.Collections.Generic;
using Bridge.BridgeReceiver;
using UnityEngine;

public class UiTransitionManagerGame : UiTransitionManager
{
    public static System.Action onNextLvl;
    [SerializeField] 
    private UiTransitionManagerGameData uiTransitionManagerGameData;
    protected override void Start()
    {
        base.Start();
        DataNextLvlPanel.onNextLvl += NextLevel;
        SetTextInfo();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        DataNextLvlPanel.onNextLvl -= NextLevel;
    }
    
    private void NextLevel()
    {
        onNextLvl?.Invoke();
    }
    
    public void EndGame()
    {
        uiTransitionManagerGameData.WinPanel.ShowPanel();
    }

    public void EndLvl(bool isWin)
    {
        if (isWin)
        {
            uiTransitionManagerGameData.NextLvlPanel.ShowPanel();
        }
        else
        {
            uiTransitionManagerGameData.LosePanel.ShowPanel();
        }
    }
 
    private void SetTextInfo()
    {
        iInfoPanel = (ITextPanel) uiTransitionManagerGameData.InfoPanel;
        string text = DataGame.SectionDataList[DataGame.IdSelectSection]
            .MissionDataList[DataGame.IdSelectMission]
            .LevelDataList[DataGame.IdSelectLvl].TextLevel;
        iInfoPanel.SetTextInfo(text);
    }
}

namespace Bridge.BridgeReceiver
{
    public class UiReceiver: IBridgeReciever<DataInfoPanel>
    {
        public static DataInfoPanel DataInfoPanel;
        public void TakeData(DataInfoPanel dataInfoPanel)
        {
            DataInfoPanel = dataInfoPanel;
        }
    }
}
