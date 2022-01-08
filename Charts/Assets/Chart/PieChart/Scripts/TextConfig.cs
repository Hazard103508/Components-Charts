using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosso.Charts.Pie
{
    [Serializable]
    public class TextConfig
    {
        /// <summary>
        /// Tipo de valor a mostrar en el grafico
        /// </summary>
        public ValueType valueType;
        /// <summary>
        /// Cantidad de decimales a mostrar en el valor
        /// </summary>
        public int decimalCount;

        public enum ValueType
        {
            Percentage,
            Original
        }
    }
}