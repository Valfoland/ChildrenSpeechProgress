using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Levels;

namespace Section0.HomeLevels.Level0
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] private ItemLevel[] itemLevels;
        [SerializeField] private Text textMessage;
        
        private string firstPartMessage;
        private string currentName;
        private char needLetter;
        private char otherLetter;
        private int countSelectNeedBox;
        private int currentIdPack;
        private int currentRound;
        private int countInstanceNeedBox;
        private int countInstanceOtherBox;
        private const int COUNT_BOX_HALF = 3;

        private List<Sprite> spriteListBox = new List<Sprite>();
        private Dictionary<char, List<Sprite>> spriteDictLetter = new Dictionary<char, List<Sprite>>();
        private bool isNotStart;

        private string helpWord = "звук";
        private DataLevelManager dataLevelManager;

        private void Start()
        {
            InitData();
            ReshapeItems();
        }

        private void OnDestroy()
        {
            ItemLevel.onClickBox -= CheckCorrectChoiceBox;
        }

        private void InitData()
        { 
            dataLevelManager = new DataLevelManager();
            ItemLevel.onClickBox += CheckCorrectChoiceBox;
            SetTextMessage();
        }

        private void SetTextMessage()
        {
            textMessage.text = firstPartMessage = dataLevelManager.StartSentence;
        }
        
        private void ReshapeItems()
        {
            if (currentIdPack < dataLevelManager.LevelSpriteDict.Count)
            {
                countSelectNeedBox = 0;
                countInstanceNeedBox = 0;
                countInstanceOtherBox = 0;
                currentIdPack++;

                SetCurrentLetter();
                SetDictSprite();
                
                if (isNotStart == false)
                {
                    Voice(firstPartMessage);
                    Voice(needLetter.ToString());
                    isNotStart = true;
                }
                else
                {
                    Voice(helpWord);
                    Voice(needLetter.ToString());
                }
                
                foreach (var box in itemLevels)
                {
                    char randLetter = GetRandomLetterBox();
                    Sprite sprite = GetRandomSprite(randLetter);
                    if (sprite == null)
                    {
                        CheckWinLevel();
                        break;
                    }
                    spriteListBox.Add(sprite);
                    box.SetDataBox(sprite, randLetter);
                    box.BtnBox.interactable = true;
                }
            }
            else
            {
                CheckWinLevel();
            }
        }
        
        private void CheckCorrectChoiceBox(char letterBox, ItemLevel itemLevel, string spriteName)
        {
            if (letterBox == needLetter)
            {
                AttemptCounter.SetAttempt(true);
                itemLevel.BtnBox.interactable = false;
                countSelectNeedBox++;

                if (countSelectNeedBox >= COUNT_BOX_HALF)
                {
                    ReshapeItems();
                }
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                itemLevel.AnimBox();
            }

            //Voice(spriteName);
        }
        
        protected override void CheckWinLevel()
        {
            if (currentRound >= 1)
            {
                base.CheckWinLevel();
            }
            else
            {
                currentIdPack = 0;
                currentRound++;
                ReshapeItems();
            }
        }

        private void SetCurrentLetter()
        {
            currentName = dataLevelManager.LevelKeySpriteList.Dequeue();
            dataLevelManager.LevelKeySpriteList.Enqueue(currentName);
            var nameListSprite = currentName.Replace("-", "");
            needLetter = nameListSprite[currentRound];
            otherLetter = nameListSprite.Replace(nameListSprite[currentRound].ToString(), "")[0];
            textMessage.text = $"{firstPartMessage} {nameListSprite[currentRound].ToString().ToUpper()}";
            onVoice?.Invoke(needLetter.ToString());
        }

        private Sprite GetRandomSprite(char inputLetter)
        {
            try
            {
                var sprite = spriteDictLetter[inputLetter][Random.Range(0, spriteDictLetter[inputLetter].Count)];
                spriteDictLetter[inputLetter].Remove(sprite);
                return sprite;
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }

            return null;
        }

        private void SetDictSprite()
        {
            spriteDictLetter.Clear();
            spriteDictLetter.Add(needLetter, new List<Sprite>());
            spriteDictLetter.Add(otherLetter, new List<Sprite>());

            foreach (var data in dataLevelManager.LevelSpriteDict[currentName])
            {
                
                if (data.name.Contains(needLetter) ||
                    data.name.Contains(needLetter.ToString().ToUpper()))
                {
                    spriteDictLetter[needLetter].Add(data);
                }
                else
                {
                    spriteDictLetter[otherLetter].Add(data);
                }
            }
        }

        public void ClickButtonVoice()
        {
            Voice(needLetter.ToString());
        }

        private char GetRandomLetterBox()
        {
            var pairName = currentName.Replace("-", "");
            var letter = pairName[Random.Range(0, 2)];
            
            if (letter == needLetter)
            {
                if (countInstanceNeedBox >= COUNT_BOX_HALF)
                    letter = otherLetter;
                else
                    countInstanceNeedBox++;
            }
            else
            {
                if (countInstanceOtherBox >= COUNT_BOX_HALF)
                    letter = needLetter;
                else
                    countInstanceOtherBox++;
            }
            return letter;
        }
    }
}
