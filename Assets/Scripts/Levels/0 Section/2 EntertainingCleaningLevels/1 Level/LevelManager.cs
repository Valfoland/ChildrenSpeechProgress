using System;
using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;
using Section1.MissionsDecorator;
using Random = UnityEngine.Random;

namespace Section0.EntertainingCleaningLevels.Level1
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] private int countRounds;
        [SerializeField] private MissionsDecorator.ItemLevel[] itemLevel;
        [SerializeField] private LevelDialogueData levelDialogueData;
        
        private LevelDialogue levelDialogue;
        private DataLevelManager dataLevelManager;
        private List<KeyValuePair<string, string>> itemList = new List<KeyValuePair<string, string>>();
        private int currentRound;
        private string needWord;

        protected override void Start()
        {
            base.Start();
            InitData();
            StartDialogue();
        }

        private void OnDestroy()
        {
            MissionsDecorator.ItemLevel.onClickBox -= CheckBox;
            levelDialogue.onEndDialogue -= StartDialogue;
        }

        private void InitData()
        {
            dataLevelManager = new DataLevelManager(countRounds);
            levelDialogue = new LevelDialogue(levelDialogueData, dataLevelManager.DialogueDict);
            SetNeedWord();
            
            MissionsDecorator.ItemLevel.onClickBox += CheckBox;
            levelDialogue.onEndDialogue += StartDialogue;
        }
        
        protected override void StartDialogue()
        {
            if (currentIdSentences >= dataLevelManager.DialogueDict.Count)
            {
                StartLevel();
                currentIdSentences = 0;
                return;
            }    
            
            levelDialogue.VoiceSentenceDialogue(currentIdSentences);
            currentIdSentences++;
        }
        
        protected override void StartLevel()
        {
            VoiceCallBack(needWord, ReshapeItems);
        }

        private void SetNeedWord()
        {
            try
            {
                itemList.Clear();
                itemList.Add(dataLevelManager.NameItemsPair.Dequeue());
                itemList.Add(dataLevelManager.NameItemsPair.Dequeue());
            
                needWord = itemList[Random.Range(0, 2)].Key;
            }
            catch (InvalidOperationException e)
            {
            }
        }

        private void ReshapeItems()
        {
            if (currentRound < countRounds && itemList.Count == 2)
            {
                SetItems();
                currentRound++;
            }
            else
            {
                CheckWinLevel();
            }
        }
        
        private void SetItems()
        {
            itemLevel[0].gameObject.SetActive(true);
            itemLevel[1].gameObject.SetActive(true);
            
            var itemSprite = dataLevelManager.SpriteDict[itemList[0].Key][itemList[0].Value];
            itemLevel[0].SetDataBox(new KeyValuePair<string, Sprite>(itemList[0].Key, itemSprite));
            
            itemSprite = dataLevelManager.SpriteDict[itemList[1].Key][itemList[1].Value];
            itemLevel[1].SetDataBox(new KeyValuePair<string, Sprite>(itemList[1].Key, itemSprite));
        }

        private void CheckBox(string wordBox, MissionsDecorator.ItemLevel itemLevel)
        {
            if (wordBox == needWord)
            {
                AttemptCounter.SetAttempt(true);
                StartCoroutine(WaitReshape(2f));
                itemLevel.AnimBox(true);
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                Voice(needWord);
                itemLevel.AnimBox(false);
            }
        }

        private IEnumerator WaitReshape(float time)
        {
            yield return new WaitForSeconds(time);
            SetNeedWord();
            VoiceCallBack(needWord, ReshapeItems);
        }
    }
}
