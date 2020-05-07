using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* This is the base class for all boat movement:
 * Using the wind angle on the WindManager GameObject,
 * boat velocities are determined and all boats are
 * translated based on their own velocity.
 */
public class BoatMovement : MonoBehaviour
{
    [Header("Wind Manager Variables")]
    float angleToWind; // prepares a variable to determine the boat's relative angle to the wind

    public GameObject windManager;
    public WindManager windManagerScript;

    [Header("Speed Variables")]
    [Range(1, 50)]
    [SerializeField]private float turnSpeed;
    public float TurnSpeed
    {
        get { return turnSpeed; }
        set { turnSpeed = value; }
    }


    private float sailSetting = 1f;
    public float SailSetting
    {
        get { return sailSetting; }
        set { sailSetting = value; }
    }

    [Header("Shooting Variables")]
    public Transform[] firingPoints = new Transform[2]; // an array of transforms is used to show how many possible firing points a boat has
    public ParticleSystem[] cannonParticles = new ParticleSystem[2];
    public GameObject cannonballPrefab;
    public float fireCD;

    [Header("Loot Variables")]
    public int lootAmount;
    public GameObject treasureChestPrefab;
    public GameObject flotsamPrefab;

    [Header("HP Variables")]
    public float currentHP;
    public float maxHP;

    public int currentLives;
    public int maxLives;

    // Start is called before the first frame update
    void Start()
    {
        windManagerScript = windManager.GetComponent<WindManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        angleToWind = (transform.eulerAngles.y - windManager.transform.eulerAngles.y) * (Mathf.PI / 180); // finds the angle between the boat and the wind and converts to radians
        //Debug.Log(1 + Mathf.Cos(angleToWind));

        transform.Translate(Vector3.forward * (sailSetting * windManagerScript.WindSpeed * (1 + (Mathf.Cos(angleToWind))) * Time.deltaTime)); // constantly moves the player boat forwards relative to their angle to the wind
    }

    public void Shoot(int firingPointIndex) // takes in an int which corresponds to an element in that boats Transform[] of firing points
    {
        Instantiate(cannonballPrefab, firingPoints[firingPointIndex].position, firingPoints[firingPointIndex].rotation); // spawns a cannonball at the correct location and rotation
        cannonParticles[firingPointIndex].Play(); //plays the cannon fire particles at the same firing point

        fireCD = 5f; // resets the fire cooldown
        InvokeRepeating("ShootingCooldown", 0f, 0.1f); // starts the cooldown method
    }

    public void ShootingCooldown() // this ensures the boat cant just rapid-fire
    {
        if(fireCD > 0) // reduces the cooldown if it's greater than 0
        {
            fireCD -= 0.1f;
        }
        else { CancelInvoke(); } // if the cooldown is finished all calls to this method are stopped
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
    }


    public virtual void Die() // this method is virtual so it can be overridden in the player script - all boats other than the player will instantly be destroyed, the player will just reset its location
    {
        for(int i = 1; i <= lootAmount; i++) // drops as much loot as that boat was carrying
        {
            Instantiate(treasureChestPrefab, transform.position, transform.rotation);
        }
        for(int i = 0; i < Random.Range(1,4); i++) // drops a random amount of flotsam
        {
            Instantiate(flotsamPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Chest") // all boats are able to pick up treasure, so all boats can drop treasure
        {
            lootAmount++;
            Destroy(coll.gameObject);
        }
    }
}
