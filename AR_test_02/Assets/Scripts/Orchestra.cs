using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.UI.Panels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Orchestra : MonoBehaviour
    {
        public bool IsPlaying;
        public float PlayTimeNormalized;
        public float PlayTimeSeconds;
        public MainPanel MainPanel;
        public List<Instrument> Instruments { get; } = new();

        private void Update()
        {
            if (!IsPlaying) return;
            
            Instrument first = Instruments.FirstOrDefault(x => x.MusicSource != null && x.MusicSource.clip != null && x.MusicSource.isPlaying);

            if (first == null)
                Stop();
            else
                PlayTimeNormalized = first.MusicSource.time / first.MusicSource.clip.length;
        }

        public void SetPlayTime(float normalizedTime)
        {
            Debug.Log($"Playing orchestra with normalized time {normalizedTime}");
            PlayTimeNormalized = normalizedTime;
            if (!IsPlaying) return;
            
            float elapsedTime = PlayTimeNormalized * PlayTimeSeconds;
            foreach (Instrument instrument in Instruments)
            {
                if (!instrument.IsMuted)
                    instrument.Play(elapsedTime);
            }
        }

        public void Play()
        {
            if (PlayTimeNormalized >= 1f)
                PlayTimeNormalized = 0f;
            
            float elapsedTime = PlayTimeNormalized * PlayTimeSeconds;
            Debug.Log($"Playing orchestra from {elapsedTime}");
            foreach (Instrument instrument in Instruments)
            {
                instrument.Play(elapsedTime);
            }

            IsPlaying = true;
        }

        public void Stop()
        {
            Debug.Log("Stop all instruments in orchestra");
            if (Instruments == null) return;
            if (Instruments.Count == 0) return;
            
            Instrument first = Instruments.FirstOrDefault(x => x.MusicSource != null && x.MusicSource.clip != null);
            
            if (first != null)
            {
                PlayTimeNormalized = first.MusicSource.time / first.MusicSource.clip.length;
                if (PlayTimeNormalized >= 1f)
                    PlayTimeNormalized = 0f;
                Debug.Log($"Got play time normalized from first instrument: {PlayTimeNormalized}");
            }
            else
            {
                Debug.Log("Cannot find instrument with clip!");
                PlayTimeNormalized = 0f;
            }

            for (int i = Instruments.Count - 1; i >= 0; i--)
            {
                var instrument = Instruments[i];
                if (instrument == null)
                {
                    Instruments.RemoveAt(i);
                    continue;
                }

                instrument.Stop();
            }

            IsPlaying = false;
        }

        public void AddInstrument(Instrument instrument)
        {
            Debug.Log($"Adding instrument {instrument.Name} to orchestra");
            Instruments.Add(instrument);
            EnableInstrument(instrument);
            MainPanel.CreateInfoButtonForInstrument(instrument);
            
            if (instrument.MusicSource == null || instrument.MusicSource.clip == null)
            {
                Debug.Log("Instrument music source or clip is null!");
                return;
            }

            float playTime = instrument.MusicSource.clip.length;
            Debug.Log($"Got play time from instrument: {playTime}, current: {PlayTimeSeconds}");
            PlayTimeSeconds = Mathf.Max(PlayTimeSeconds, playTime);
        }

        public void EnableInstrument(Instrument instrument)
        {
            Debug.Log($"Enabling instrument {instrument.Name} in orchestra, is playing: {IsPlaying}");
            instrument.IsMuted = false;

            if (!IsPlaying) return;
            
            float elapsedTime = PlayTimeNormalized * PlayTimeSeconds;
            instrument.Play(elapsedTime);
        }

        public void DisableInstrument(Instrument instrument)
        {
            Debug.Log($"Disabling instrument {instrument.Name} in orchestra");
            instrument.IsMuted = true;
            instrument.Stop();
        }

        public void ClearInstruments()
        {
            Debug.Log("Clearing instruments in orchestra");
            Stop();
            
            MainPanel.ClearInfoButtons();
            
            foreach (Instrument instrument in Instruments)
            {
                Destroy(instrument.gameObject);
            }
            
            Instruments.Clear();
            PlayTimeSeconds = 0f;
            PlayTimeNormalized = 0f;
        }
    }
}