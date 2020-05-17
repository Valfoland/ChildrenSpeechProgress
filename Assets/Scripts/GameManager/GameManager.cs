using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static System.Action onSetCompletionLevel;
    [SerializeField] private UiTransitionManagerGameData uiTransitionManagerGameData;

    private void Start()
    {
        LevelManager.onEndLevel += EndLevel;
    }

    private void OnDestroy()
    {
        LevelManager.onEndLevel -= EndLevel;
    }

    private void EndLevel(bool isWin)
    {
        if (isWin)
        {
            if (DataGame.IdSelectLvl <
                DataGame.CountSections[DataGame.IdSelectSection].CountMissions[DataGame.IdSelectMission].CountLevels - 1)
            {
                NextLevel();
            }
            else
            {
                EndGame();
            }
            
        }
        else
        {
            Restart();
        }
    }

    private void NextLevel()
    {
        SetCompletedLvl();
        uiTransitionManagerGameData.EndLvl(true);
    }

    private void EndGame()
    {
        SetCompletedLvl();
        uiTransitionManagerGameData.EndGame();
    }

    private void Restart()
    {
        uiTransitionManagerGameData.EndLvl(false);
    }

    private void SetCompletedLvl()
    {
        Child.CurrentChildrenData.CompletedLevels
            [$"{DataGame.IdSelectSection}{DataGame.IdSelectMission}"]
            [DataGame.IdSelectLvl] = true;
        onSetCompletionLevel?.Invoke();
    }
}
