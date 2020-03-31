using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    bool isSailing = true;

    [Range(1, 10)]
    public float mouseSensitivity = 5f;

    public GameObject thirdPersonFocus;

    // BOAT CAMERA VARIABLES
    float boatCamPitch;
    float boatCamYaw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // THIRD PERSON BOAT CAMERA
        if(isSailing)
        {
            boatCamPitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
            boatCamPitch = Mathf.Clamp(boatCamPitch, -10f, 10f);
            boatCamYaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            thirdPersonFocus.transform.eulerAngles = new Vector3(boatCamPitch, boatCamYaw, 0f);
        }


    }
}
