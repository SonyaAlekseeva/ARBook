using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Orchestra : MonoBehaviour
    {
        public float PlayTimerNormalized => _playTimer / PlayTimeSeconds;
        public float PlayTimeSeconds;
        
        private List<Instrument> _instruments = new List<Instrument>();

        private bool _isPlaying;
        private float _playTimer;

        private void Update()
        {
            if (!_isPlaying)
                return;
            
            _playTimer += Time.deltaTime;
            if (_playTimer >= PlayTimeSeconds)
            {
                Stop();
                _playTimer = 0;
            }
        }

        public void SetPlayTime(float t)
        {
            _playTimer = PlayTimeSeconds * t;
            
            foreach (Instrument instrument in _instruments)
            {
                if (!instrument.IsMuted)
                    instrument.PlayInOrchestra(_playTimer);
            }
        }

        public void Play()
        {
            foreach (Instrument instrument in _instruments)
            {
                instrument.PlayInOrchestra(_playTimer);
            }

            _isPlaying = true;
        }

        public void Stop()
        {
            foreach (Instrument instrument in _instruments)
            {
                instrument.Stop();
            }

            _isPlaying = false;
        }

        public void AddInstrument(Instrument instrument)
        {
            _instruments.Add(instrument);
        }

        public void EnableInstrument(Instrument instrument)
        {
            if (!_isPlaying)
                return;

            instrument.IsMuted = false;
            instrument.PlayInOrchestra(_playTimer);
        }

        public void DisableInstrument(Instrument instrument)
        {
            instrument.IsMuted = true;
            instrument.Stop();
        }
    }
}