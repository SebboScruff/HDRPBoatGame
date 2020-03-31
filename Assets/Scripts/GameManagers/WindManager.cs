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

    [Range(5, 10)]
    public int minWindSpeed = 5;

    [Range(11, 30)]
    public int maxWindSpeed = 20;

    [Range(30, 90)]
    public int windChangeRate = 60;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeWind", 0f, windChangeRate);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(transform.eulerAngles != newWindAngle)
        {
            transform.eulerAngles += (newWindAngle - transform.eulerAngles) / 1200f;
        }*/
        transform.eulerAngles = newWindAngle; // [DEBUG VERSION]

        // UI Stuff
        Vector3 indicatorRotation = new Vector3(0f, 0f, -transform.eulerAngles.y);
        windIndicator.transform.eulerAngles = indicatorRotation;
        windText.text = "Wind: " + windSpeed.ToString("0.0") + " knots";

        transform.position = relocator.transform.position; // keeps the arrow in the same place relative to the boat
    }

    void ChangeWind()
    {
        targetAngle = Random.Range(-179, 180);
        windSpeed = Random.Range(minWindSpeed, maxWindSpeed);
        newWindAngle = new Vector3(0f, targetAngle, 0f);
        Vector3 change = (newWindAngle - transform.eulerAngles) / 10f;
    }
}
