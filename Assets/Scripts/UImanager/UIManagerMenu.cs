using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour
{
    [Header("Панель меню")]
    [SerializeField] private DataSetMenuPanel dataSetMenuPanel;
    [Header("Панель настроек")]
    [SerializeField] private DataSetSettingsPanel dataSettings;
    [Header("Панель информации")]
    [SerializeField] private DataSetInfoPanel dataSetInfo;
    [Header("Панель статистики")]
    [SerializeField] private DataSetStatsPanel dataSetStats;
    [Header("Панель детей")]
    [SerializeField] private DataSetChildrenPanel dataSetChildrenPanel;
    [Header("Панель добавления ребенка")]
    [SerializeField] private DataSetAddChildPanel dataSetAddChildPanel;
    [Header("Панель выхода из игры")]
    [SerializeField] private DataSetExitPanel dataSetExitPanel;
    [Header("Панель разделов")]
    [SerializeField] private DataSetSectionsPanel dataSetSectionsPanel;
    [Header("Панель миссий")]
    [SerializeField] private DataSetMissionsPanel dataSetMissionsPanel;
    [Header("Панель уровней")]
    [SerializeField] private DataSetLevelsPanel dataSetLevelsPanel;

    private Panel panelMenu;
    private Panel panelAddChild;
    private Panel panelSections;
    private Panel panelMissions;
    private Panel panelLevels;
    private ITextPanel panelInfo;
    
    private void Start()
    {
        new SettingsPanel(dataSettings);
        new ChildrenPanel(dataSetChildrenPanel);
        new ExitPanel(dataSetExitPanel);
        new StatsPanel(dataSetStats);
        
        panelMenu = new MenuPanel(dataSetMenuPanel);
        panelAddChild = new AddChildPanel(dataSetAddChildPanel);
        panelSections = new SectionsPanel(dataSetSectionsPanel);
        panelMissions = new MissionsPanel(dataSetMissionsPanel);
        panelLevels = new LevelsPanel(dataSetLevelsPanel);
        panelInfo = new InfoPanel(dataSetInfo);
        InitActions();
    }

    private void InitActions()
    {
        panelMenu.onHidePanel += ClickChildrenPanel;
        panelSections.onHidePanel += ClickGoToMissions;
        panelMissions.onHidePanel += ClickGoToLevels;
        panelMissions.onShowPanel += SetTextInfo;
        panelLevels.onShowPanel += SetTextInfo;
        panelSections.onShowPanel += SetTextInfo;
    }

    private void ClickChildrenPanel()
    {
        if (!PlayerPrefs.HasKey("countChild"))
            panelAddChild.ShowPanel();
        else
            panelMenu.onHidePanel -= ClickChildrenPanel;
    }

    private void ClickGoToMissions() => panelMissions.ShowPanel();
    private void ClickGoToLevels() => panelLevels.ShowPanel();
    private void SetTextInfo(string nameActivePanel) => panelInfo.SetTextInfo(nameActivePanel);

}

