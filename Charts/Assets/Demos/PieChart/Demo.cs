using Rosso.Charts;
using Rosso.Charts.Pie;
using UnityEngine;

namespace Rosso.Demo.PieChart
{
    public class Demo : MonoBehaviour
    {
        public Rosso.Charts.Pie.PieChart chartA;
        public Rosso.Charts.Pie.PieChart chartB;
        public Rosso.Charts.Pie.PieChart chartC;
        public Rosso.Charts.Pie.PieChart chartD;

        void Start()
        {
            chartA.Add_Item(new Item("Germany", 500, Color.green));
            chartA.Add_Item(new Item("United Kingdom", 450, Color.red));
            chartA.Add_Item(new Item("France", 800, Color.blue, Color.white));
            chartA.Add_Item(new Item("Italy", 200, Color.yellow));
            chartA.Add_Item(new Item("Spain", 350, Color.gray));
            chartA.Draw();

            chartB.Add_Item(new Item("Yes", 800, Color.cyan));
            chartB.Add_Item(new Item("No", 350, Color.magenta, Color.white));
            chartB.Draw();

            chartC.Add_Item(new Item("Green", 0.589f, Color.green));
            chartC.Add_Item(new Item("Red", 0.257f, Color.red));
            chartC.Add_Item(new Item("Blue", 1.2f, Color.blue, Color.white));
            chartC.Add_Item(new Item("Yellow", 0.879f, Color.yellow));
            chartC.Draw();

            chartD.Add_Item(new Item("English", 17.15f, Color.magenta));
            chartD.Add_Item(new Item("French", 15.6f, Color.blue, Color.white));
            chartD.Add_Item(new Item("Italian", 12.3f, Color.yellow));
            chartD.Draw();
        }
    }
}
