using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bridge.BridgeReceiver;

public class UiTransitionManagerGameData : UiTransitionManagerData
{
    [Header("Дополнительные данные для панелек")]
    #region Auto_Generated_Code_Placement_Field
    [SerializeField] private DataGamePanel dataGamePanel;
    [SerializeField] private DataInfoPanel dataInfoPanel;
    [SerializeField] private DataExitPanel dataExitPanel;
    [SerializeField] private DataEndGamePanel dataEndGamePanel;
    [SerializeField] private DataLosePanel dataLosePanel;
    [SerializeField] private DataSetWinPanel dataSetWinPanel;
    [SerializeField] private DataNextLvlPanel dataNextLvlPanel;

    private Panel winPanel;
    private Panel losePanel;
    private Panel nextLvlPanel;

    private ITextPanel iInfoPanel;
    #endregion

    protected override void Start()
    {
        Panel.onSetInfoPanel += SetTextInfo;
        InitPanels();
        base.Start();
    }
    
    private void OnDestroy()
    {
        Panel.onSetInfoPanel -= SetTextInfo;
    }
    
    protected override void InitPanels()
    {
        base.InitPanels();
        dataInfoPanel.TextIdSectionsList = UiReceiver.DataInfoPanel.TextIdSectionsList;
        #region Auto_Generated_Code_Placement_Init
        
        Panel infoPanel = new InfoPanel(dataPanelDict["InfoPanel"], dataInfoPanel, PanelTypes.Secondary);
        iInfoPanel = (ITextPanel) infoPanel;
        Panel exitPanel = new ExitPanel(dataPanelDict["ExitPanel"], dataExitPanel, PanelTypes.Secondary);
        Panel endGamePanel = new EndGamePanel(dataPanelDict["EndGamePanel"], dataEndGamePanel, PanelTypes.Secondary);
        Panel gamePanel = new GamePanel(dataPanelDict["GamePanel"], dataGamePanel, PanelTypes.Main);
        nextLvlPanel = new NextLvlPanel(endGamePanel, dataPanelDict["EndGamePanel"], dataEndGamePanel, dataNextLvlPanel);
        losePanel = new LosePanel(endGamePanel, dataPanelDict["EndGamePanel"], dataEndGamePanel, dataLosePanel);
        winPanel = new WinPanel(endGamePanel, dataPanelDict["EndGamePanel"], dataEndGamePanel, dataSetWinPanel);

        UiTransitionDict = new Dictionary<Panel, Dictionary<Panel, TransitionTypes>>
        {
            {
                gamePanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {infoPanel, TransitionTypes.Soft},
                    {exitPanel, TransitionTypes.Soft},
                    {endGamePanel, TransitionTypes.Soft},
                }
            },
            {
                infoPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {gamePanel, TransitionTypes.Normal},
                }
            },
            {
                exitPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {gamePanel, TransitionTypes.Hard},
                }
            },
            {
                endGamePanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    
                }
            }
        };

        #endregion
    }
    
    public void EndGame()
    {
        winPanel.ShowPanel();
    }

    public void EndLvl(bool isWin)
    {
        if (isWin)
        {
            nextLvlPanel.ShowPanel();
        }
        else
        {
            losePanel.ShowPanel();
        }
    }

    private void SetTextInfo(string nameActivePanel = "")
    {
        string text = dataInfoPanel
                .TextIdSectionsList[DataGame.IdSelectSection]
                .TextMissionsList[DataGame.IdSelectMission]
                .TextLevelsList[DataGame.IdSelectLvl].TextLevel;
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


