using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rosso.Charts.Pie
{
    public class PieChart : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Templates templates;
        [SerializeField] private Label labels;
        private List<Item> items = new List<Item>();
        private GameObject itemsFolder;
        #endregion

        #region Events
        public ItemEvent onItemPointerEnter;
        public ItemEvent onItemPointerExit;
        public ItemEvent onItemPointerDown;
        public ItemEvent onItemPointerUp;
        public ItemEvent onItemPointerClick;
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
                section.Initialize(item, rotation, labels);
                section.onPointerEnter.AddListener(OnItemPointerEnter);
                section.onPointerExit.AddListener(OnItemPointerExit);
                section.onPointerDown.AddListener(OnItemPointerDown);
                section.onPointerUp.AddListener(OnItemPointerUp);
                section.onPointerClick.AddListener(OnItemPointerClick);

                rotation += item.PercentageVaue * 360;
            });
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
            rec.SetAll(new RectOffset());
        }
        private void OnItemPointerEnter(Item item)
        {
            onItemPointerEnter.Invoke(item);
        }
        private void OnItemPointerExit(Item item)
        {
            onItemPointerExit.Invoke(item);
        }
        private void OnItemPointerDown(Item item)
        {
            onItemPointerDown.Invoke(item);
        }
        private void OnItemPointerUp(Item item)
        {
            onItemPointerUp.Invoke(item);
        }
        private void OnItemPointerClick(Item item)
        {
            onItemPointerClick.Invoke(item);
        }
        #endregion

        #region Structures
        [Serializable]
        public class Templates
        {
            public GameObject pieSection;
        }
        [Serializable]
        public class Label
        {
            /// <summary>
            /// Tipo de valor a mostrar en el grafico
            /// </summary>
            public ValueType valueType;
            /// <summary>
            /// Cantidad de decimales a mostrar en el valor
            /// </summary>
            public int decimalCount;


        }
        public enum ValueType
        {
            Percentage,
            Original
        }
        #endregion
    }
}

