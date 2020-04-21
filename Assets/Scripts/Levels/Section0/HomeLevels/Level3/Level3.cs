﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.HomeLevels.Level3
{
    public class Level3 : LevelManager
    {
        [SerializeField] private BoxLevel3[] boxLevel3;
        [SerializeField] private Text textMessage;
        
        private int currentIdPack;
        private int countNeedSprite;
        private string needWord;
        private string otherWord;
        private string currentSentence;
        
        private List<Sprite> spriteList = new List<Sprite>();
        
        private void Start()
        {
            InitData();
            Voice(DataLevel3Manager.QueueSenteceses.Peek());
            ReshapeItems();
        }

        private void InitData()
        {
            BoxLevel3.onClickBox += CheckBox;
            ILevelData data = new DataLevel3Manager();
            data.InitData();
        }

        private void OnDestroy()
        {
            BoxLevel3.onClickBox -= CheckBox;
        }

        private void ReshapeItems()
        {
            countNeedSprite = 0;

            if (currentIdPack < DataLevel3Manager.QueueSenteceses.Count)
            {
                currentIdPack++;
                GetSentence();
                GetRandomSprites();

                int i = 0;
                foreach (var box in boxLevel3)
                {
                    box.SetDataBox(spriteList[i], spriteList[i].name);
                    i++;
                }
                
            }
            else
            {
                CheckWinLevel();
            }
        }

        private void Voice(string data)
        {
            onVoice?.Invoke(data);
        }

        private void GetSentence()
        {
            currentSentence = DataLevel3Manager.QueueSenteceses.Dequeue();
            DataLevel3Manager.QueueSenteceses.Enqueue(currentSentence);
            SetTextMessage(currentSentence);
        }

        private void GetRandomSprites()
        {
            spriteList  = DataLevel3Manager.QueueSprites.Dequeue();
            DataLevel3Manager.QueueSprites.Enqueue(spriteList);
            bool[] mixStates = {true, false};
            bool toMix = mixStates[Random.Range(0, 2)];
            
            if (toMix)
            {
                var temp = spriteList[0];
                spriteList[0] = spriteList[1];
                spriteList[1] = temp;
            }

            needWord = toMix ? spriteList[1].name: spriteList[0].name;
            otherWord = toMix ? spriteList[0].name: spriteList[1].name;
        }

        private void CheckBox(string wordBox, BoxLevel3 boxLevel3)
        {
            if (wordBox == needWord)
            {
                AttemptCounter.SetAttempt(true);
                Voice(currentSentence + "/" + needWord);
                var newSentence = currentSentence.Replace("...", " " + needWord.ToLower());
                SetTextMessage(newSentence);
                StartCoroutine(WaitReshape());
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                boxLevel3.AnimBox();
            }
        }
        
        private void CheckWinLevel()
        {
            currentIdPack = 0;
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
        }

        private void SetTextMessage(string msg)
        {
            textMessage.text = msg;
        }

        private IEnumerator WaitReshape()
        {
            yield return  new WaitForSeconds(1f); //TEMP
            ReshapeItems();
        }
    }
}
