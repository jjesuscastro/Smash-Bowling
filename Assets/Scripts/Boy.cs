using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    [HideInInspector]
    public InGameColor color = InGameColor.Green;

    public CharacterController characterController;
    public HitGauge gauge;
    public float speed;
    public float sideSpeed;
    public float speedDecay;
    public Ball ball;
    public MeshRenderer ballRenderer;
    public SkinnedMeshRenderer[] body;
    public int pixelDistToDetect = 20;

    float screenWidth;
    Vector2 startPos;
    bool fingerDown;
    bool isStopping;
    bool movementDisabled = false;

    float hitStrength = 0;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        screenWidth = (float)Screen.width / 2.0f;
    }

    void Update()
    {
        #region movement
        Vector3 direction = new Vector3(0, 0, 1).normalized;
        characterController.Move(direction * speed * Time.deltaTime);

        if (!movementDisabled && !fingerDown && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            startPos = touch.position;
            fingerDown = true;
        }

        if (!movementDisabled && fingerDown && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 swipeDirection = new Vector3(0, 0, 0);

            if (touch.position.x >= startPos.x + pixelDistToDetect)
            {
                swipeDirection = new Vector3(1, 0, 0);

                fingerDown = false;
                Debug.Log("[Boy.cs] - Swipe Right");
            }
            else if (touch.position.x <= startPos.x - pixelDistToDetect)
            {
                swipeDirection = new Vector3(-1, 0, 0);

                fingerDown = false;
                Debug.Log("[Boy.cs] - Swipe Left");
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = false;
            }

            characterController.Move(swipeDirection * sideSpeed * Time.deltaTime);
            #endregion
        }

        if (isStopping)
        {
            speed = Mathf.Clamp(speed - speedDecay, 0, 100f);
            Vector3 ballPosition = ball.transform.localPosition;
            ballPosition.z += 0.2f * Time.deltaTime;
            ball.transform.localPosition = ballPosition;
        }

        if (gauge.isHitting && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                gauge.isHitting = false;
            }
        }

    }

    void SetColor(InGameColor color)
    {
        this.color = color;

        ballRenderer.material = MaterialCatalog.instance.GetColor(color.ToString());
        foreach (SkinnedMeshRenderer renderer in body)
            renderer.material = MaterialCatalog.instance.GetColor(color.ToString());
    }

    void SpherePickedup(bool correctPickup)
    {
        if (correctPickup)
        {
            ball.AddSize();
            gauge.HitUp();
        }
        else
        {
            ball.MinusSize();
            gauge.HitDown();
        }
    }

    public InGameColor GetColor()
    {
        return color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("GoalLine"))
        {
            // Debug.Log("[Boy.cs] - Passed goal line");
            movementDisabled = true;
            isStopping = true;
            animator.Play("kick_ani");
            gauge.StartHit();
            StartCoroutine(KickBall());
        }
        else if (other.gameObject.tag.Equals("Gate"))
        {
            Gate gate = other.gameObject.GetComponent<Gate>();
            SetColor(gate.GetColor());
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Sphere"))
        {
            SpherePickup sphere = other.gameObject.GetComponent<SpherePickup>();
            InGameColor sphereColor = sphere.GetColor();

            if (color == sphereColor)
                SpherePickedup(true);
            else
                SpherePickedup(false);

            Destroy(other.transform.parent.gameObject);
        }
    }

    IEnumerator KickBall()
    {
        yield return new WaitForSeconds(2f);

        hitStrength = gauge.arrowSlider.value;
        Rigidbody rBody = ball.gameObject.GetComponent<Rigidbody>();
        SphereCollider sCollider = ball.gameObject.GetComponent<SphereCollider>();
        sCollider.isTrigger = false;
        rBody.AddForce(new Vector3(0, 0, 1000 * hitStrength), ForceMode.Impulse);

        StartCoroutine(LevelController.instance.ActivateLevelClear());
    }
}

public enum InGameColor
{
    Green,
    Red,
    Yellow
}