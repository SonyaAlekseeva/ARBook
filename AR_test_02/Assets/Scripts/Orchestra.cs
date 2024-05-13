using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.UI.Panels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Orchestra : MonoBehaviour
    {
        public float PlayTimer => _playTimeNormalized;
        public float PlayTimeSeconds;
        public MainPanel MainPanel;
        public List<Instrument> Instruments { get; } = new List<Instrument>();

        private bool _isPlaying;
        private float _playTimeNormalized;

        public void SetPlayTime(float normalizedTime)
        {
            _playTimeNormalized = normalizedTime;
            
            foreach (Instrument instrument in Instruments)
            {
                if (!instrument.IsMuted)
                    instrument.Play(normalizedTime);
            }
        }

        public void Play()
        {
            foreach (Instrument instrument in Instruments)
            {
                instrument.Play(0f);
            }

            _isPlaying = true;
        }

        public void Stop()
        {
            if (Instruments == null) return;
            if (Instruments.Count == 0) return;
            
            Instrument first = Instruments.First();
            
            if (first.MusicSource != null && first.MusicSource.clip != null)
                _playTimeNormalized = first.MusicSource.time / first.MusicSource.clip.length;
            else
                _playTimeNormalized = 0f;

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

            _isPlaying = false;
        }

        public void AddInstrument(Instrument instrument)
        {
            Instruments.Add(instrument);
            EnableInstrument(instrument);
            MainPanel.CreateInfoButtonForInstrument(instrument);
            
            if (instrument.MusicSource == null || instrument.MusicSource.clip == null)
                return;

            float playTime = instrument.MusicSource.clip.length;
            PlayTimeSeconds = Mathf.Max(PlayTimeSeconds, playTime);
        }

        public void EnableInstrument(Instrument instrument)
        {
            instrument.IsMuted = false;
            
            if (_isPlaying)
                instrument.Play(_playTimeNormalized);
        }

        public void DisableInstrument(Instrument instrument)
        {
            instrument.IsMuted = true;
            instrument.Stop();
        }

        public void ClearInstruments()
        {
            Stop();
            
            MainPanel.ClearInfoButtons();
            
            foreach (Instrument instrument in Instruments)
            {
                Destroy(instrument.gameObject);
            }
            
            Instruments.Clear();
            PlayTimeSeconds = 0f;
        }
    }
}