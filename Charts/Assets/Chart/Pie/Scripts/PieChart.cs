using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rosso.Charts.Pie
{
    public class PieChart : MonoBehaviour
    {
        #region Objects
        [SerializeField] private GameObject pieSectionTemplate;
        [SerializeField] private TextConfig textConfig;
        private List<Item> items = new List<Item>();
        private GameObject itemsFolder;
        #endregion

        #region Events
        public ItemEvent onSelectItem;
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
            itemsFolder = new GameObject("Items");
            itemsFolder.transform.SetParent(this.transform);
            var rec = itemsFolder.AddComponent<RectTransform>();
            rec.anchorMin = new Vector2(0, 0);
            rec.anchorMax = new Vector2(1, 1);
            rec.SetAll(new RectOffset());

            this.items = this.items.Where(i => i.Value > 0).ToList();

            float ratio = (((RectTransform)transform).sizeDelta / 2).x;
            float totalValue = this.items.Any() ? this.items.Sum(item => item.Value) : 0;
            float rotation = 0;

            this.items.ForEach(item =>
            {
                var child = Instantiate(pieSectionTemplate, itemsFolder.transform);
                child.name = "Item - " + item.Label;
                item.PercentageVaue = totalValue != 0 ? item.Value / totalValue : 0;

                var section = child.GetComponent<PieSection>();
                section.Ratio = ratio;
                section.Initialize(item, rotation, textConfig);
                section.onSelectItem.AddListener(OnSelectItem);

                rotation += item.PercentageVaue * 360;
            });
        }
        #endregion

        #region Private Methods
        private void OnSelectItem(Item item)
        {
            onSelectItem.Invoke(item);
        }
        #endregion
    }
}

