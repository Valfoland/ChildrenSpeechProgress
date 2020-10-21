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
    [SerializeField] private DataSettingsPanel dataSettingsPanel;
    [SerializeField] private DataStatsPanel dataStatsPanel;
    [SerializeField] private DataChildrenPanel dataChildrenPanel;
    [SerializeField] private DataAddChildPanel dataAddChildPanel;
    [SerializeField] private DataExitPanel dataExitPanel;
    [SerializeField] private DataSectionsPanel dataSectionsPanel;
    [SerializeField] private DataMissionsPanel dataSetMissionsPanel;
    [SerializeField] private DataSetLevelsPanel dataSetLevelsPanel;
    public DataInfoPanel DataInfoPanel;
    
    public Panel AddChildPanel;
    private Panel missionsPanel;
    private Panel levelsPanel;
    public Panel ChildrenPanel;
    public Panel MenuPanel;
    public Panel InfoPanel;
    #endregion

    protected override void Start()
    {
        InitPanels();
        base.Start();
    }

    protected override void InitPanels()
    {
        base.InitPanels();
        
        new UIManagerBridge<DataInfoPanel>(new UiReceiver(), DataInfoPanel);

        #region Auto_Generated_Code_Placement_Init
        MenuPanel = new MenuPanel(dataPanelDict["MenuPanel"], dataMenuPanel, PanelTypes.Main);
        Panel statsPanel = new StatsPanel(dataPanelDict["StatsPanel"], dataStatsPanel, PanelTypes.Main);
        ChildrenPanel = new ChildrenPanel(dataPanelDict["ChildrenPanel"], dataChildrenPanel, PanelTypes.Main);
        levelsPanel = new LevelsPanel(dataPanelDict["LevelsPanel"], dataSetLevelsPanel, PanelTypes.Main);
        Panel sectionsPanel = new SectionsPanel(dataPanelDict["SectionsPanel"], dataSectionsPanel, PanelTypes.Main);
        missionsPanel = new MissionsPanel(dataPanelDict["MissionsPanel"], dataSetMissionsPanel, PanelTypes.Main);

        Panel settingsPanel = new SettingsPanel(dataPanelDict["SettingsPanel"], dataSettingsPanel, PanelTypes.Secondary);
        Panel exitPanel = new ExitPanel(dataPanelDict["ExitPanel"], dataExitPanel, PanelTypes.Secondary);
        AddChildPanel = new AddChildPanel(dataPanelDict["AddChildPanel"], dataAddChildPanel, PanelTypes.Secondary);
        InfoPanel = new InfoPanel(dataPanelDict["InfoPanel"], DataInfoPanel, PanelTypes.Secondary);

        UiTransitionDict = new Dictionary<Panel, Dictionary<Panel, TransitionTypes>>
        {
            {
                MenuPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {statsPanel, TransitionTypes.Hard},
                    {ChildrenPanel, TransitionTypes.Hard},
                    {sectionsPanel, TransitionTypes.Hard},
                    {exitPanel, TransitionTypes.Soft},
                    {settingsPanel, TransitionTypes.Soft}
                }
            },
            {
                statsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Hard},
                }
            },
            {
                ChildrenPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Hard},
                    {AddChildPanel, TransitionTypes.Soft}
                }
            },
            {
                AddChildPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    
                }
            },
            {
                settingsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {MenuPanel, TransitionTypes.Normal},
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
            },
        };

        #endregion
    }
}

