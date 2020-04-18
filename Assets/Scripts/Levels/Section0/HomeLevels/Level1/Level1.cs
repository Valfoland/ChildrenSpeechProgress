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
        
        private static char currentLetter;
        private static string currentName;
        private static int countSelectNeedBox;
        private static int currentIdPack;
        
        private string firstPartMessage;
        private int countNeedBox = 0;
        private int countOtherBox = 0;
        
        private void Start()
        {
            firstPartMessage = textMessage.text;
            ReShapeSprites();
            BoxLevel1.onClickBox += CheckBox;
        }

        private void OnDestroy()
        {
            BoxLevel1.onClickBox -= CheckBox;
        }

        public void CheckBox(char letterBox, BoxLevel1 boxLevel1)
        {
            if (letterBox == currentLetter)
            {
                if (countSelectNeedBox >= boxLevels.Length / 2)
                {
                    ReShapeSprites();
                }
                AttemptCounter.SetAttempt(true);
                countSelectNeedBox++;
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                boxLevel1.AnimBox();
            }
        }
        
        private void ReShapeSprites()
        {
            SetCurrentLetter();
            foreach (var box in boxLevels)
            {
                char randLetter = GetRandomLetter();
                Sprite sprite = GetRandomSprite(randLetter);
                box.SetDataBox(sprite, randLetter);
            }
            
            if (currentIdPack < DataLevel1Manager.DataLevel1Dict.Count)
            {
                countSelectNeedBox = 0;
                countNeedBox = 0;
                countOtherBox = 0;
                currentIdPack++;
            }
            else
            {
                //TODO выиграли этот уровень
                if (AttemptCounter.IsLevelPass())
                {
                    //Выиграл
                }
                else
                {
                    //Проиграл
                }
            }
        }

        private void SetCurrentLetter()
        {
            currentName = DataLevel1Manager.DataNameList.Dequeue();
            DataLevel1Manager.DataNameList.Enqueue(currentName);
            string pairName = currentName.Replace("-", "");
            currentLetter = pairName[Random.Range(0, 2)];
            textMessage.text = $"{firstPartMessage} {currentLetter.ToString().ToUpper()}";
        }

        private Sprite GetRandomSprite(char inputLetter)
        {
            var levelList = DataLevel1Manager.DataLevel1Dict[currentName];
            var spriteList = new List<Sprite>();
            
            foreach (var data in levelList)
            {
                if (data.name.Contains(inputLetter) ||
                    data.name.Contains(inputLetter.ToString().ToUpper()))
                {
                    spriteList.Add(data);
                }
            }
            int rand = Random.Range(0, spriteList.Count);
            
            return spriteList[rand];
        }

        private char GetRandomLetter()
        {
            string pairName = currentName.Replace("-", "");
            char letter = pairName[Random.Range(0, 2)];
            if (letter == currentLetter)
            {
                if (countNeedBox >= boxLevels.Length / 2)
                {
                    foreach (char name in currentName)
                    {
                        if (letter != name)
                        {
                            letter = name;
                            break;
                        }
                    }
                }
                else
                    countNeedBox++;
            }
            else
            {
                if (countOtherBox >= boxLevels.Length / 2)
                    letter = currentLetter;
                else
                    countOtherBox++;
            }
            return letter;
        }
    }
}
