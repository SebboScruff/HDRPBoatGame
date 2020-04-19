using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : BoatMovement // since the player is a boat the player movement is derived from the standard boat movement, but with more inputs.
{
    public GameObject playerGroup;
    public GameObject steeringWheel;

    Vector3 spawnPos;

    [Header("UI Elements")]
    public Image healthBar;
    public Image cannonCooldownBar;
    public TextMeshProUGUI LootText;

    public Camera thirdPersonCam, firstPersonCam;
    private bool isCamAxisInUse = false;
    CameraModes currentCamMode = CameraModes.thirdPerson;

    // Start is called before the first frame update
    void Start()
    {
        fireCD = 0f;

        spawnPos = transform.position;

        lootAmount = 0;
        maxHP = 100;
        currentHP = maxHP;
    }

    // Update is called once per frame
    new void Update()
    {
        //movement controls
        transform.eulerAngles += new Vector3(0f, Input.GetAxisRaw("Horizontal"), 0f) * turnSpeed * Time.deltaTime; // standard A/D rotatation controls 
        steeringWheel.transform.localEulerAngles += new Vector3(0f, 0f, Input.GetAxisRaw("Horizontal")) * turnSpeed * Time.deltaTime; // steering wheel animation
        base.Update(); // this is just the basic boat movement from the parent class
        playerGroup.transform.position = transform.position; // ensure the player group is always in the same place as the boat

        if(Input.GetAxisRaw("Fire1") > 0 && fireCD <= 0)
        {
            base.Shoot(0);
        }
        if (Input.GetAxisRaw("Fire2") > 0 && fireCD <= 0)
        {
            base.Shoot(1);
        }

        if (Input.GetAxis("CamChange") != 0 && isCamAxisInUse == false)
        {
            if(currentCamMode == CameraModes.thirdPerson)
            {
                currentCamMode = CameraModes.firstPerson;
                isCamAxisInUse = true;

            }
            else
            { 
                currentCamMode = CameraModes.thirdPerson;
                isCamAxisInUse = true;

            }
        }
        if(Input.GetAxis("CamChange") == 0)
        {
            isCamAxisInUse = false;
        }

        //Debug.Log(fireCD);

        //UI UPDATE
        LootText.text = "Current Loot: " + lootAmount.ToString("00");
        healthBar.fillAmount = currentHP / maxHP;
        cannonCooldownBar.fillAmount = 1 - (fireCD / 5);

        switch(currentCamMode)
        {
            case CameraModes.thirdPerson:
                thirdPersonCam.gameObject.SetActive(true);
                firstPersonCam.gameObject.SetActive(false);
                break;
            case CameraModes.firstPerson:
                firstPersonCam.gameObject.SetActive(true);
                thirdPersonCam.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public override void Die()
    {
        for(int i = 1; i <= lootAmount; i++)
        {
            Instantiate(treasureChestPrefab);
        }
        transform.position = spawnPos;
    }

    enum CameraModes
    {
        firstPerson,
        thirdPerson
    }
}
