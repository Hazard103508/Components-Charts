using Rosso.Charts.Pie;
using UnityEngine;
using UnityEngine.UI;

namespace Rosso.Demo.PieChart
{
    public class LegendDemo : MonoBehaviour
    {
        [SerializeField] private Image colorPanel;
        [SerializeField] private Text label;
        [SerializeField] private Text value;

        public void Show_Info(Item item)
        {
            colorPanel.color = item.Color;
            label.text = item.Label;
            value.text = $"{item.Value} ({ (item.PercentageVaue * 100).ToString("f0") }%)";
        }
    }
}