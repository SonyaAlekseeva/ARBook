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

        public void Initialize(InstrumentData data, int page, Vector3 position)
        {
            Data = data;
            _page = page;
            transform.position = position;
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