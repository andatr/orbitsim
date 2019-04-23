using UnityEngine;
using UnityEngine.UI;

namespace Osim.UI
{
    public class TimeWarpPanel : MonoBehaviour
    {
        public int[] items;
        public GameObject itemPrefab;
        public GameObject hoverText;
        public Color activeColor;
        public Color inactiveColor;

        private TimeWarpPanelItem[] _panelItems;
        private Text _textObject;

        // -------------------------------------------------------------------------------------------------------------------
        public void Start()
        {
            if (itemPrefab == null || items == null || items.Length == 0)
            {
                gameObject.SetActive(false);
                return;
            }
            _panelItems = new TimeWarpPanelItem[items.Length];
            for (int i = 0; i < items.Length; ++i)
                _panelItems[i] = NewItem(i);
            SetWidth();
            OnHoverExit(0);
            OnItemClick(0);
        }

        // -------------------------------------------------------------------------------------------------------------------
        private TimeWarpPanelItem NewItem(int index)
        {
            var itemObject = Instantiate(itemPrefab);
            itemObject.name = itemPrefab.name + items[index];
            itemObject.SetParent(gameObject);

            var panelItem = itemObject.GetComponent<TimeWarpPanelItem>();
            panelItem.Init(activeColor, inactiveColor);
            panelItem.RegisterCallback(index, OnItemClick, OnHoverEnter, OnHoverExit);
            return panelItem;
        }

        // -------------------------------------------------------------------------------------------------------------------
        private void SetWidth()
        {
            var rect = GetComponent<RectTransform>();
            var childRect = itemPrefab.GetComponent<RectTransform>();
            var layout = GetComponent<HorizontalLayoutGroup>();
            var width = layout.padding.right + layout.padding.left +
                        layout.spacing * (items.Length - 1) + items.Length * childRect.sizeDelta.x;
            rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
            SetHoverTextPosition(width);
        }

        // -------------------------------------------------------------------------------------------------------------------
        private void SetHoverTextPosition(float left)
        {
            if (hoverText == null) return;
            _textObject = hoverText.GetComponent<Text>();
            var rect = hoverText.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(5.0f + left, 0.0f);
        }

        // -------------------------------------------------------------------------------------------------------------------
        private void OnItemClick(int index)
        {
            for (int i = 0; i <= index; ++i)
                _panelItems[i].active = true;
            for (int i = index + 1; i < _panelItems.Length; ++i)
                _panelItems[i].active = false;
        }

        // -------------------------------------------------------------------------------------------------------------------
        private void OnHoverEnter(int index)
        {
            if (hoverText == null || _textObject == null) return;
            hoverText.SetActive(true);
            _textObject.text = "x" + items[index];
        }

        // -------------------------------------------------------------------------------------------------------------------
        private void OnHoverExit(int index)
        {
            if (hoverText == null) return;
                hoverText.SetActive(false);
        }
    }
}
