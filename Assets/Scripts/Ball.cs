using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 firstSize;
    public Vector3 firstPosition;
    public Vector3 secondSize;
    public Vector3 secondPosition;
    public Vector3 thirdSize;
    public Vector3 thirdPosition;

    int size = 1;

    public void AddSize()
    {
        size = Mathf.Clamp(size + 1, 1, 3);
        SetSize(size);
    }

    public void MinusSize()
    {
        size = Mathf.Clamp(size - 1, 1, 3);
        SetSize(size);
    }

    void SetSize(int size)
    {
        if (size == 1)
        {
            transform.localScale = firstSize;
            transform.localPosition = firstPosition;
        }
        else if (size == 2)
        {
            transform.localScale = secondSize;
            transform.localPosition = secondPosition;
        }
        else
        {
            transform.localScale = thirdSize;
            transform.localPosition = thirdPosition;
        }
    }
}
