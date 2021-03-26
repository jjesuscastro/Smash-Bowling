using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePickup : MonoBehaviour
{
    public InGameColor color = InGameColor.Green;
    public InGameColor GetColor()
    {
        return color;
    }
}
