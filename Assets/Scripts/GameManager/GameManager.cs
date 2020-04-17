using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManagerGame uiManagerGame;
    
    public void EndLevel(bool isWin)
    {
        if (isWin)
        {
            if (DataTasks.IdSelectLvl <
                DataTasks.CountSections[DataTasks.IdSelectSection].CountMissions[DataTasks.IdSelectMission].CountLevels - 1)
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
        uiManagerGame.EndLvl(true);
    }

    private void EndGame()
    {
        SetCompletedLvl();
        uiManagerGame.EndGame();
    }

    private void Restart()
    {
        uiManagerGame.EndLvl(false);
    }

    private void SetCompletedLvl()
    {
        DataTasks
            .CountSections[DataTasks.IdSelectSection]
            .CountMissions[DataTasks.IdSelectMission]
            .CompletedLevels[DataTasks.IdSelectLvl]
            .isCompleted = true;
    }
}
