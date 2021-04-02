using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Sounds;

namespace Levels
{
    public abstract class LevelProduct : MonoBehaviour
    {
        public static Action<string> onVoice;
        public static Action<bool> onEndLevel;
        
        protected int currentIdDialogue;
        protected Button voiceButton;
        
        protected const string WORD_SOUND = "звук";

        protected virtual void Start()
        {
            voiceButton = GameObject.FindWithTag("PlayMessage").GetComponent<Button>();
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
        
        protected virtual void StartIntroDialogue() { }

        protected abstract void StartLevel();
    }
}