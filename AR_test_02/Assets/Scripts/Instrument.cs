using UnityEngine;

namespace DefaultNamespace
{
    public class Instrument : MonoBehaviour
    {
        public bool IsMuted { get; set; }
        
        public Animation Animation;
        public InstrumentData Data;
        public AudioSource MusicSource;

        private int _page;

        public void Initialize(int page)
        {
            _page = page;
        }

        public void PlayInOrchestra(float time)
        {
            MusicSource.clip = Data.MusicInOrchestra;
            MusicSource.time = time;
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