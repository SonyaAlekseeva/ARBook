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
        
        public void Initialize(InstrumentOnImageInfo info, string pageName)
        {
            Debug.Log($"Initializing instrument {Name} with info, hasMusic: {info.MusicClip != null}");
            _info = info;
            Page = int.Parse(pageName);
            MusicSource.clip = info.MusicClip != null ? info.MusicClip : Data.Music;
        }

        public void Play(float elapsedTime)
        {
            Debug.Log($"Playing instrument {Name} with time {elapsedTime}, has clip: {MusicSource.clip != null}, is muted: {IsMuted}");
            if (MusicSource.clip == null)
                return;
            
            if (IsMuted)
                return;
            
            MusicSource.time = elapsedTime;
            MusicSource.Play();
        }

        public void Stop()
        {
            Debug.Log($"Stopping instrument {Name}");
            MusicSource.Stop();
        }
    }
}