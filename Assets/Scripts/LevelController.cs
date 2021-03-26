using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    BoxCollider boxCollider;

    public HitGauge gauge;
    public TMP_Text levelName;
    public GameObject scoreCounter;
    public GameObject levelClear;

    [Header("S for Spheres G for Gates")]
    public string pattern = "SGSGSG";
    public int rows = 4;
    public Transform bowlingPinsParent;
    public GameObject pinPrefab;
    public GameObject sphereGroup;
    public GameObject gate;

    GameManager gameManager;
    int score;


    #region Singleton
    public static LevelController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("[LevelController.cs] - Multiple LevelController(s) found!");
            Destroy(gameObject);
        }

        boxCollider = GetComponent<BoxCollider>();
    }
    #endregion 

    public void StartGame(int levelNumber)
    {
        gameManager = GameManager.instance;
        levelName.text = "Level " + levelNumber.ToString();
        if (levelNumber > 1)
            SetPattern(levelNumber);
        ParsePattern();
        AddPins(levelNumber);
        AddPinListeners();
    }

    public IEnumerator ActivateLevelClear()
    {
        yield return new WaitForSeconds(5f);

        levelClear.SetActive(true);
    }

    void SetPattern(int levelNumber)
    {
        switch (levelNumber)
        {
            case 2:
                pattern = "SSGSSGS";
                break;
            case 3:
                pattern = "SSGSSGS";
                break;
            case 4:
                pattern = "SSGSSGSGS";
                break;
            case 5:
                pattern = "SGSSSGSGS";
                break;
        }
    }

    void AddPins(int levelNumber)
    {
        Debug.Log("[LevelController.cs] - Adding pins");
        //Always instantiate at least 1 pin
        rows = levelNumber + 2;
        Instantiate(pinPrefab, new Vector3(0, 0, 0), Quaternion.identity, bowlingPinsParent).transform.localPosition = new Vector3(0, 0, 0);

        float startX = -0.29f;
        float startZ = 0.58f;
        for (int i = 1; i < rows; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                GameObject newPin = Instantiate(pinPrefab, new Vector3(0, 0, 0), Quaternion.identity, bowlingPinsParent);
                newPin.transform.localPosition = new Vector3(startX + 0.58f * j, 0, startZ + (i - 1) * 0.58f);
                // Pin pin = newPin.GetComponent<Pin>();
                // pin.onPinTipped.AddListener(AddScore);
            }
            startX -= 0.29f;
        }
    }

    void AddPinListeners()
    {
        foreach (Transform child in bowlingPinsParent)
        {
            Pin pin = child.gameObject.GetComponent<Pin>();
            if (pin != null)
                pin.onPinTipped.AddListener(AddScore);
        }
    }

    void AddScore()
    {
        scoreCounter.SetActive(true);
        score += 1;
        TMP_Text text = scoreCounter.GetComponent<TMP_Text>();
        text.text = score.ToString();
    }

    void ParsePattern()
    {
        Queue<char> patternQueue = new Queue<char>(pattern.ToCharArray());
        int count = patternQueue.Count;
        int sphereCounter = 0;
        int counter = 0;
        bool doubleCount = false;

        while (patternQueue.Count > 0)
        {
            char c = patternQueue.Dequeue();
            if (c.Equals('S'))
            {
                SphereGroup sGroup = Instantiate(sphereGroup, new Vector3(0, 0, boxCollider.bounds.min.z + (boxCollider.size.z / count) * counter), Quaternion.identity).GetComponent<SphereGroup>();
                sGroup.RandomColor();
                sphereCounter++;
                counter++;
            }
            else if (c.Equals('r') || c.Equals('g') || c.Equals('y'))
            {
                if (!doubleCount)
                {
                    count *= 2;
                    doubleCount = true;
                }

                InGameColor color = InGameColor.Yellow;
                Gate newGate = Instantiate(gate, new Vector3(0, 0, boxCollider.bounds.min.z + (boxCollider.size.z / count) * counter), Quaternion.identity).GetComponent<Gate>();
                counter++;
                SphereGroup sGroup = Instantiate(sphereGroup, new Vector3(0, 0, boxCollider.bounds.min.z + (boxCollider.size.z / count) * counter), Quaternion.identity).GetComponent<SphereGroup>();


                if (c.Equals('r'))
                    color = InGameColor.Red;
                else if (c.Equals('g'))
                    color = InGameColor.Green;
                else if (c.Equals('y'))
                    color = InGameColor.Yellow;

                sGroup.OneColor(color);
                newGate.SetColor(color);
                sphereCounter++;
                counter++;
                continue;
            }
            else if (c.Equals('G'))
            {
                Instantiate(gate, new Vector3(0, 0, boxCollider.bounds.min.z + (boxCollider.size.z / count) * counter), Quaternion.identity);
                counter++;
            }
        }

        gauge.SetSphereCounter(sphereCounter);
    }
}
