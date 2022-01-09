using UnityEngine;

namespace Rosso.Charts.Pie
{
    public class Item
    {
        #region Constructor
        public Item(string label, float value, Color color, object data = null) : this(label, value, color, Color.black, data)
        {
        }
        public Item(string label, float value, Color color, Color colorText, object data = null)
        {
            this.Label = label;
            this.Value = value;
            this.Color = color;
            this.ColorText = colorText;
            this.Data = data;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Etiqueta asociada al valor del item
        /// </summary>
        public string Label { get; private set; }
        /// <summary>
        /// Valor numerico asociado al item
        /// </summary>
        public float Value { get; private set; }
        /// <summary>
        /// Valor porcentual
        /// </summary>
        public float PercentageVaue { get; set; }
        /// <summary>
        /// Color a mostrar en el grafico
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Color del texto a mostrar sobre el grafico
        /// </summary>
        public Color ColorText { get; private set; }
        /// <summary>
        /// Objeto asociado al item
        /// </summary>
        public object Data { get; set; }
        #endregion
    }
}