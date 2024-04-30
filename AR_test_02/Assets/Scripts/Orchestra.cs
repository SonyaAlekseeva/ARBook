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
                    instrument.PlayInOrchestra(normalizedTime);
            }
        }

        public void Play()
        {
            foreach (Instrument instrument in Instruments)
            {
                instrument.PlayInOrchestra(0f);
            }

            _isPlaying = true;
        }

        public void Stop()
        {
            Instrument first = Instruments.First();
            _playTimeNormalized = first.MusicSource.time / first.MusicSource.clip.length;
            
            foreach (Instrument instrument in Instruments)
            {
                instrument.Stop();
            }

            _isPlaying = false;
        }

        public void AddInstrument(Instrument instrument)
        {
            Instruments.Add(instrument);
            EnableInstrument(instrument);
            MainPanel.CreateInfoButtonForInstrument(instrument);
        }

        public void EnableInstrument(Instrument instrument)
        {
            instrument.IsMuted = false;
            
            if (_isPlaying)
                instrument.PlayInOrchestra(_playTimeNormalized);
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
        }
    }
}