using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Levels;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Section0.LettersBasketsLevels
{
    /// <summary>
    /// Инициализирует данные для каждого объекта т.е. читает данные и записвыает в падающий плод дерева
    /// в виде текста или картинки.
    /// Проверяет при возникновении события соответствует ли плод заданному критерию
    /// </summary>

    [Serializable]
    public class UiObjectsData
    {
        public RectTransform Speeker;
        public RectTransform AnswerButton;
        public RectTransform Basket;
    }
    
    public class LevelManager : LevelProduct
    {
        [SerializeField] private ItemsGenerator itemsGenerator;
        [SerializeField] private Basket basket;
        [SerializeField] private LevelDialogueData levelDialogueData;
        [SerializeField] private UiShower uiShower;
        [SerializeField] private UiObjectsData uiObjectsData;
        [SerializeField] private int countRounds;
        
        private LevelDialogue levelDialogue;
        private LevelDialogue levelGameSentences;
        private DataLevelManager dataLevelManager;
        
        private string currentTypeWords;
        private int currentRound;

        private Dictionary<int, List<DialogueData>> sentencesDict;

        protected override void Start()
        {
            
            base.Start();
            InitData();
            StartDialogue();
        }
        
        private void InitData()
        {
            dataLevelManager = new DataLevelManager();
            levelDialogue = new LevelDialogue(levelDialogueData, dataLevelManager.DialogueDict);
            sentencesDict = dataLevelManager.DialogueDict;
            voiceButton.onClick.AddListener(() => Voice(currentTypeWords));
            itemsGenerator.InitData();
            SetCurrentTypeWord();

            levelDialogue.onEndDialogue += DelayInvokeDialogue;
            itemsGenerator.onItemTake += CheckTakenItem;
            itemsGenerator.onItemFall += CheckFallenItem;
        }

        private void OnDestroy()
        {
            levelDialogue.onEndDialogue -= DelayInvokeDialogue;
            itemsGenerator.onItemTake -= CheckTakenItem;
            itemsGenerator.onItemFall -= CheckFallenItem;
        }

        private void SetCurrentTypeWord()
        {
            var random = Random.Range(0, dataLevelManager.ItemsDataDict.Count);
            
            while (currentTypeWords == dataLevelManager.ItemsDataDict.Keys.ToList()[random])
            {
                random = Random.Range(0, dataLevelManager.ItemsDataDict.Count);
            }
            
            currentTypeWords = dataLevelManager.ItemsDataDict.Keys.ToList()[random];
        }

        private void DelayInvokeDialogue()
        {
            Invoke("StartDialogue", 1f);
        }

        protected override void StartDialogue()
        {
            if (currentIdSentences >= sentencesDict.Count)
            {
                StartLevel();
                
                currentIdSentences = 0;
                return;
            }

            levelDialogue.VoiceSentenceDialogue(currentIdSentences, currentTypeWords.ToLower());
            currentIdSentences++;
        }
        
        protected override void StartLevel()
        {
            itemsGenerator.EnableGenerateItems(currentTypeWords, dataLevelManager.ItemsDataDict);

            uiShower.HideObjects(new List<RectTransform>
            {
                uiObjectsData.Speeker,
                uiObjectsData.AnswerButton
            });

            uiShower.ShowObjects(new List<RectTransform> {uiObjectsData.Basket});
        }

        private void CheckFallenItem(string currentTypeWords)
        {
            if (currentTypeWords == this.currentTypeWords)
            {
                CheckItem(false);
            }
        }

        private void CheckTakenItem(string currentTypeWords)
        {
            basket.AnimBasket(currentTypeWords == this.currentTypeWords);
            CheckItem(currentTypeWords == this.currentTypeWords);
        }

        private void CheckItem(bool isTrueItem)
        {
            AttemptCounter.SetAttempt(isTrueItem);

            if (currentRound < countRounds)
            {
                /*if (currentRound > countRounds / 2)
                {
                    SetCurrentTypeWord();
                    levelDialogue = new LevelDialogue(levelDialogueData, dataLevelManager.GameSentencesDict);
                    sentencesDict = dataLevelManager.GameSentencesDict;
                    StartDialogue();
                    itemsGenerator.DisableGenerateItems();
                }*/
                
                currentRound++;
            }
            else
            {
                Debug.Log("sadsf ");
                itemsGenerator.DisableGenerateItems();
                CheckWinLevel();
            }
        }
    }
}
