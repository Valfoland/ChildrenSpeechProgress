﻿using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;
using Section1.MissionsDecorator;

namespace Section1.HelpAstronautLevels.Level0
{
    public class LevelManager : MissionsDecorator.LevelManager
    {
        private void Start()
        {
            InitData();
            ReshapeItems();
        }
        
        private void OnDestroy()
        {
            MissionsDecorator.ItemLevel.onClickBox -= CheckBox;
        }

        private void InitData()
        {
            MissionsDecorator.ItemLevel.onClickBox += CheckBox;
            dataLevelManager = new DataLevelManager(countRounds);
        }
    }
}
