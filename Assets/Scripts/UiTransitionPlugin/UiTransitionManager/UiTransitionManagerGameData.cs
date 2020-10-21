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
    [SerializeField] private DataExitPanel dataExitPanel;
    [SerializeField] private DataEndGamePanel dataEndGamePanel;
    [SerializeField] private DataLosePanel dataLosePanel;
    [SerializeField] private DataSetWinPanel dataSetWinPanel;
    [SerializeField] private DataNextLvlPanel dataNextLvlPanel;
    public DataInfoPanel DataInfoPanel;
    
    public Panel WinPanel;
    public Panel LosePanel;
    public Panel NextLvlPanel;
    public Panel InfoPanel;
    #endregion

    private void Awake()
    {
        InitPanels();
    }

    protected override void InitPanels()
    {
        base.InitPanels();
        
        #region Auto_Generated_Code_Placement_Init
        DataInfoPanel.TextIdSectionsList = UiReceiver.DataInfoPanel.TextIdSectionsList;
        InfoPanel = new InfoPanel(dataPanelDict["InfoPanel"], DataInfoPanel, PanelTypes.Secondary);
        Panel exitPanel = new ExitPanel(dataPanelDict["ExitPanel"], dataExitPanel, PanelTypes.Secondary);
        Panel endGamePanel = new EndGamePanel(dataPanelDict["EndGamePanel"], dataEndGamePanel, PanelTypes.Secondary);
        Panel gamePanel = new GamePanel(dataPanelDict["GamePanel"], dataGamePanel, PanelTypes.Main);
        NextLvlPanel = new NextLvlPanel(endGamePanel, dataPanelDict["EndGamePanel"], dataEndGamePanel, dataNextLvlPanel);
        LosePanel = new LosePanel(endGamePanel, dataPanelDict["EndGamePanel"], dataEndGamePanel, dataLosePanel);
        WinPanel = new WinPanel(endGamePanel, dataPanelDict["EndGamePanel"], dataEndGamePanel, dataSetWinPanel);

        UiTransitionDict = new Dictionary<Panel, Dictionary<Panel, TransitionTypes>>
        {
            {
                gamePanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {InfoPanel, TransitionTypes.Soft},
                    {exitPanel, TransitionTypes.Soft}
                }
            },
            {
                InfoPanel,
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
            }
        };

        #endregion
    }
}

