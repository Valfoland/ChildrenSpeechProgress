using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Section0.HomeLevels.Level1
{
    public class Level1 : MonoBehaviour
    {
        [SerializeField] private BoxLevel1[] boxLevels;
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

        public static Action<bool> onEndLevel;
        
        private void Start()
        {
            firstPartMessage = textMessage.text;
            ILevelData data = new DataLevel1Manager();
            data.InitData();
            ReShapeSprites();
            BoxLevel1.onClickBox += CheckBox;
        }

        private void OnDestroy()
        {
            BoxLevel1.onClickBox -= CheckBox;
        }

        public void CheckBox(char letterBox, BoxLevel1 boxLevel1)
        {
            if (letterBox == needLetter)
            {
                AttemptCounter.SetAttempt(true);
                boxLevel1.BtnBox.interactable = false;
                countSelectNeedBox++;

                if (countSelectNeedBox >= COUNT_BOX_HALF)
                {
                    ReShapeSprites();
                }
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                boxLevel1.AnimBox();
            }
        }

        private void ReShapeSprites()
        {
            if (currentIdPack < DataLevelManager.DataLevelDict.Count)
            {
                countSelectNeedBox = 0;
                countInstanceNeedBox = 0;
                countInstanceOtherBox = 0;
                currentIdPack++;

                SetCurrentLetter();
                SetDictSprite();

                foreach (var box in boxLevels)
                {
                    char randLetter = GetRandomLetterBox();
                    Sprite sprite = GetRandomSprite(randLetter);
                    spriteListBox.Add(sprite);
                    box.SetDataBox(sprite, randLetter);
                    box.BtnBox.interactable = true;
                }
            }
            else
            {
                if (currentRound >= 1)
                {
                    if (AttemptCounter.IsLevelPass())
                    {
                        onEndLevel?.Invoke(true);
                    }
                    else
                    {
                        onEndLevel?.Invoke(false);
                    }
                }
                else
                {
                    currentIdPack = 0;
                    currentRound++;
                    ReShapeSprites();
                }
            }
        }

        private void SetCurrentLetter()
        {
            currentName = DataLevelManager.DataNameList.Dequeue();
            DataLevelManager.DataNameList.Enqueue(currentName);
            var nameListSprite = currentName.Replace("-", "");
            needLetter = nameListSprite[currentRound];
            otherLetter = nameListSprite.Replace(nameListSprite[currentRound].ToString(), "")[0];
            textMessage.text = $"{firstPartMessage} {nameListSprite[currentRound].ToString().ToUpper()}";
        }

        private Sprite GetRandomSprite(char inputLetter)
        {
            var sprite = spriteDictLetter[inputLetter][Random.Range(0, spriteDictLetter[inputLetter].Count)];
            spriteDictLetter[inputLetter].Remove(sprite);
            return sprite;
        }

        private void SetDictSprite()
        {
            spriteDictLetter.Clear();
            spriteDictLetter.Add(needLetter, new List<Sprite>());
            spriteDictLetter.Add(otherLetter, new List<Sprite>());
            
            foreach (var data in DataLevelManager.DataLevelDict[currentName])
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
