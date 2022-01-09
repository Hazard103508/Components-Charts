using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rosso.Charts.Bar
{
    public class Bar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region Objects
        [SerializeField] private Image image;
        [SerializeField] private Text value;
        [SerializeField] private Text label;
        private Item item;
        #endregion

        #region Events
        public ItemEvent onPointerEnter = new ItemEvent();
        public ItemEvent onPointerExit = new ItemEvent();
        public ItemEvent onPointerDown = new ItemEvent();
        public ItemEvent onPointerUp = new ItemEvent();
        public ItemEvent onPointerClick = new ItemEvent();
        #endregion

        #region Unity Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke(this.item);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke(this.item);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown.Invoke(this.item);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp.Invoke(this.item);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick.Invoke(this.item);
        }
        #endregion

        #region Public Methods
        public void Initialize(Item item, Rosso.Charts.Bar.BarChart.Bars bars)
        {
            this.item = item;
            this.image.color = item.color.HasValue ? item.color.Value : bars.defaultColor;
            this.value.text = item.Value.ToString($"f{bars.decimalCount}");
            this.label.text = item.Label;

            if (!bars.showValue)
                this.value.gameObject.SetActive(false);

            if (!bars.showLabel)
                this.label.gameObject.SetActive(false);
        }
        #endregion
    }
}