using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : BoatMovement // since the player is a boat the player movement is derived from the standard boat movement, but with more inputs.
{
    public GameObject playerGroup;
    public GameObject steeringWheel; // this is used for a small animation

    Vector3 spawnPos;

    [Header("UI Elements")]
    public Image healthBar;
    public Image cannonCooldownBar;
    public TextMeshProUGUI LootText;

    public Camera thirdPersonCam, firstPersonCam;
    private bool isCamAxisInUse = false;
    CameraModes currentCamMode = CameraModes.thirdPerson;

    // Pause Screen Stuff
    public RawImage pauseBG;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        fireCD = 0f; // sets the fire cooldown to 0

        spawnPos = transform.position; // initializes the player's spawn position to their current location

        lootAmount = 0; // the player starts with no loot
        maxHP = 100; // the player starts with 100 max HP
        currentHP = maxHP; // the player starts with 100 current HP 

        // the game is paused when it starts for demo purposes
        isPaused = true;
        pauseBG.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        // the manual movement on the player is called before the base update so that the player goes where they want with no delay
        //movement controls
        transform.eulerAngles += new Vector3(0f, Input.GetAxisRaw("Horizontal"), 0f) * turnSpeed * Time.deltaTime; // standard A/D rotatation controls 
        steeringWheel.transform.localEulerAngles += new Vector3(0f, 0f, Input.GetAxisRaw("Horizontal")) * turnSpeed * Time.deltaTime; // steering wheel animation

        base.Update(); // this is just the basic boat movement from the parent class
        playerGroup.transform.position = transform.position; // ensure the player group (including the camera focus and wind arrow) is always in the same place as the boat

        // 2 different shooting controls: 1 for each cannon
        if(Input.GetAxisRaw("Fire1") > 0 && fireCD <= 0)
        {
            base.Shoot(0);
        }
        if (Input.GetAxisRaw("Fire2") > 0 && fireCD <= 0)
        {
            base.Shoot(1);
        }

        // Camera toggle controls
        if (Input.GetAxis("CamChange") != 0 && isCamAxisInUse == false)
        {
            if(currentCamMode == CameraModes.thirdPerson) // toggles third- to first-person or vice versa
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
            isCamAxisInUse = false; // this bool is used so that the camera isn't toggled every frame
        }

        //Debug.Log(fireCD);

        // UI UPDATE
        LootText.text = "Current Loot: " + lootAmount.ToString("00"); // displays the current amount of loot as a 2-digit string
        healthBar.fillAmount = currentHP / maxHP; // fills the health bar accordingly
        cannonCooldownBar.fillAmount = 1 - (fireCD / 5); // fills the cannon cooldown bar accordingly

        // determine which camera needs to be active
        // cameras are always activated first to ensure at least one is rendering at any given point
        switch(currentCamMode) // the relevant enum is at the bottom of this script
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

        if(Input.GetKeyDown(KeyCode.P))
        {
            TogglePause(); // pauses the game when P is pressed
        }

        if(currentHP <= 0 )
        {
            Die(); // kills the player when they run out of HP
        }
    }

    public override void Die() // overrides the basic Die() method so that the player object isn't destroyed
    {
        for(int i = 1; i <= lootAmount; i++) // drops all the player's loot
        {
            Instantiate(treasureChestPrefab);
        }
        transform.position = spawnPos; // relocates the player
        currentHP = maxHP; // resets HP values
        lootAmount = 0; // sets the player's loot back to 0
    }

    public void TogglePause() // pauses the game is it isn't, or unpauses if it is
    {
        if(isPaused == false)
        {
            pauseBG.gameObject.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            pauseBG.gameObject.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    enum CameraModes // small enum used to switch camera views
    {
        firstPerson,
        thirdPerson
    }
}
