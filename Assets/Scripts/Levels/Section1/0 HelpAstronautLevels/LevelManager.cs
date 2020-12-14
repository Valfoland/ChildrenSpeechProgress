using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section1.HelpAstronautLevels.Level0
{
    public class LevelManager : LevelProduct
    {
        [SerializeField] private int countRounds;
        [SerializeField] private ItemLevel[] itemLevel;
        [SerializeField] private Text textMessage;

        private DataLevelManager dataLevelManager;
        private string needWord;
        private int currentRound;

        private void Start()
        {
            InitData();
            ReshapeItems();
        }

        private void InitData()
        {
            ItemLevel.onClickBox += CheckBox;
            dataLevelManager = new DataLevelManager(countRounds);
        }

        private void OnDestroy()
        {
            ItemLevel.onClickBox -= CheckBox;
        }

        private void SetTextMessage(string msg)
        {
            textMessage.text = msg;
        }

        private void ReshapeItems()
        {
            if (currentRound < countRounds)
            {
                var mainId = Random.Range(0, itemLevel.Length);
                SetItems(mainId);
                SetTextMessage("Предмет, который нужно найти это - " + dataLevelManager.NameItemsPair[currentRound].Key);
                needWord = dataLevelManager.NameItemsPair[currentRound].Key;
                currentRound++;
            }
            else
            {
                CheckWinLevel();
            }
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
                var itemSprite = dataLevelManager.LevelSpriteDict[itemPair.Key][itemPair.Value];
                itemLevel[i].SetDataBox(new KeyValuePair<string, Sprite>(itemPair.Key, itemSprite));
            }
        }

        private void CheckBox(string wordBox, ItemLevel itemLevel)
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
