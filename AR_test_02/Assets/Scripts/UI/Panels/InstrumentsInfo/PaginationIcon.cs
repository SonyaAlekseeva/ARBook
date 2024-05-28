using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels.InstrumentsInfo
{
    public class PaginationIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        public void SetState(bool active)
        {
            _image.sprite = active ? _activeSprite : _inactiveSprite;
        }
    }
}