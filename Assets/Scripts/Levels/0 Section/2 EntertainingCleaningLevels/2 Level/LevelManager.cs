using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;
using Section0.EntertainingCleaningLevels.MissionsDecorator;

namespace Section0.EntertainingCleaningLevels.Level2
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
            dataLevelManager = new  MissionsDecorator.DataLevelManager(countRounds);
        }
    }
}
