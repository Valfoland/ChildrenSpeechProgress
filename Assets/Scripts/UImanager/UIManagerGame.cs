using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bridge.BridgeReceiver;

public class UiManagerGame : MonoBehaviour
{
    [Header("Панель выхода из игры")]
    [SerializeField] private DataSetExitPanel dataSetExitPanel;
    [Header("Панель переходов на новый уровень")] 
    [SerializeField] private DataSetEndGamePanel dataSetEndGamePanel;

    private Panel nextLvlPanel;
    private Panel losePanel;
    private Panel winPanel;
    private Panel infoPanel;
    
    private void Start()
    {
        InitPanels();
    }

    private void InitPanels()
    {
        Panel tempPanel = new EndGamePanel(dataSetEndGamePanel);
        nextLvlPanel = new NextLvlPanel(tempPanel, dataSetEndGamePanel);
        losePanel = new LosePanel(tempPanel, dataSetEndGamePanel);
        winPanel = new WinPanel(tempPanel, dataSetEndGamePanel);
        infoPanel = new InfoPanel(UiReceiver.DataSetInfoPanel);
    }
    
    private void EndGame()
    {
       winPanel.ShowPanel();
    }

    private void EndLvl(bool isWin)
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


