﻿using System;
using System.Collections;
using System.Collections.Generic;
using Section0.HomeLevels.Level1;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiManagerGame uiManagerGame;

    private void Start()
    {
        Level1.onEndLevel += EndLevel;
    }

    private void OnDestroy()
    {
        Level1.onEndLevel -= EndLevel;
    }

    private void EndLevel(bool isWin)
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
