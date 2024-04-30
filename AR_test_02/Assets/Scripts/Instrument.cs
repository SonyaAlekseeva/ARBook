using UnityEngine;

namespace DefaultNamespace
{
    public class Instrument : MonoBehaviour
    {
        public bool IsMuted;
        
        public int Page;
        
        public Animation Animation;
        public InstrumentData Data;
        public AudioSource MusicSource;

        public void Initialize(int page)
        {
            Page = page;
        }

        public void PlayInOrchestra(float normalizedTime)
        {
            MusicSource.clip = Data.MusicInOrchestra;
            MusicSource.time = MusicSource.clip.length * normalizedTime;
            MusicSource.Play();
        }

        public void PlaySolo()
        {
            MusicSource.clip = Data.MusicSolo;
            MusicSource.Play();
        }

        public void Stop()
        {
            MusicSource.Stop();
        }
    }
}