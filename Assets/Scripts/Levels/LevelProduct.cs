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

        protected virtual void CheckWinLevel()
        {
            onEndLevel?.Invoke(AttemptCounter.IsLevelPass());
        }

        protected virtual void Voice(string word)
        {
            SoundSource.VoiceSound(word);
        }

    }
}