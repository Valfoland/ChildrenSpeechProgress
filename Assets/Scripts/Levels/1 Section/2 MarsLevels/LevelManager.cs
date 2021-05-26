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
        protected override void Start()
        {
            base.Start();
            InitData();
            StartDialogue();
        }
 
        private void OnDestroy()
        {
            MissionsDecorator.ItemLevel.onClickBox -= CheckBox;
        }

        protected override void InitData()
        {
            MissionsDecorator.ItemLevel.onClickBox += CheckBox;
            dataLevelManager = new DataLevelManager(countRounds);
            base.InitData();
        }
    }
}
