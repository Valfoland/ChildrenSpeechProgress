using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Sounds;

namespace Levels
{
    [Serializable]
    public class UiObjectsData
    {
        public RectTransform Speeker;
        public RectTransform AnswerButton;
    }
    
    public abstract class LevelProduct : MonoBehaviour
    {
        [SerializeField] protected GameObject backgroundObject;
        public static Action<string> onVoice;
        public static Action<bool> onEndLevel;
        
        protected int currentIdSentences;
        protected Button voiceButton;
        
        protected const string WORD_SOUND = "звук";

        protected virtual void Start()
        {
            var voiceObject = GameObject.FindWithTag("PlayMessage");
            var backgroundParentObject = GameObject.FindWithTag("Background");

            Instantiate(backgroundObject, backgroundParentObject.transform);
            
            if(voiceObject != null)
                voiceButton = voiceObject.GetComponent<Button>();
        }

        protected virtual void CheckWinLevel()
        {
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
        }

        protected virtual void Voice(string word)
        {
            SoundSource.VoiceSound(word);
        }

        protected void VoiceCallBack(string word, Action onEndSound)
        {
            SoundSource.VoiceSoundCallBack(word, onEndSound);
        }
        
        protected virtual void StartDialogue() { }

        protected abstract void StartLevel();
    }
}