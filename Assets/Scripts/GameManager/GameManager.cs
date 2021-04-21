using Levels;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static System.Action onSetCompletionLevel;
    [SerializeField] private UiTransitionManagerGame uiTransitionManagerGame;
    [SerializeField] private LevelCreator levelCreator;
    
    private void Start()
    {
        LevelProduct.onEndLevel += EndLevel;
        UiTransitionManagerGame.onNextLvl += NextLevel;
        NextLevel();
    }

    private void OnDestroy()
    {
        LevelProduct.onEndLevel -= EndLevel;
        UiTransitionManagerGame.onNextLvl -= NextLevel;
    }

    private void EndLevel(bool isWin)
    {
        if (isWin)
        {
            SetCompletedLvl();
            if (DataGame.IdSelectLvl <
                DataGame.SectionDataList[DataGame.IdSelectSection]
                    .MissionDataList[DataGame.IdSelectMission]
                    .LevelDataList.Count - 1)
            {
                EndLevel();
            }
            else
            {
                EndMission();
            }
            
        }
        else
        {
            Restart();
        }
    }

    private void EndLevel()
    {
        uiTransitionManagerGame.EndLvl(true);
    }

    private void NextLevel()
    {
        levelCreator.CreateLevel();
    }
    
    private void EndMission()
    {
        uiTransitionManagerGame.EndGame();
    }

    private void Restart()
    {
        uiTransitionManagerGame.EndLvl(false);
    }

    private void SetCompletedLvl()
    {
        Child.CurrentChildData.CompletedLevels
            [$"{DataGame.IdSelectSection}{DataGame.IdSelectMission}"]
            [DataGame.IdSelectLvl] = true;
        onSetCompletionLevel?.Invoke();
    }
}
