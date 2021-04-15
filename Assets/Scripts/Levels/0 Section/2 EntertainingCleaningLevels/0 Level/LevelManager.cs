using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section0.EntertainingCleaningLevels.Level0
{
    /// <summary>
    /// Порядок уровня
    /// Озвучивается вступление
    /// При этом на поле появляются 5 блоков со словами, на них можно нажимать
    /// Нажимается кнопка начать
    /// Озвучивается звук
    /// Нужно нажать на блок, соответствующий звуку
    /// Если верно то приложение анимирует что это правильно, и также если наоборот
    /// При верном выборе на месте старого блока генерируется новый
    /// Игра длится несколько раундов, затем подсчитываются очки
    /// </summary>
    public class LevelManager : LevelProduct
    {       
        [SerializeField] private int countRounds;
        [SerializeField] private LevelDialogueData levelDialogueData;
        [SerializeField] private MissionsDecorator.ItemLevel[] itemLevels;
        private Dictionary<MissionsDecorator.ItemLevel, int> idItemsDict = new Dictionary<MissionsDecorator.ItemLevel, int>();
        private DataLevelManager dataLevelManager;
        private LevelDialogue levelDialogue;
        
        private bool startLevel;
        private int currentRound;
        private string needWord;

        protected override void Start()
        {
            base.Start();
            InitData();
            InitItems();
            StartDialogue();
        }
        
        private void OnDestroy()
        {
            MissionsDecorator.ItemLevel.onClickBox -= CheckBox;
            levelDialogue.onEndDialogue -= StartDialogue;
        }

        private void InitData()
        {
            voiceButton.onClick.AddListener(OnClickVoiceButton);
            dataLevelManager = new DataLevelManager(countRounds);
            levelDialogue = new LevelDialogue(levelDialogueData, dataLevelManager.DialogueDict);
            
            if (countRounds > dataLevelManager.NameItemsPair.Count - 1)
                countRounds = dataLevelManager.NameItemsPair.Count - 1;
            
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
            
            currentIdSentences++;
            levelDialogue.VoiceSentenceDialogue(currentIdSentences - 1);
        }

        protected override void StartLevel()
        {
            startLevel = true;
            Voice(needWord);
        }

        private void OnClickVoiceButton()
        {
            Voice(needWord);
        }

        private void InitItems()
        {
            if (currentRound >= countRounds)
            {
                CheckWinLevel();
                return;
            }
            
            var mainIndex = Random.Range(0, itemLevels.Length);
            var mainId = Random.Range(0, dataLevelManager.NameItemsPair.Count);

            for (int i = 0; i < itemLevels.Length; i++)
            {
                var id = SearchUniqueId(i, mainIndex, mainId);
                idItemsDict.Add(itemLevels[i], id);
                var itemPair = dataLevelManager.NameItemsPair[id];
                var itemSprite = dataLevelManager.SpriteDict[itemPair.Key][itemPair.Value];
                itemLevels[i].SetDataBox(new KeyValuePair<string, Sprite>(itemPair.Key, itemSprite));
            }
            
            needWord = dataLevelManager.NameItemsPair[mainId].Key;
            currentRound++;
        }

        private void ResetItem(MissionsDecorator.ItemLevel itemLevel)
        {
            if (currentRound >= countRounds) CheckWinLevel();

            var id = Random.Range(0, dataLevelManager.NameItemsPair.Count);
            var itemPair = dataLevelManager.NameItemsPair[id];
            var itemSprite = dataLevelManager.SpriteDict[itemPair.Key][itemPair.Value];

            itemLevel.SetDataBox(new KeyValuePair<string, Sprite>(itemPair.Key, itemSprite));
            
            idItemsDict[itemLevel] = id;
            var idItem = idItemsDict.Values.ToList()[Random.Range(0, idItemsDict.Values.Count)];
            
            needWord = dataLevelManager.NameItemsPair[idItem].Key;
            currentRound++;
        }

        private int SearchUniqueId(int i, int mainIndex, int mainId)
        {
            int id;
            
            if (i != mainIndex)
            {
                id = Random.Range(0, dataLevelManager.NameItemsPair.Count);

                while (id == mainId)
                {
                    id = Random.Range(0, dataLevelManager.NameItemsPair.Count);
                }
            }
            else
            {
                id = mainId;
            }

            return id;
        }

        private void CheckBox(string wordBox, MissionsDecorator.ItemLevel itemLevel)
        {
            if (!startLevel)
            {
                Voice(wordBox);
                return;
            }

            if (wordBox == needWord)
            {
                AttemptCounter.SetAttempt(true);
                StartCoroutine(WaitReshape(2f, itemLevel));
                itemLevel.AnimBox(true);
            }
            else
            {
                AttemptCounter.SetAttempt(false);
                Voice(needWord);
                itemLevel.AnimBox(false);
            }
        }

        private IEnumerator WaitReshape(float time, MissionsDecorator.ItemLevel itemLevel)
        {
            yield return new WaitForSeconds(time);
            ResetItem(itemLevel);
        }
    }
}
