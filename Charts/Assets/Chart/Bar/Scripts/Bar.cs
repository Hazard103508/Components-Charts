using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rosso.Charts.Bar
{
    public class Bar : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Image image;
        [SerializeField] private Text value;
        [SerializeField] private Text label;
        private Item item;
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