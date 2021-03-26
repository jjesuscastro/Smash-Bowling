using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitGauge : MonoBehaviour
{
    public Slider arrowSlider;
    float sliderIncrement = 0.02f;

    Slider slider;
    float[] hitValues = { 0.06f, 0.133f, 0.217f, 0.315f, 0.408f, 0.52f, 0.628f, 0.741f, 0.863f, 1f };
    int index;

    public bool isHitting = false;
    bool minReached = true;

    int sphereCounter = 0;

    float hitTimer = 2f;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = hitValues[0];
    }

    void Start()
    {
        arrowSlider.gameObject.SetActive(false);
        arrowSlider.value = 0;
    }

    void Update()
    {
        if (isHitting)
        {
            if (hitTimer > 0)
            {
                hitTimer -= Time.deltaTime;
                if (minReached)
                {
                    arrowSlider.value = Mathf.Clamp(arrowSlider.value + sliderIncrement, 0, slider.value);
                    if (arrowSlider.value >= slider.value)
                        minReached = false;
                }
                else
                {
                    arrowSlider.value = Mathf.Clamp(arrowSlider.value - sliderIncrement, 0, slider.value);
                    if (arrowSlider.value <= 0)
                        minReached = true;
                }
            }
        }
    }

    public void SetSphereCounter(int sphereCounter)
    {
        this.sphereCounter = sphereCounter > 5 ? 5 : sphereCounter;

        Debug.Log("[HitGauge.cs] - Sphere Counter: " + sphereCounter);
    }

    public void StartHit()
    {
        arrowSlider.gameObject.SetActive(true);
        sliderIncrement = slider.value * .021f;
        arrowSlider.value = 0;
        isHitting = true;
    }

    public void HitUp()
    {
        Debug.Log("[HitGauge.cs] - hitvalues/SphereCounter" + (hitValues.Length / sphereCounter));
        index = Mathf.Clamp(index + hitValues.Length / sphereCounter, 0, hitValues.Length - 1);
        slider.value = hitValues[index];
    }

    public void HitDown()
    {
        index = Mathf.Clamp(index - hitValues.Length / sphereCounter, 0, hitValues.Length - 1);
        slider.value = hitValues[index];
    }
}
