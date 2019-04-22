using UnityEngine;
using UnityEngine.EventSystems;

namespace Osim.Views
{
    public class SpaceBodyIcon : MonoBehaviour, IPointerClickHandler
    {
        private Views.SpaceBody _spaceBodyView;
        private RectTransform _canvasRect;
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private float _deltaX, _deltaY;

        public new Camera camera { get; set; }
        public ICameraController cameraController { get; set; }

        // -------------------------------------------------------------------------------------------------------------------
        public Views.SpaceBody spaceBody
        {
            get { return _spaceBodyView; }
            set
            {
                _spaceBodyView = value;
                var image = GetComponent<UnityEngine.UI.Image>();
                image.color = _spaceBodyView.spaceBody.asset.icon.color;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------
        public Canvas canvas
        {
            get { return _canvas; }
            set
            {
                _canvas = value;
                _canvasRect = _canvas.transform as RectTransform;
                if (_canvasRect == null) return;
                _deltaX = _canvasRect.sizeDelta.x * 0.5f;
                _deltaY = _canvasRect.sizeDelta.y * 0.5f;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Start()
        {
            _rectTransform = transform as RectTransform;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Update()
        {
            if (spaceBody == null || camera == null || canvas == null || _rectTransform == null) return;
            Vector2 viewportPos = camera.WorldToViewportPoint(spaceBody.transform.position);
            _rectTransform.anchoredPosition = new Vector2(
                viewportPos.x * _canvasRect.sizeDelta.x - _deltaX,
                viewportPos.y * _canvasRect.sizeDelta.y - _deltaY);
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (cameraController != null && _spaceBodyView != null)
            {
                cameraController.Target = _spaceBodyView.gameObject;
            }
        }
    }
}
