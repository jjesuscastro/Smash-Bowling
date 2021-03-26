using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    ParticleSystem pSystem;
    public InGameColor color = InGameColor.Yellow;

    void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();

        int randomColor = Random.Range(0, 3);
        if (randomColor == 1)
        {
            color = InGameColor.Green;
            ParticleSystem.MainModule main = pSystem.main;
            main.startColor = new Color(0.0901f, 1, 0.0274f, .25f);
        }
        else if (randomColor == 2)
        {
            color = InGameColor.Red;
            ParticleSystem.MainModule main = pSystem.main;
            main.startColor = new Color(0.7372f, 0, 0.0941f, .25f);
        }
        else
        {
            color = InGameColor.Yellow;
        }
    }

    public void SetColor(InGameColor color)
    {
        this.color = color;
        ParticleSystem.MainModule main = pSystem.main;

        if (color == InGameColor.Green)
            main.startColor = new Color(0.0901f, 1, 0.0274f, .25f);
        else if (color == InGameColor.Red)
            main.startColor = new Color(0.7372f, 0, 0.0941f, .25f);
        else
            main.startColor = new Color(0.8705f, 0.9450f, 0.2862f, .25f);

    }

    public InGameColor GetColor()
    {
        return color;
    }
}
