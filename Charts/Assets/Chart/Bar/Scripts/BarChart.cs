using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Rosso.Charts.Bar
{
    public class BarChart : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Templates templates;
        [SerializeField] private Bars bars;
        [SerializeField] private Lines horizontalLines;
        public Events events;

        private List<Item> items = new List<Item>();
        private GameObject itemsFolder;
        private GameObject linesFolder;
        #endregion

        #region Public Methods
        /// <summary>
        /// Restaura el chart al estado inicial
        /// </summary>
        public void Clean()
        {
            Destroy(itemsFolder);
            items.Clear();
        }
        /// <summary>
        /// Agrega un nuevo item al grafico
        /// </summary>
        /// <param name="item">Item a agregar al grafico</param>
        public void Add_Item(Item item)
        {
            this.items.Add(item);
        }
        /// <summary>
        /// Dibuja el grafico para 
        /// </summary>
        public void Draw()
        {
            if (!this.items.Any())
                return;

            this.items = this.items.Where(i => i.Value > 0).ToList();

            float maxHeight = Get_MaxHeight();

            Add_Bars(maxHeight);
            Add_HorizontalLines(maxHeight);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Agrega la carpeta para los items
        /// </summary>
        private void Add_ItemsFolder()
        {
            itemsFolder = new GameObject("Items");
            itemsFolder.transform.SetParent(this.transform);
            var rec = itemsFolder.AddComponent<RectTransform>();
            rec.anchorMin = new Vector2(0, 0);
            rec.anchorMax = new Vector2(1, 1);
            rec.SetAll(0, 0, 0, 0);
        }
        /// <summary>
        /// Agrega la carpeta para las lineas
        /// </summary>
        private void Add_LineFolder()
        {
            linesFolder = new GameObject("Lines");
            linesFolder.transform.SetParent(this.transform);
            var rec = linesFolder.AddComponent<RectTransform>();
            rec.anchorMin = new Vector2(0, 0);
            rec.anchorMax = new Vector2(1, 1);
            rec.SetAll(0, 0, 0, 0);
        }
        /// <summary>
        /// Obtiene la maxima altura de las barras
        /// </summary>
        /// <returns></returns>
        private float Get_MaxHeight()
        {
            float max = this.items.Max(item => item.Value);
            string strMax = max.ToString("f10");

            float _newNumber = 0;
            for (int i = 1; i < strMax.Length; i++)
            {
                char _prev = strMax[i - 1];
                if (char.IsNumber(_prev) && _prev != '0')
                {
                    char _current = strMax[i];
                    int n = char.IsNumber(_current) ? 1 : 2;

                    string _strNumber = string.Join("", strMax.Take(i + n).ToArray());
                    float.TryParse(_strNumber, out _newNumber);

                    if (_newNumber % 1 != 0)
                    {
                        float pow = i > 1 ? i - 1 : 1;
                        _newNumber += Mathf.Pow(0.1f, pow);
                    }
                    else
                        _newNumber += 1;

                    break;
                }
            }

            float digits = ((int)max).ToString().Length;
            if (digits > 2)
                _newNumber *= Mathf.Pow(10, digits - 2);

            return _newNumber;
        }
        /// <summary>
        /// Dibuja las baras
        /// </summary>
        /// <param name="maxHeight">Alto maximo de las barras</param>
        private void Add_Bars(float maxHeight)
        {
            Add_ItemsFolder();

            var rec = (RectTransform)transform;
            Vector2 backgroundSize = new Vector2(rec.rect.width, rec.rect.height) - new Vector2(this.bars.padding.left + this.bars.padding.right, this.bars.padding.top + this.bars.padding.bottom) - new Vector2((this.items.Count - 1) * this.bars.spacing, 0);
            float barWidth = backgroundSize.x / this.items.Count;

            Vector2 startPos = new Vector2(this.bars.padding.left, this.bars.padding.bottom) - new Vector2(rec.rect.width, rec.rect.height) / 2;
            this.items.ForEach(item =>
            {
                var child = Instantiate(templates.bar, itemsFolder.transform);
                child.name = "Item - " + item.Label;
                child.transform.localPosition = startPos;

                var cRec = (RectTransform)child.transform;
                float barHeight = (item.Value / maxHeight) * backgroundSize.y;
                cRec.sizeDelta = new Vector2(barWidth, barHeight);

                var barItem = child.GetComponent<Bar>();
                barItem.onPointerEnter.AddListener(OnItemPointerEnter);
                barItem.onPointerExit.AddListener(OnItemPointerExit);
                barItem.onPointerDown.AddListener(OnItemPointerDown);
                barItem.onPointerUp.AddListener(OnItemPointerUp);
                barItem.onPointerClick.AddListener(OnItemPointerClick);
                barItem.Initialize(item, this.bars);

                startPos += new Vector2(barWidth + this.bars.spacing, 0);
            });
        }
        /// <summary>
        /// Agrega las lineas horizontales al grafico
        /// </summary>
        /// <param name="maxHeight">Alto maximo de las barras</param>
        private void Add_HorizontalLines(float maxHeight)
        {
            Add_LineFolder();

            var rec = (RectTransform)transform;
            Vector2 backgroundSize = new Vector2(rec.rect.width, rec.rect.height) - new Vector2(this.bars.padding.left + this.bars.padding.right, this.bars.padding.top + this.bars.padding.bottom);

            var vertical = linesFolder.AddComponent<VerticalLayoutGroup>();
            vertical.padding.right = this.bars.padding.right;
            vertical.padding.top = this.bars.padding.top;
            vertical.padding.bottom = this.bars.padding.bottom;

            float interval = maxHeight / this.horizontalLines.count;
            vertical.spacing = backgroundSize.y * interval / maxHeight;

            List<float> lstInterval = new List<float>();
            for (int i = 0; i < this.horizontalLines.count; i++)
                lstInterval.Add(interval * i);
            lstInterval.Add(maxHeight);
            lstInterval = lstInterval.OrderByDescending(obj => obj).ToList();

            foreach (float value in lstInterval)
            {
                var child = Instantiate(templates.horizontalLines, linesFolder.transform);
                child.name = $"Line - {value}";

                var line = child.GetComponent<HorizontalLine>();

                float? height = value == 0 ? (float?)5f : null;
                line.Initialize(value, this.bars.padding.left, this.horizontalLines, height);
            }
        }
        private void OnItemPointerEnter(Item item)
        {
            events.onItemPointerEnter.Invoke(item);
        }
        private void OnItemPointerExit(Item item)
        {
            events.onItemPointerExit.Invoke(item);
        }
        private void OnItemPointerDown(Item item)
        {
            events.onItemPointerDown.Invoke(item);
        }
        private void OnItemPointerUp(Item item)
        {
            events.onItemPointerUp.Invoke(item);
        }
        private void OnItemPointerClick(Item item)
        {
            events.onItemPointerClick.Invoke(item);
        }
        #endregion


        #region Structures
        [Serializable]
        public class Templates
        {
            public GameObject bar;
            public GameObject horizontalLines;
        }
        [Serializable]
        public class Bars
        {
            public RectOffset padding;
            public float spacing;
            public Color defaultColor;
            public string valueFormat;
        }
        [Serializable]
        public class Lines
        {
            public string valueFormat;
            public int count;
        }
        [Serializable]
        public class Events
        {
            public ItemEvent onItemPointerEnter;
            public ItemEvent onItemPointerExit;
            public ItemEvent onItemPointerDown;
            public ItemEvent onItemPointerUp;
            public ItemEvent onItemPointerClick;
        }
        #endregion
    }
}