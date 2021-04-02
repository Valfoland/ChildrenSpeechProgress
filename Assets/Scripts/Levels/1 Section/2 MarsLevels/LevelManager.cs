using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section1.MarsLevels.Level0
{
    public class LevelManager : MissionsDecorator.LevelManager
    {
        private void Start()
        {
            InitData();
            StartLevel();
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
