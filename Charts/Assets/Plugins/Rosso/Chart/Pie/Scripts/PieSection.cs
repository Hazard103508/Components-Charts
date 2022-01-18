using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rosso.Charts.Pie
{
    public class PieSection : MonoBehaviour, ICanvasRaycastFilter, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region Objects
        [SerializeField] private Image mask;
        [SerializeField] private Image image;
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

        #region Properties
        /// <summary>
        /// Valor del radio de circulo del chart
        /// </summary>
        public float Ratio { get; set; }
        #endregion

        #region Unity Methods
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
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            // Obtengo la posición relativa del mouse desde el centro del chart
            Vector2 mousePos = sp - (Vector2)this.transform.position;

            // Obtengo el ángulo en grados que forma la magnitud del vector
            float angle = -Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;

            // Rango de ángulos que utiliza esta porción del grafico
            float angleMin = this.mask.transform.localRotation.eulerAngles.z;
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
        /// <param name="pie">configuracion del grafico</param>
        /// <param name="labels">configuracion de las etiquetas</param>
        public void Initialize(Item item, float rotation, Rosso.Charts.Pie.PieChart.Pie pie ,Rosso.Charts.Pie.PieChart.Label labels)
        {
            this.item = item;
            image.color = item.Color;
            mask.fillAmount = this.item.PercentageVaue;
            mask.transform.Rotate(Vector3.forward * rotation);

            if (this.item.PercentageVaue == 1)
                label.transform.localPosition = Vector3.zero;
            else
            {
                float centerAngle = (rotation + ((this.item.PercentageVaue * 360) / 2)) * Mathf.Deg2Rad;
                var direction = new Vector3(-Mathf.Sin(centerAngle), Mathf.Cos(centerAngle), 0f).normalized; // direcion hacia fuera del circulo que posee esta seccion del chart
                label.transform.localPosition = direction * this.Ratio * labels.location;

                Set_Spacing(pie, centerAngle);
            }

            float value = labels.valueType == Rosso.Charts.Pie.PieChart.ValueType.Percentage ? (mask.fillAmount * 100) : this.item.Value;
            label.text = string.Format(labels.Format, value);

            label.color = item.ColorText;
        }
        /// <summary>
        /// Setea el margen entre las porciones del grafico
        /// </summary>
        /// <param name="pie">configuracion del grafico</param>
        /// <param name="centerAngle">angulo dentral de la porcion</param>
        private void Set_Spacing(Rosso.Charts.Pie.PieChart.Pie pie, float centerAngle)
        {
            if (pie.spacing > 0)
            {
                float _angle = ((this.item.PercentageVaue * 360) / 2) * Mathf.Deg2Rad;
                float r = pie.spacing / Mathf.Sin(_angle);

                var newPos = new Vector3(Mathf.Sin(centerAngle) * r, Mathf.Cos(centerAngle) * r);
                var maskRec = (RectTransform)mask.transform;
                if (newPos.x < 0)
                {
                    maskRec.SetLeft(Mathf.Abs(newPos.x));
                    maskRec.SetRight(newPos.x);
                }
                else if (newPos.x > 0)
                {
                    maskRec.SetLeft(-newPos.x);
                    maskRec.SetRight(newPos.x);
                }

                if (newPos.y < 0)
                {
                    maskRec.SetBottom(newPos.y);
                    maskRec.SetTop(Mathf.Abs(newPos.y));
                }
                else if (newPos.y > 0)
                {
                    maskRec.SetBottom(newPos.y);
                    maskRec.SetTop(-newPos.y);
                }

                image.transform.position = this.transform.position;
            }
        }
        #endregion
    }
}

