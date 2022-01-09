using Rosso.Charts.Bar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosso.Demo.BarChart
{
    public class Demo : MonoBehaviour
    {
        public Rosso.Charts.Bar.BarChart chartA;
        public Rosso.Charts.Bar.BarChart chartB;
        public Rosso.Charts.Bar.BarChart chartC;

        // Start is called before the first frame update
        void Start()
        {
            chartA.Add_Item(new Item("Germany", 500));
            chartA.Add_Item(new Item("United Kingdom", 450));
            chartA.Add_Item(new Item("France", 800));
            chartA.Add_Item(new Item("Italy", 200));
            chartA.Add_Item(new Item("Spain", 350));
            chartA.Draw();

            chartB.Add_Item(new Item("Green", 0.589f, Color.green));
            chartB.Add_Item(new Item("Red", 0.257f, Color.red));
            chartB.Add_Item(new Item("Blue", 1.2f, Color.blue, Color.white));
            chartB.Add_Item(new Item("Yellow", 0.879f, Color.yellow));
            chartB.Draw();

            for (int i = 2000; i < 2025; i++)
            {
                float value = Random.Range(0.00100f, 0.02150f);
                chartC.Add_Item(new Item(i.ToString(), value));
            }
            chartC.Draw();
        }

    }
}
