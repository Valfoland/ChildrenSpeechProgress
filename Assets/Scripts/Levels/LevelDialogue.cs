using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Levels;
using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[System.Serializable]
public class LevelDialogueData
{
    public Button ButtonAnswer;
    public Text TextInButtonAnswer;
    public Text TextDialog;
}

public class LevelDialogue
{
    public Action onEndDialogue;
    private LevelDialogueData levelDialogueData;
    private Dictionary<int, List<DialogueData>> dialogueDict;

    public LevelDialogue(LevelDialogueData levelDialogueData, Dictionary<int, List<DialogueData>> dialogueDict)
    {
        this.levelDialogueData = levelDialogueData;
        this.dialogueDict = dialogueDict;
        if(levelDialogueData.ButtonAnswer != null)
            this.levelDialogueData.ButtonAnswer.onClick.AddListener(() =>
            {
                onEndDialogue?.Invoke();
                levelDialogueData.ButtonAnswer.gameObject.SetActive(false);
            });
    }
    
    public void VoiceSentenceDialogue(int id, string addictionSentence = "")
    {
        if (!dialogueDict.ContainsKey(id))
        {
            onEndDialogue?.Invoke();
            return;
        }
        
        var idStr = dialogueDict[id].Count > 1 ? Random.Range(0, dialogueDict[id].Count) : 0;
        var dialogueText = dialogueDict[id][idStr].DialogueText;
        var dialogueList = new List<string>
        {
            dialogueText
        };

        if (dialogueText.IndexOf('+') != -1)
        {
            dialogueText = dialogueText.Replace("+", addictionSentence);
            dialogueList.Add(addictionSentence);
        }

        for(int i = 0; i < dialogueList.Count; i++)
        {
            if (i == dialogueList.Count - 1)
            {
                SoundSource.VoiceSoundCallBack(dialogueList[i], () => ShowButtonDialogue(id, idStr));
            }
            else
            {
                SoundSource.VoiceSound(dialogueList[i]);
            }
        }
        
        levelDialogueData.TextDialog.text = dialogueText;
    }

    private void ShowButtonDialogue(int id, int idStr)
    {
        if (dialogueDict[id][idStr].AnswerText != "")
        {
            if(levelDialogueData.TextInButtonAnswer != null)
                levelDialogueData.TextInButtonAnswer.text = dialogueDict[id][idStr].AnswerText;
            if(levelDialogueData.ButtonAnswer != null)
                levelDialogueData.ButtonAnswer.gameObject.SetActive(true);
        }
        else
        {
            if(levelDialogueData.ButtonAnswer != null)
                levelDialogueData.ButtonAnswer.gameObject.SetActive(false);
            
            onEndDialogue?.Invoke();
        }
    }
}
