using UnityEngine;
using UnityEngine.UI;

namespace Rosso.Charts.Bar
{
    public class HorizontalLine : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Image line;
        [SerializeField] private Text label;
        #endregion

        #region Public Methods
        public void Initialize(float value, float paddingLeft, Rosso.Charts.Bar.BarChart.Lines lines, float? height = null)
        {
            label.text = string.Format(lines.valueFormat, value);

            var rec = (RectTransform)line.transform;
            rec.SetLeft(paddingLeft);

            if (height.HasValue)
                rec.sizeDelta = new Vector2(rec.sizeDelta.x, height.Value);
        }
        #endregion
    }
}