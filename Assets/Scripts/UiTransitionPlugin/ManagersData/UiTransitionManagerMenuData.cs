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
    [SerializeField] private DataInfoPanel dataInfoPanel;
    [SerializeField] private DataStatsPanel dataStatsPanel;
    [SerializeField] private DataChildrenPanel dataChildrenPanel;
    [SerializeField] private DataAddChildPanel dataAddChildPanel;
    [SerializeField] private DataExitPanel dataExitPanel;
    [SerializeField] private DataSectionsPanel dataSectionsPanel;
    [SerializeField] private DataMissionsPanel dataSetMissionsPanel;
    [SerializeField] private DataSetLevelsPanel dataSetLevelsPanel;

    private ITextPanel iTextInfo;
    private Panel addChildPanel;
    private Panel missionsPanel;
    private Panel levelsPanel;
    private Panel childrenPanel;
    private Panel menuPanel;

    #endregion

    protected override void Start()
    {
        Panel.onSetInfoPanel += SetTextInfo;
        DataMenuPanel.onCheckChild += ShowAddChildPanel;
        DataAddChildPanel.onCheckEmptyPanel += CheckEmptyPanel;
        InitPanels();
        base.Start();
    }
    
    private void OnDestroy()
    {
        Panel.onSetInfoPanel -= SetTextInfo;
        DataMenuPanel.onCheckChild -= ShowAddChildPanel;
        DataAddChildPanel.onCheckEmptyPanel -= CheckEmptyPanel;
    }

    private void CheckEmptyPanel()
    {
        if (childrenPanel.PanelObject.activeSelf == false)
        {
            menuPanel.ShowPanel();
        }
    }

    protected override void InitPanels()
    {
        base.InitPanels();
        #region Auto_Generated_Code_Placement_Init

        new UIManagerBridge<DataInfoPanel>(new UiReceiver(), dataInfoPanel);
        
        menuPanel = new MenuPanel(dataPanelDict["MenuPanel"], dataMenuPanel, PanelTypes.Main);
        Panel statsPanel = new StatsPanel(dataPanelDict["StatsPanel"], dataStatsPanel, PanelTypes.Main);
        childrenPanel = new ChildrenPanel(dataPanelDict["ChildrenPanel"], dataChildrenPanel, PanelTypes.Main);
        levelsPanel = new LevelsPanel(dataPanelDict["LevelsPanel"], dataSetLevelsPanel, PanelTypes.Main);
        Panel sectionsPanel = new SectionsPanel(dataPanelDict["SectionsPanel"], dataSectionsPanel, PanelTypes.Main);
        missionsPanel = new MissionsPanel(dataPanelDict["MissionsPanel"], dataSetMissionsPanel, PanelTypes.Main);

        Panel settingsPanel = new SettingsPanel(dataPanelDict["SettingsPanel"], dataSettingsPanel, PanelTypes.Secondary);
        Panel exitPanel = new ExitPanel(dataPanelDict["ExitPanel"], dataExitPanel, PanelTypes.Secondary);
        addChildPanel = new AddChildPanel(dataPanelDict["AddChildPanel"], dataAddChildPanel, PanelTypes.Secondary);
        Panel infoPanel = new InfoPanel(dataPanelDict["InfoPanel"], dataInfoPanel, PanelTypes.Secondary);
        iTextInfo = (ITextPanel) infoPanel;

        UiTransitionDict = new Dictionary<Panel, Dictionary<Panel, TransitionTypes>>
        {
            {
                menuPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {statsPanel, TransitionTypes.Hard},
                    {childrenPanel, TransitionTypes.Hard},
                    {sectionsPanel, TransitionTypes.Hard},
                    {exitPanel, TransitionTypes.Soft},
                    {settingsPanel, TransitionTypes.Soft}
                }
            },
            {
                statsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {menuPanel, TransitionTypes.Hard},
                }
            },
            {
                childrenPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {menuPanel, TransitionTypes.Hard},
                    {addChildPanel, TransitionTypes.Soft}
                }
            },
            {
                addChildPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    
                }
            },
            {
                settingsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {menuPanel, TransitionTypes.Normal},
                }
            },
            {
                infoPanel,
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
                    {menuPanel, TransitionTypes.Hard},
                }
            },
            {
                sectionsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {menuPanel, TransitionTypes.Hard},
                    {missionsPanel, TransitionTypes.Hard}
                }
            },
            {
                missionsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {sectionsPanel, TransitionTypes.Hard},
                    {levelsPanel, TransitionTypes.Hard},
                    {infoPanel, TransitionTypes.Soft}
                }
            },
            {
                levelsPanel,
                new Dictionary<Panel, TransitionTypes>
                {
                    {missionsPanel, TransitionTypes.Hard},
                    {infoPanel, TransitionTypes.Soft}
                }
            },
        };

        #endregion
    }

    private void SetTextInfo(string nameActivePanel = "")
    {
        string text = "";
        
        if (nameActivePanel == "Missions")
        {
            text = dataInfoPanel.TextIdSectionsList[DataGame.IdSelectSection].TextSection;
        }
        else if (nameActivePanel == "Levels")
        {
            text = dataInfoPanel.InfoText.text = dataInfoPanel
                .TextIdSectionsList[DataGame.IdSelectSection]
                .TextMissionsList[DataGame.IdSelectMission].TextMission;
        }

        iTextInfo.SetTextInfo(text);
    }

    private void ShowAddChildPanel()
    {
        addChildPanel.ShowPanel();
    }
}

