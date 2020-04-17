using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace HomeLevels.Level1
{
    public class Level1 : MonoBehaviour
    {
        [SerializeField] private BoxLevel1[] boxLevels;

        public static char CurrentLetter;
        private static string currentName;
        private static int countSelectNeedBox;
        private static int currentIdPack;
        
        private int countNeedBox = 0;
        private int countOtherBox = 0;
        
        private void Start()
        {
            BoxLevel1.onClickBox += CheckBox;
        }

        private void OnDestroy()
        {
            BoxLevel1.onClickBox -= CheckBox;
        }

        public void CheckBox(char letterBox, BoxLevel1 boxLevel1)
        {
            if (letterBox == CurrentLetter)
            {
                if (countSelectNeedBox >= boxLevels.Length / 2)
                {
                    ReShapeSprites();
                }
                countSelectNeedBox++;
            }
            else
            {
                //boxLevel1.animation... //TODO запускается анимация неправильного блока
            }
        }
        
        private void ReShapeSprites()
        {
            SetCurrentLetter();
            foreach (var box in boxLevels)
            {
                box.SetDataBox(GetRandomSprite(GetRandomLetter()), CurrentLetter);
            }
            
            if (currentIdPack < DataLevel1Manager.DataLevel1Dict.Count - 1)
            {
                currentIdPack++;
            }
            else
            {
                //TODO выиграли этот уровень
            }
        }

        private void SetCurrentLetter()
        {
            currentName = DataLevel1Manager.DataNameList.Dequeue();
            DataLevel1Manager.DataNameList.Enqueue(currentName);
            CurrentLetter = currentName.Replace("-", "")[Random.Range(0, 2)];
        }

        private Sprite GetRandomSprite(char inputLetter)
        {
            var levelDict = DataLevel1Manager.DataLevel1Dict[currentName];
            var spriteList = levelDict.Where(x =>
                x.name.Contains(inputLetter) ||
                x.name.Contains(inputLetter.ToString().ToUpper())) as List<Sprite>;
            return spriteList?[Random.Range(0, spriteList.Count)];
        }

        private char GetRandomLetter()
        {
            char letter = currentName.Replace("-", "")[Random.Range(0, 2)];
                
            if (letter == CurrentLetter)
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
                    letter = CurrentLetter;
                else
                    countOtherBox++;
            }

            return letter;
        } 
    }
}
