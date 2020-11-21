﻿    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section0.PatternsLevel
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private Animation[] animField;
        [SerializeField] private Transform[] circleParent;
        [SerializeField] private Button[] voiceBtns;
        [SerializeField] private Image[] checkerImages;

        [SerializeField] private Sprite trueSprite;
        [SerializeField] private Sprite falseSprite;
        private int countTrueWords;

        private DataLevelManager dataLevelManager;

        private void Start()
        {
            InitData();
            InitItems();
        }

        private void OnDestroy()
        {
            ItemLevel.onClickBox -= CheckBox;
        }
        
        private void InitData()
        {
            ItemLevel.onClickBox += CheckBox;
            dataLevelManager = new DataLevelManager();

            for (int i = 0; i < voiceBtns.Length; i++)
            {
                var i1 = i;
                voiceBtns[i].onClick.AddListener(() => Voice(dataLevelManager.WordsLevel[i1]));
            }
        }

        private void InitItems()
        {
            InstanceCircles();
        }

        private void InstanceCircles()
        {
            var word = dataLevelManager.WordsWithoutSounds;
            for (int i = 0; i < dataLevelManager.WordsWithoutSounds.Count; i++)
            {
                for (int j = 0; j < word[i].Length; j++)
                {
                    var circleObject = Instantiate(circlePrefab, circleParent[i]);
                    if (word[i][j] != '_')
                    {
                        circleObject.GetComponent<ItemLevel>()
                            .SetDataBox(i, j, dataLevelManager.SoundsLevel,
                                word[i][j].ToString());
                    }
                    else
                    {
                        circleObject.GetComponent<ItemLevel>()
                            .SetDataBox(i, j, dataLevelManager.SoundsLevel);
                    }
                }
            }
        }
        
        private void CheckBox(ItemLevel prevBox, ItemLevel currentBox)
        {
            var word = dataLevelManager
                .WordsWithoutSounds[currentBox.Line]
                .Remove(currentBox.PosInWord, 1);
            dataLevelManager.WordsWithoutSounds[currentBox.Line] =
                word.Insert(currentBox.PosInWord, currentBox.CurrentSound.ToString());

            if (dataLevelManager.WordsWithoutSounds[currentBox.Line] ==
                dataLevelManager.WordsLevel[currentBox.Line])
            {

                SetCheckWordImage(currentBox.Line, true);
                currentBox.CallInteractable(currentBox.Line);
                AttemptCounter.SetAttempt(true);
                countTrueWords++;
                
                if (countTrueWords >= dataLevelManager.WordsLevel.Count)
                {
                    CheckWinLevel();
                }
            }

            if (prevBox != null && 
                prevBox.transform.parent != currentBox.transform.parent && 
                dataLevelManager.WordsWithoutSounds[prevBox.Line] !=
                dataLevelManager.WordsLevel[prevBox.Line])
            {
                AnimField(prevBox.Line);
                SetCheckWordImage(prevBox.Line, false);
                AttemptCounter.SetAttempt(false);
            }
        }

        private void AnimField(int line)
        {
            animField[line].Play();
        }

        private void SetCheckWordImage(int line, bool isTrue)
        {
            if (checkerImages[line].gameObject.activeSelf == false)
            {
                checkerImages[line].gameObject.SetActive(true);
            }

            if (isTrue)
            {
                checkerImages[line].sprite = trueSprite;
                checkerImages[line].color = new Color(0,0.5f,0,1);
            }
            else
            {
                checkerImages[line].sprite = falseSprite;
                checkerImages[line].color = new Color(0.5f,0,0,1);
            }
        }
    }
}