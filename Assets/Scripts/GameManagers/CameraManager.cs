using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField]private float mouseSensitivity = 5f; // a variable to determine how much the camera moves per unit the mouse moves
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

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
        boatCamPitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity; // gets the input from mouse movement in the vertical axis and multiplies it by the sensitivity variable
        boatCamPitch = Mathf.Clamp(boatCamPitch, -10f, 10f); // the pitch of the camera cannot exceed 10 above or below 0
        boatCamYaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity; // gets the input from mouse movement in the horizontal axis and multiplies it by the sensitivity variable
        thirdPersonFocus.transform.eulerAngles = new Vector3(boatCamPitch, boatCamYaw, 0f); // moves the camera by the amount defined by the mouse movement
    }
}
