using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Sounds
{
    public class DataSounds
    {
        public List<string> SoundNameList = new List<string>();
    }

    /// <summary>
    /// Класс-дата аудиофайлов для приложения
    /// </summary>
    public class SoundSource : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private static Action<string> onPlayAudio;
        private static Action<string, Action> onPlayAudioCallBack;
        private DataSounds dataSounds;
        private bool canPlayAudio;

        private void Start()
        {
            onPlayAudio += StartAudio;
            onPlayAudioCallBack += StartAudioCallBack;
            LoadDataSounds();
        }

        private void OnDestroy()
        {
            onPlayAudio -= StartAudio;
            onPlayAudioCallBack -= StartAudioCallBack;
        }

        private void LoadDataSounds()
        {
            dataSounds = new DataSounds();
            JsonParserGame<Dictionary<string, List<string>>> jsonData =
                new JsonParserGame<Dictionary<string, List<string>>>();

            try
            {
                dataSounds.SoundNameList = jsonData.GetData("JsonDataSounds", "JsonDataSounds")["Sounds"];
            }
            catch (KeyNotFoundException e)
            {
            }
        }

        public static void VoiceSound(string sentence)
        {
            onPlayAudio?.Invoke(sentence);
        }

        public static void VoiceSoundCallBack(string sentence, Action endSound)
        {
            onPlayAudioCallBack?.Invoke(sentence, endSound);
        }

        private void StartAudio(string soundName)
        {
            StartCoroutine(WaitAudio(soundName));
        }

        private void StartAudioCallBack(string soundName, Action endSound)
        {
            StartCoroutine(WaitAudioCallBack(soundName, endSound));
        }

        private IEnumerator WaitAudio(string soundName)
        {
            while (audioSource != null && audioSource.isPlaying)
            {
                yield return null;
            }

            PlayAudio(soundName);
        }

        private IEnumerator WaitAudioCallBack(string soundName, Action endSound)
        {
            while (audioSource != null && audioSource.isPlaying)
            {
                yield return null;
            }

            PlayAudio(soundName);

            while (audioSource != null && audioSource.isPlaying)
            {
                yield return null;
            }

            endSound?.Invoke();
        }

        private string NormalizeString(string soundName)
        {
            return soundName.ToLower()
                .Replace(" ", "")
                .Replace("ё", "е");
        }
        
        private void PlayAudio(string soundName)
        {
            if(soundName == null) return;
            
            int percentMax = 0;
            string actualSound = "";
            var soundIn = NormalizeString(soundName);

            foreach (var sound in dataSounds.SoundNameList)
            {
                var soundOut = NormalizeString(sound);

                if (soundIn.StartsWith(soundOut))
                {
                    GetMostSuitableSound(ref actualSound, ref percentMax, soundOut, soundIn, sound);
                }
            }

            ResourceRequest resourceRequest = Resources.LoadAsync<AudioClip>($"Sounds/{actualSound}");
            audioSource.clip = resourceRequest.asset as AudioClip;
            audioSource.Play();
        }

        private void GetMostSuitableSound(ref string actualSound, ref int percentMax, string soundOut, string soundIn,
            string sound)
        {
            string soundTempBigger = soundOut.Length > soundIn.Length ? soundOut : soundIn;
            string soundTempSmaller = soundOut.Length <= soundIn.Length ? soundOut : soundIn;
            int counTrueSound = 0;

            for (int i = 0; i < soundTempBigger.Length; i++)
            {
                if (i < soundTempSmaller.Length &&
                    soundTempBigger[i] == soundTempSmaller[i])
                {
                    counTrueSound++;
                }
                else
                {
                    break;
                }
            }

            if (percentMax <= MathExtensions.CalculatePercent(counTrueSound, soundTempBigger.Length))
            {
                percentMax = MathExtensions.CalculatePercent(counTrueSound, soundTempBigger.Length);
                actualSound = sound;
            }
        }
    }
}
