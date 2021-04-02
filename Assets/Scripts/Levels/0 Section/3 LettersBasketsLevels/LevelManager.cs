using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Levels;
using UnityEditor;

namespace Section0.LettersBasketsLevels
{
    /// <summary>
    /// Инициализирует данные для каждого объекта т.е. читает данные и записвыает в падающий плод дерева
    /// в виде текста или картинки.
    /// Проверяет при возникновении события соответствует ли плод заданному критерию
    /// </summary>

    public class LevelManager : LevelProduct
    {
        [SerializeField] private ItemsGenerator itemsGenerator;
        [SerializeField] private LevelDialogueData levelDialogueData;
        [SerializeField] private int countRounds;
        private LevelDialogue levelDialogue;
        private DataLevelManager dataLevelManager;
        private string currentTypeWords;
        private int currentRound;

        protected override void Start()
        {
            base.Start();
            InitData();
            StartIntroDialogue();
        }


        private void InitData()
        {
            dataLevelManager = new DataLevelManager();
            levelDialogue = new LevelDialogue(levelDialogueData, dataLevelManager.DialogueDict);
            SetCurrentTypeWord();
            itemsGenerator.InitData();

            levelDialogue.onEndDialogue += StartIntroDialogue;
            itemsGenerator.onItemTake += CheckTakenItem;
            itemsGenerator.onItemFall += CheckFallenItem;
        }

        private void OnDestroy()
        {
            levelDialogue.onEndDialogue -= StartIntroDialogue;
            itemsGenerator.onItemTake -= CheckTakenItem;
            itemsGenerator.onItemFall -= CheckFallenItem;
        }

        private void SetCurrentTypeWord()
        {
            var random = Random.Range(0, dataLevelManager.ItemsDataDict.Count);
            currentTypeWords = dataLevelManager.ItemsDataDict.Keys.ToList()[random];
        }

        protected override void StartIntroDialogue()
        {
            if (currentIdDialogue >= dataLevelManager.DialogueDict.Count)
            {
                StartLevel();
                currentIdDialogue = 0;
                return;
            }

            levelDialogue.VoiceSentenceDialogue(currentIdDialogue, currentTypeWords.ToLower());
            currentIdDialogue++;
        }

        protected override void StartLevel()
        {
            itemsGenerator.EnableGenerateItems(currentTypeWords, dataLevelManager.ItemsDataDict[currentTypeWords]);
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
            CheckItem(currentTypeWords == this.currentTypeWords);
        }

        private void CheckItem(bool isTrueItem)
        {
            AttemptCounter.SetAttempt(isTrueItem);
            
            if (currentRound < countRounds)
            {
                currentRound++;
            }
            else
            {
                itemsGenerator.DisableGenerateItems();
                CheckWinLevel();
            }
        }
    }
}
