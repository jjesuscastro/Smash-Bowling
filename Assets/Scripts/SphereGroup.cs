using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGroup : MonoBehaviour
{
    public MeshRenderer[] ballsRenderer;

    InGameColor[] colors = { InGameColor.Red, InGameColor.Yellow, InGameColor.Green };

    public void OneColor(InGameColor color)
    {
        for (int i = 0; i < 3; i++)
        {
            SpherePickup sPickup = ballsRenderer[i].GetComponent<SpherePickup>();
            sPickup.color = color;

            Material material = MaterialCatalog.instance.GetColor(sPickup.color.ToString());
            ballsRenderer[i].material = material;
        }
    }

    public void RandomColor()
    {
        Shuffle(colors);

        for (int i = 0; i < 3; i++)
        {
            SpherePickup sPickup = ballsRenderer[i].GetComponent<SpherePickup>();
            sPickup.color = colors[i];

            ballsRenderer[i].material = MaterialCatalog.instance.GetColor(sPickup.color.ToString());
        }
    }

    void Shuffle(InGameColor[] colors)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < colors.Length; t++)
        {
            InGameColor tmp = colors[t];
            int r = Random.Range(t, colors.Length);
            colors[t] = colors[r];
            colors[r] = tmp;
        }
    }
}
