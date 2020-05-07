using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BoatMovement
{
    npcDetectionManager detectionManager; // ***UNUSED***

    public GameObject player;

    public Transform rayOrigin;
    [Range(30, 80)]
    public float detectionRayLength = 50f;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        detectionManager = GetComponentInChildren<npcDetectionManager>();

        lootAmount = 0; // enemies start with no loot
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); // calls the basic update functions from the boat movement class

        //Debug.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRayLength, Color.red);
        if(Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, detectionRayLength) == true) // NPC boats use a raycast to detect land and the player
        {
            //Debug.Log("Enemy Ray Hit Something");

            if(hit.collider.tag == "Player" && fireCD == 0) // if the ray hits the player, the boat will fire a cannonball
            {
                Shoot(0);
            }
            else if(hit.collider.tag == "Terrain") // if the ray hits the terrain, the boat will turn around 180 degrees, setting up a patrolling behaviour
            {
                transform.rotation = Quaternion.LookRotation(transform.position - hit.point);
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 75) // if the player is within a certain distance to the enemy, the enemy will turn towards it 
        {
            //turn towards the player
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        }

        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f); // resets the x and z rotations just in case things go wrong
    }
}
