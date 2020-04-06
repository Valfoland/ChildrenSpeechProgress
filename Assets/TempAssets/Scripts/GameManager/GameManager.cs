using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static System.Action onEndGame;
    public static System.Action<bool> onInstanceObjects;

    [SerializeField] private Text currentScoreText;
    private int currentScore;

    [Header("Количество раундов")]
    [SerializeField] private int countOfRounds = 3;
    [Space(1)]
    private static int currentRound;

    private void Start()
    {
        currentRound = 0;
        InstanceObjects(true);
    }

    public void InstanceObjects(bool isFirstInstance)
    {
        if (currentRound < countOfRounds)
        {
            onInstanceObjects?.Invoke(isFirstInstance);
            currentRound++;
        }
        else
        {
            EndGame();
        }
    }

    public void IterScore()
    {
        currentScore++;
        currentScoreText.text = currentScore.ToString();
    }

    private void EndGame()
    {

        if (PlayerPrefs.GetInt(PlayerPrefs.GetInt("ChooseChild").ToString() + "ScoreChild") <= currentScore)
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetInt("ChooseChild").ToString() + "ScoreChild", currentScore);
        }
        onEndGame?.Invoke();
    }
}
