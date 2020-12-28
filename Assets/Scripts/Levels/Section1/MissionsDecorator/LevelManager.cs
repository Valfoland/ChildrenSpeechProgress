using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section1.MissionsDecorator
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] protected int countRounds;
        [SerializeField] protected ItemLevel[] itemLevel;
        [SerializeField] protected Text textMessage;

        protected DataLevelManager dataLevelManager;
        private int currentRound;
        private string needWord;
        

        protected void SetTextMessage(string msg)
        {
            textMessage.text = msg;
        }

        protected void ReshapeItems()
        {
            if (currentRound < countRounds)
            {
                var mainId = Random.Range(0, itemLevel.Length);
                SetItems(mainId);
                SetTextMessage(dataLevelManager.StartSentence + dataLevelManager.NameItemsPair[currentRound].Key);
                needWord = dataLevelManager.NameItemsPair[currentRound].Key;
                currentRound++;
            }
            else
            {
                CheckWinLevel();
            }
        }

        protected void SetItems(int mainId)
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
                var itemSprite = dataLevelManager.LevelSpriteDict[itemPair.Key][itemPair.Value];
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
