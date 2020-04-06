using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DataSetEndGamePanel dataEndGamePanel;
    [SerializeField] private DataSetSettingsPanel dataSettings;
    [SerializeField] private DataSetChildPanel dataSetChildPanel;
    [SerializeField] private DataSetAddChildPanel dataSetAddChildPanel;

    public static System.Action<bool> onEndGame;

    private void Start()
    {
        new SettingsPanel(dataSettings);
        new ChildPanel(dataSetChildPanel);
        new AddChildPanel(dataSetAddChildPanel);
    }

    private void OnEnable()
    {
        GameManager.onEndGame += ShowEndGamePanel;
    }

    private void OnDisable()
    {
        GameManager.onEndGame -= ShowEndGamePanel;
    }

    public void ShowEndGamePanel()
    {
        new EndGamePanel(dataEndGamePanel);
    }

    public void PlayGame()
    {
        if (PlayerPrefs.HasKey("countChild"))
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            new ChildPanel(dataSetChildPanel, 0);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

