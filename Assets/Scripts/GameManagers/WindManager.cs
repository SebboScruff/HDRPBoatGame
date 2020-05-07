using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindManager : MonoBehaviour
{
    public RawImage windIndicator;
    public TextMeshProUGUI windText;

    public GameObject relocator; // this just keeps the arrow at the stern of the boat

    int targetAngle;
    private float windSpeed;
    public float WindSpeed
    {
        get { return windSpeed; }
        set { windSpeed = value; }
    }

    Vector3 newWindAngle;

    [Header("Wind Speeds")]

    [Range(5, 10)]
    public float minWindSpeed = 5f;

    [Range(11, 30)]
    public float maxWindSpeed = 20f;

    [Range(30, 90)]
    private int windChangeRate = 60;
    public int WindChangeRate
    {
        get { return windChangeRate; }
        set { windChangeRate = value; }
    }

    // Storm Variables
    private bool isStormHappening = false;
    public bool IsStormHappening
    {
        get { return isStormHappening; }
        set { isStormHappening = value; }
    }

    public int minStormSpeed = 20;
    public int maxStormSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeWind", 0f, windChangeRate); // repeatedly calls the method to change the wind speed and direction every minute or so
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = newWindAngle;

        // UI Stuff
        Vector3 indicatorRotation = new Vector3(0f, 0f, -transform.eulerAngles.y); // sets a rotation vector for the UI wind direction indicator
        windIndicator.transform.eulerAngles = indicatorRotation; // applies the rotation to the UI object
        windText.text = "Wind: " + windSpeed.ToString("0.0") + " knots"; // displays the current wind speed as text on the UI

        transform.position = relocator.transform.position; // keeps the arrow in the same place relative to the boat
    }

    void ChangeWind()
    {
        targetAngle = Random.Range(-179, 180); // sets a target angle anywhere within a full circle
        windSpeed = Random.Range(minWindSpeed, maxWindSpeed); // sets the new wind speed to be a random float within the allocated range
        newWindAngle = new Vector3(0f, targetAngle, 0f); // sets the new wind angle to be a random float within the allocated range
        Vector3 change = (newWindAngle - transform.eulerAngles) / 10f; // ***UNUSED***
    }
}
