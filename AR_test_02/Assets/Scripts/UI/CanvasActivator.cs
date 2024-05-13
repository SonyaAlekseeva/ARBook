using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CanvasActivator : MonoBehaviour
    {
        [SerializeField] private GameObject[] ObjectsToEnable;
        [SerializeField] private GameObject[] ObjectsToDisable;

        private void Start()
        {
            foreach (var target in ObjectsToEnable)
            {
                target.SetActive(true);
            }

            foreach (var target in ObjectsToDisable)
            {
                target.SetActive(false);
            }
        }
    }
}