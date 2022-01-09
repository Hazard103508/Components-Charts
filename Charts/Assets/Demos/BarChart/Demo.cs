using Rosso.Charts.Bar;
using UnityEngine;

namespace Rosso.Demo.BarChart
{
    public class Demo : MonoBehaviour
    {
        public Rosso.Charts.Bar.BarChart chartA;
        public Rosso.Charts.Bar.BarChart chartB;
        public Rosso.Charts.Bar.BarChart chartC;

        void Start()
        {
            chartA.Add_Item(new Item("Germany", 500));
            chartA.Add_Item(new Item("United Kingdom", 450));
            chartA.Add_Item(new Item("France", 800));
            chartA.Add_Item(new Item("Italy", 200));
            chartA.Add_Item(new Item("Spain", 350));
            chartA.Draw();

            chartB.Add_Item(new Item("Green", 0.5f, Color.green));
            chartB.Add_Item(new Item("Red", 0.2f, Color.red));
            chartB.Add_Item(new Item("Blue", 1.2f, Color.blue, Color.white));
            chartB.Add_Item(new Item("Yellow", 0.8f, Color.yellow));
            chartB.Add_Item(new Item("Black", 0.9f, Color.black));
            chartB.Add_Item(new Item("White", 0.6f, Color.white));
            chartB.Add_Item(new Item("Orange", 1f, new Color(255/255f, 69/255f, 0)));
            chartB.Add_Item(new Item("Cyan", 0.3f, Color.cyan));
            chartB.Add_Item(new Item("Magenta", 0.7f, Color.magenta));
            chartB.Draw();

            for (int i = 2000; i < 2025; i++)
            {
                float value = Random.Range(0.00100f, 0.02150f);
                chartC.Add_Item(new Item(i.ToString(), value));
            }
            chartC.Draw();
        }

        public void OnItemPointerEnter(Item item)
        {
            print($"Pointer Enter: {item.Label} - {item.Value}");
        }
        public void OnItemPointerExit(Item item)
        {
            print($"Pointer Exit: {item.Label} - {item.Value}");
        }
        public void OnItemPointerDown(Item item)
        {
            print($"Pointer Down: {item.Label} - {item.Value}");
        }
        public void OnItemPointerUp(Item item)
        {
            print($"Pointer Up: {item.Label} - {item.Value}");
        }
        public void OnItemPointerClick(Item item)
        {
            print($"Pointer Click: {item.Label} - {item.Value}");
        }
    }
}
