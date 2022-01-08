using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rosso.Charts.Pie
{
    public class PieSection : MonoBehaviour, ICanvasRaycastFilter, IPointerEnterHandler
    {
        #region Objects
        [SerializeField] private Image image;
        [SerializeField] private Text label;
        private Item item;
        #endregion

        #region Events
        public ItemEvent onSelectItem;
        #endregion

        #region Properties
        /// <summary>
        /// Valor del radio de circulo del chart
        /// </summary>
        public float Ratio { get; set; }
        #endregion

        #region Unity Methods
        public void OnPointerEnter(PointerEventData eventData)
        {
            onSelectItem.Invoke(this.item);
        }
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            // Obtengo la posición relativa del mouse desde el centro del chart
            Vector2 mousePos = sp - (Vector2)this.transform.position;

            // Obtengo el ángulo en grados que forma la magnitud del vector
            float angle = -Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;

            // Rango de ángulos que utiliza esta porción del grafico
            float angleMin = this.image.transform.localRotation.eulerAngles.z;
            float angleMax = angleMin + (this.item.PercentageVaue * 360);

            // Si el ángulo obtenido de la posición del mouse en relación al eje del chart, se encuentra entre el rango de ángulos que utiliza esta porción del gráfico y la distancia al eje es menor al radio, se asume que el mouse esta sobre el grafico
            return angle >= angleMin && angle <= angleMax && mousePos.magnitude <= Ratio;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Seteo el item que tendra la porsion del grafico
        /// </summary>
        /// <param name="item">Item a agregar al grafico</param>
        /// <param name="rotation">Rotacion inicial que tendra la seecion del grafico</param>
        /// <param name="config">configuracion adicional</param>
        public void Set_Item(Item item, float rotation, TextConfig config)
        {
            this.item = item;
            image.color = item.Color;
            image.fillAmount = this.item.PercentageVaue;
            image.transform.Rotate(Vector3.forward * rotation);

            if (this.item.PercentageVaue == 1)
                label.transform.localPosition = Vector3.zero;
            else
            {
                float centerAngle = (rotation + ((this.item.PercentageVaue * 360) / 2)) * Mathf.Deg2Rad;
                var direction = new Vector3(-Mathf.Sin(centerAngle), Mathf.Cos(centerAngle), 0f).normalized; // direcion hacia fuera del circulo que posee esta seccion del chart
                label.transform.localPosition = direction * (Ratio * 0.5f);
            }

            if (config.valueType == TextConfig.ValueType.Percentage)
                label.text = (image.fillAmount * 100).ToString($"f{config.decimalCount}") + "%";
            else
                label.text = this.item.Value.ToString($"f{config.decimalCount}");

            label.color = item.ColorText;
        }
        #endregion
    }
}

