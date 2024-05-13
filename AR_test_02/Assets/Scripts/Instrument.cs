using UnityEngine;

namespace DefaultNamespace
{
    public class Instrument : MonoBehaviour
    {
        public string Name;
        
        public bool IsMuted;
        public int Page;

        public Animation Animation;
        public InstrumentData Data;
        public AudioSource MusicSource;

        private InstrumentOnImageInfo _info;
        
        public void Initialize(InstrumentOnImageInfo info)
        {
            _info = info;
            Page = info.FactsPage;
            MusicSource.clip = info.MusicClip != null ? info.MusicClip : Data.Music;
        }

        public void Play(float normalizedTime)
        {
            if (MusicSource.clip == null)
                return;
            
            MusicSource.time = MusicSource.clip.length * normalizedTime;
            MusicSource.Play();
        }

        public void Stop()
        {
            MusicSource.Stop();
        }
    }
}