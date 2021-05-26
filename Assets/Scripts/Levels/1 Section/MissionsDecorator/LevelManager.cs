using System;
using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;
using Random = UnityEngine.Random;

namespace Section1.MissionsDecorator
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] protected int countRounds;
        [SerializeField] protected ItemLevel[] itemLevel;
        [SerializeField] protected LevelDialogueData levelDialogueData;
        
        protected DataLevelManager dataLevelManager;
        private LevelDialogue levelIntroDialogue;
        private int currentRound;
        private string needWord;

        protected virtual void InitData()
        {
            voiceButton.onClick.AddListener(ClickButtonVoice);
            levelIntroDialogue = new LevelDialogue(levelDialogueData, dataLevelManager.DialogueIntroDict);
            SetCurrentWord();
            
            levelIntroDialogue.onEndDialogue += StartDialogue;
        }
        
        private void OnDestroy()
        {
            levelIntroDialogue.onEndDialogue -= StartDialogue;
        }

        private void SetCurrentWord()
        {
            needWord = dataLevelManager.NameItemsPair[currentRound].Key;
        }
        
        protected override void StartDialogue()
        {
            if (currentIdSentences >= dataLevelManager.DialogueIntroDict.Count)
            {
                StartLevel();
                currentIdSentences = 0;
                return;
            }    
            
            currentIdSentences++;
            levelIntroDialogue.VoiceSentenceDialogue(currentIdSentences - 1);
        }

        protected override void StartLevel()
        {
            ReshapeItems();
        }
        
        private void ReshapeItems()
        {
            if (currentRound < countRounds)
            {
                var mainId = Random.Range(0, itemLevel.Length);
                SetItems(mainId);
                SetCurrentWord();
                levelDialogueData.TextDialog.text = needWord;
                Voice(needWord);
                currentRound++;
            }
            else
            {
                CheckWinLevel();
            }
        }
        
        private void ClickButtonVoice()
        {
            Voice(needWord);
        }

        private void SetItems(int mainId)
        {
            for (int i = 0; i < itemLevel.Length; i++)
            {
                var id = currentRound;
                
                if (i != mainId)
                {
                    while (id == currentRound)
                    {
                        id = Random.Range(0, dataLevelManager.NameItemsPair.Count);
                    }
                }
                
                var itemPair = dataLevelManager.NameItemsPair[id];
                var itemSprite = dataLevelManager.SpriteDict[itemPair.Key][itemPair.Value];
                itemLevel[i].SetDataBox(new KeyValuePair<string, Sprite>(itemPair.Key, itemSprite));
            }
        }

        protected virtual void CheckBox(string wordBox, ItemLevel itemLevel)
        {
            if (wordBox == needWord)
            {
                AttemptCounter.SetAttempt(true);
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                itemLevel.AnimBox();
            }

            StartCoroutine(WaitReshape(2f));
            Voice(needWord);
        }

        private IEnumerator WaitReshape(float time)
        {
            yield return new WaitForSeconds(time);
            ReshapeItems();
        }
    }
}
