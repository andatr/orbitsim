using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Osim.UI
{
    public class TimeWarpPanelItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private int _index;
        private System.Action<int> _clickCallback;
        private System.Action<int> _hoverEnterCallback;
        private System.Action<int> _hoverExitCallback;
        private Image _image;
        public Color _activeColor;
        public Color _inactiveColor;
        private bool _active;

        // -------------------------------------------------------------------------------------------------------------------
        public bool active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (_image != null)
                    _image.color = _active ? _activeColor : _inactiveColor;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Start()
        {
            _image = GetComponent<Image>();
            _image.color = _active ? _activeColor : _inactiveColor;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void OnDestroy()
        {
            _clickCallback = null;
            _hoverEnterCallback = null;
            _hoverExitCallback = null;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Init(Color activeColor, Color inactiveColor)
        {
            _active = false;
            _activeColor = activeColor;
            _inactiveColor = inactiveColor;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void RegisterCallback(int index, System.Action<int> clickCallback,
            System.Action<int> hoverEnterCallback, System.Action<int> hoverExitCallback)
        {
            _index = index;
            _clickCallback = clickCallback;
            _hoverEnterCallback = hoverEnterCallback;
            _hoverExitCallback = hoverExitCallback;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (_clickCallback != null)
                _clickCallback(_index);
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (_hoverEnterCallback != null)
                _hoverEnterCallback(_index);
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if (_hoverExitCallback != null)
                _hoverExitCallback(_index);
        }
    }
}
