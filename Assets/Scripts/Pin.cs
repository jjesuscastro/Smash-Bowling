using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pin : MonoBehaviour
{
    public UnityEvent onPinTipped;
    bool pinTipped;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("BowlingZone"))
        {
            if (!pinTipped)
            {
                Debug.Log("[Pin.cs] - Pin tipped");
                if (onPinTipped != null)
                    onPinTipped.Invoke();

                pinTipped = true;
            }
        }
    }
}
