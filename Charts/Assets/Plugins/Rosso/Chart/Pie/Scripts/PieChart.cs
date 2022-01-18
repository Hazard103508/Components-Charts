using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Rosso.Charts.Pie
{
    public class PieChart : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Templates templates;
        [SerializeField] private Pie pie;
        [SerializeField] private Label labels;
        public Events events;

        private List<Item> items = new List<Item>();
        private GameObject itemsFolder;
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
            Add_ItemsFolder();

            this.items = this.items.Where(i => i.Value > 0).ToList();

            float ratio = ((RectTransform)transform).rect.width / 2;
            float totalValue = this.items.Any() ? this.items.Sum(item => item.Value) : 0;
            float rotation = 0;

            this.items.ForEach(item =>
            {
                var child = Instantiate(templates.pieSection, itemsFolder.transform);
                child.name = "Item - " + item.Label;
                item.PercentageVaue = totalValue != 0 ? item.Value / totalValue : 0;

                var section = child.GetComponent<PieSection>();
                section.Ratio = ratio;
                section.Initialize(item, rotation, pie, labels);
                section.onPointerEnter.AddListener(OnItemPointerEnter);
                section.onPointerExit.AddListener(OnItemPointerExit);
                section.onPointerDown.AddListener(OnItemPointerDown);
                section.onPointerUp.AddListener(OnItemPointerUp);
                section.onPointerClick.AddListener(OnItemPointerClick);

                rotation += item.PercentageVaue * 360;
            });

            GetComponent<Image>().enabled = false;
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
            public GameObject pieSection;
        }
        [Serializable]
        public class Pie
        {
            public float spacing;
        }
        [Serializable]
        public class Label
        {
            /// <summary>
            /// Tipo de valor a mostrar en el grafico
            /// </summary>
            public ValueType valueType;
            /// <summary>
            /// Ubicacion proporcional del label
            /// </summary>
            public float location;
            /// <summary>
            /// Formato personalizado
            /// </summary>
            public string Format;
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
        public enum ValueType
        {
            Percentage,
            Original,
        }
        #endregion
    }
}

