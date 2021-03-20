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
        private Dictionary<char, Dictionary<string, Sprite>> spriteDictLetter = new Dictionary<char, Dictionary<string, Sprite>>();
        private bool isNotStart;

        private string helpWord = "звук";
        private DataLevelManager dataLevelManager;

        protected override void Start()
        {
            base.Start();
            InitData();
            ReshapeItems();
        }

        private void OnDestroy()
        {
            ItemLevel.onClickBox -= CheckCorrectChoiceBox;
        }

        private void InitData()
        { 
            ItemLevel.onClickBox += CheckCorrectChoiceBox;
            dataLevelManager = new DataLevelManager();
            voiceButton.onClick.AddListener(ClickButtonVoice);
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
                    var sprite = GetRandomSprite(randLetter);
                    
                    if (sprite.Value == null)
                    {
                        CheckWinLevel();
                        break;
                    }
                    spriteListBox.Add(sprite.Value);
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

            Voice(spriteName);
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

        private KeyValuePair<string, Sprite> GetRandomSprite(char inputLetter)
        {
            var sprite = new KeyValuePair<string, Sprite>();
            try
            {
                sprite = spriteDictLetter[inputLetter].ToList()[Random.Range(0, spriteDictLetter[inputLetter].Count)];
                spriteDictLetter[inputLetter].Remove(sprite.Key);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
            return sprite;
        }

        private void SetDictSprite()
        {
            spriteDictLetter.Clear();
            spriteDictLetter.Add(needLetter, new Dictionary<string, Sprite>());
            spriteDictLetter.Add(otherLetter, new Dictionary<string, Sprite>());

            foreach (var data in dataLevelManager.LevelSpriteDict[currentName])
            {
                if (data.Key.Contains(needLetter) ||
                    data.Key.Contains(needLetter.ToString().ToUpper()))
                {
                    spriteDictLetter[needLetter].Add(data.Key, data.Value);
                }
                else
                {
                    spriteDictLetter[otherLetter].Add(data.Key, data.Value);
                }
            }
        }

        private void ClickButtonVoice()
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
