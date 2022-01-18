using Rosso.Charts.Donut;
using UnityEngine;
using Rosso.Charts;

namespace Rosso.Demo.DonutChart
{
    public class Demo : MonoBehaviour
    {
        public Rosso.Charts.Donut.DonutChart chartA;
        public Rosso.Charts.Donut.DonutChart chartB;
        public Rosso.Charts.Donut.DonutChart chartC;
        public Rosso.Charts.Donut.DonutChart chartD;
        
        void Start()
        {
            chartA.Add_Item(new Item("Germany", 500, Color.green));
            chartA.Add_Item(new Item("United Kingdom", 450, Color.red));
            chartA.Add_Item(new Item("France", 800, Color.blue));
            chartA.Add_Item(new Item("Italy", 200, Color.yellow));
            chartA.Add_Item(new Item("Spain", 350, Color.gray));
            chartA.Draw();

            chartB.Add_Item(new Item("A", 40, Get_Color(116, 215, 147)));
            chartB.Add_Item(new Item("B", 20, Get_Color(146, 224, 170)));
            chartB.Add_Item(new Item("C", 30, Get_Color(176, 232, 194)));
            chartB.Add_Item(new Item("D", 50, Get_Color(205, 241, 217)));
            chartB.Add_Item(new Item("F", 30, Get_Color(236, 249, 240)));
            chartB.Add_Item(new Item("F", 15, Get_Color(22, 135, 255)));
            chartB.Add_Item(new Item("G", 30, Get_Color(57, 152, 255)));
            chartB.Add_Item(new Item("H", 60, Get_Color(91, 170, 255)));
            chartB.Add_Item(new Item("I", 20, Get_Color(126, 188, 255)));
            chartB.Add_Item(new Item("J", 30, Get_Color(160, 206, 255)));
            chartB.Add_Item(new Item("K", 20, Get_Color(195, 224, 255)));
            chartB.Add_Item(new Item("L", 10, Get_Color(229, 242, 255)));
            chartB.Draw();

            chartC.Add_Item(new Item("W", 10, Get_Color(240, 84, 64)));
            chartC.Add_Item(new Item("X", 20, Get_Color(213, 67, 61)));
            chartC.Add_Item(new Item("Y", 30, Get_Color(179, 53, 53)));
            chartC.Add_Item(new Item("Z", 40, Get_Color(140, 35, 0)));
            chartC.Draw();

            chartD.Add_Item(new Item("W", 100, Color.yellow));
            chartD.Add_Item(new Item("X", 150, Color.red));
            chartD.Add_Item(new Item("Y", 200, Color.cyan));
            chartD.Draw();
        }

        public Color Get_Color(byte r, byte g, byte b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public void OnItemPointerEnter(Item item)
        {
            print($"Pointer Enter: {item.Label} - {item.Value} ({item.PercentageVaue.ToString("P")})");
        }
        public void OnItemPointerExit(Item item)
        {
            print($"Pointer Exit:  {item.Label} - {item.Value} ({item.PercentageVaue.ToString("P")})");
        }
        public void OnItemPointerDown(Item item)
        {
            print($"Pointer Down:  {item.Label} - {item.Value} ({item.PercentageVaue.ToString("P")})");
        }
        public void OnItemPointerUp(Item item)
        {
            print($"Pointer Up:  {item.Label} - {item.Value} ({item.PercentageVaue.ToString("P")})");
        }
        public void OnItemPointerClick(Item item)
        {
            print($"Pointer Click:  {item.Label} - {item.Value} ({item.PercentageVaue.ToString("P")})");
        }
    }
}

