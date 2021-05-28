using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bridge;
using Bridge.BridgeReceiver;

public class UiTransitionManagerMenuData : UiTransitionManagerData
{
    [Header("Дополнительные данные для панелек")]
    #region Auto_Generated_Code_Placement_Field
    [SerializeField] private DataMenuPanel dataMenuPanel;
    [SerializeField] private DataStatsPanel dataStatsPanel;
    [SerializeField] private DataChildrenPanel dataChildrenPanel;
    [SerializeField] private DataExitPanel dataExitPanel;
    [SerializeField] private DataSectionsPanel dataSectionsPanel;
    [SerializeField] private DataMissionsPanel dataSetMissionsPanel;
    [SerializeField] private DataSetLevelsPanel dataSetLevelsPanel;
    [SerializeField] private DataInfoGamePanel dataInfoGamePanel;

    public DataLogInPanel DataLogInPanel;
    public DataInfoPanel DataInfoPanel;

    public Panel LogInPanel;
    public Panel ChildrenPanel;
    public Panel MenuPanel;
    public Panel InfoPanel;
    #endregion

    protected override void Awake()
    {
        InitPanels();
        base.Awake();
    }

    protected override void InitPanels()
    {
        base.InitPanels();
        
        #region Auto_Generated_Code_Placement_Init

        MenuPanel = new MenuPanel(dataPanelDict["MenuPanel"], dataMenuPanel, PanelTypes.Main);
        ChildrenPanel = new ChildrenPanel(dataPanelDict["ChildrenPanel"], dataChildrenPanel, PanelTypes.Main);
        InfoPanel = new InfoPanel(dataPanelDict["InfoPanel"], DataInfoPanel, PanelTypes.Secondary);
        LogInPanel = new LogInPanel(dataPanelDict["LogInPanel"], DataLogInPanel, PanelTypes.Secondary);
        
        Panel levelsPanel = new LevelsPanel(dataPanelDict["LevelsPanel"], dataSetLevelsPanel, PanelTypes.Main);
        Panel sectionsPanel = new SectionsPanel(dataPanelDict["SectionsPanel"], dataSectionsPanel, PanelTypes.Main);
        Panel missionsPanel = new MissionsPanel(dataPanelDict["MissionsPanel"], dataSetMissionsPanel, PanelTypes.Main);
        Panel exitPanel = new ExitPanel(dataPanelDict["ExitPanel"], dataExitPanel, PanelTypes.Secondary);
        Panel infoGamePanel = new InfoGamePanel(dataPanelDict["InfoGamePanel"], dataInfoGamePanel, PanelTypes.Secondary);
        
        UiTransitionDict = new Dictionary<Panel, Dictionary<Panel, TransitionTypes>>
        {
            {
                MenuPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {ChildrenPanel, TransitionTypes.Hard},
                    {sectionsPanel, TransitionTypes.Hard},
                    {exitPanel, TransitionTypes.Soft},
                    {LogInPanel, TransitionTypes.Soft},
                    {infoGamePanel, TransitionTypes.Soft}
                }
            },
            {
                ChildrenPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Hard}
                }
            },
            {
                infoGamePanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Hard}
                }
            },
            {
                InfoPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {missionsPanel, TransitionTypes.Normal},
                    {levelsPanel, TransitionTypes.Normal}
                }
            },
            {
                exitPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Hard},
                }
            },
            {
              LogInPanel,
              new Dictionary<Panel, TransitionTypes>
              {
                  {MenuPanel, TransitionTypes.Hard}
              }
            },
            {
                sectionsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Hard},
                    {missionsPanel, TransitionTypes.Hard}
                }
            },
            {
                missionsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {sectionsPanel, TransitionTypes.Hard},
                    {levelsPanel, TransitionTypes.Hard},
                    {InfoPanel, TransitionTypes.Soft}
                }
            },
            {
                levelsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {missionsPanel, TransitionTypes.Hard},
                    {InfoPanel, TransitionTypes.Soft}
                }
            }
        };

        #endregion
    }
}

