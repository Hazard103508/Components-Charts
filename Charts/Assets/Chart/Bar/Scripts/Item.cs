using UnityEngine;

namespace Rosso.Charts.Bar
{
    public class Item
    {
        #region Constructor
        public Item(string label, float value, Color? color = null, object data = null)
        {
            this.Label = label;
            this.Value = value;
            this.color = color;
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
        /// Color
        /// </summary>
        public Color? color { get; set; }
        /// <summary>
        /// Objeto asociado al item
        /// </summary>
        public object Data { get; set; }
        #endregion
    }
}