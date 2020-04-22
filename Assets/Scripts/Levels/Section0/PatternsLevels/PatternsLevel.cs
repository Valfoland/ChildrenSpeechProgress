using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Section0.PatternsLevel
{
    public class PatternsLevel : LevelManager
    {
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private Transform[] circleParent;
        [SerializeField] private Button[] voiceBtns;
        private int countTrueWords;
        
        private void Start()
        {
            InitData();
            InitItems();
        }

        private void InitData()
        {
            ItemPatternsLevel.onClickBox += CheckBox;
            ILevelData data = new DataPatternsLevelManager();
            data.InitData();
        }

        private void OnDestroy()
        {
            ItemPatternsLevel.onClickBox -= CheckBox;
        }

        private void InitItems()
        {
            InstanceCircles();
        }

        private void InstanceCircles()
        {
            var word = DataPatternsLevelManager.WordsWithoutSounds;
            for (int i = 0; i < DataPatternsLevelManager.WordsWithoutSounds.Count; i++)
            {
                for (int j = 0; j < word[i].Length; j++)
                {
                    var circleObject = Instantiate(circlePrefab, circleParent[i]);
                    if (word[i][j] != '_')
                    {
                        circleObject.GetComponent<ItemPatternsLevel>()
                            .SetDataBox(i, j, DataPatternsLevelManager.SoundsLevel);
                    }
                    else
                    {
                        circleObject.GetComponent<ItemPatternsLevel>()
                            .SetDataBox(i, j, DataPatternsLevelManager.SoundsLevel,
                                word[i][j].ToString());
                    }
                }
            }
        }

        private void Voice(string data)
        {
            onVoice?.Invoke(data);
        }
        
        private void CheckBox(ItemPatternsLevel prevBox, ItemPatternsLevel currentBox)
        {
            if (prevBox == currentBox)
            {
                StringBuilder currentWordBuilder = 
                    new StringBuilder(DataPatternsLevelManager.WordsWithoutSounds[currentBox.Line]);
                string currentWord = currentWordBuilder.Replace('_', currentBox.CurrentSound, currentBox.PosInWord,
                    currentBox.PosInWord + 1).ToString();

                if (currentWord == DataPatternsLevelManager.WordsLevel[currentBox.Line])
                {
                    AttemptCounter.SetAttempt(true);
                    countTrueWords++;

                    if (countTrueWords >= DataPatternsLevelManager.WordsLevel.Count)
                    {
                        CheckWinLevel();
                    }
                }
            }
            else
            {
                currentBox.AnimBox();
                AttemptCounter.SetAttempt(false);
            }
            
        }
        
        private void CheckWinLevel()
        {
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
        }
    }
}
