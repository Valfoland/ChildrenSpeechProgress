using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bridge.BridgeReceiver;

public class UiManagerGame : MonoBehaviour
{
    [Header("Панель инфо")] [SerializeField]
    private DataSetInfoPanel dataSetInfoPanel;
    [Header("Панель выхода из игры")] [SerializeField] 
    private DataSetExitPanel dataSetExitPanel;
    [Header("Панель конца уровня")] [SerializeField] 
    private DataSetEndGamePanel dataSetEndGamePanel;
    [Header("Панель новый уровень")] [SerializeField] 
    private DataSetLosePanel dataSetLosePanel;
    [Header("Панель новый уровень")] [SerializeField] 
    private DataSetWinPanel dataSetWinPanel;
    [Header("Панель новый уровень")] [SerializeField] 
    private DataSetNextLvlPanel dataSetNextLvlPanel;
    
    private Panel nextLvlPanel;
    private Panel losePanel;
    private Panel winPanel;

    private void Start()
    {
        InitPanels();
    }

    private void InitPanels()
    {
        dataSetInfoPanel.TextIdSectionsList = UiReceiver.DataSetInfoPanel.TextIdSectionsList;
        
        Panel tempPanel = new EndGamePanel(dataSetEndGamePanel);
        nextLvlPanel = new NextLvlPanel(tempPanel, dataSetEndGamePanel, dataSetNextLvlPanel);
        losePanel = new LosePanel(tempPanel, dataSetEndGamePanel, dataSetLosePanel);
        winPanel = new WinPanel(tempPanel, dataSetEndGamePanel, dataSetWinPanel);
        
        new InfoPanel(dataSetInfoPanel);
        new ExitPanel(dataSetExitPanel);
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
}

namespace Bridge.BridgeReceiver
{
    public class UiReceiver: IBridgeReciever<DataSetInfoPanel>
    {
        public static DataSetInfoPanel DataSetInfoPanel;
        public void TakeData(DataSetInfoPanel dataSetInfoPanel)
        {
            DataSetInfoPanel = dataSetInfoPanel;
        }
    }
}


