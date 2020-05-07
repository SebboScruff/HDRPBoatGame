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

    private int windChangeRate = 30;
    public int WindChangeRate
    {
        get { return windChangeRate; }
        set { windChangeRate = value; }
    }

    [Header("Storm Variables")]
    [Range(0, 100)]
    public int stormChance = 20;

    [SerializeField]public bool isStormHappening = false;

    public int minStormSpeed = 20;
    public int maxStormSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeWind", 0f, WindChangeRate); // repeatedly calls the method to change the wind speed and direction every minute or so
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
        int rnd = Random.Range(0, 100); // generate a random number to determine whether or not there is a storm
        if(rnd < stormChance)
        {
            //storm happens
            isStormHappening = true;
            //Debug.Log("Storm is happening");
            windSpeed = Random.Range(minStormSpeed, maxStormSpeed); // the minimum and maximum wind speeds during a storm are substantially higher, making all boats move faster
        }
        else
        {
            //storm does not happen
            isStormHappening = false;
            //Debug.Log("Storm is not happening");
            windSpeed = Random.Range(minWindSpeed, maxWindSpeed); // sets the new wind speed to be a random float within the allocated range
        }

        targetAngle = Random.Range(-179, 180); // sets a target angle anywhere within a full circle
        newWindAngle = new Vector3(0f, targetAngle, 0f); // sets the new wind angle to be a random float within the allocated range
        Vector3 change = (newWindAngle - transform.eulerAngles) / 10f; // ***UNUSED***
    }
}
