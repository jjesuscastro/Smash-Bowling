using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }
}
