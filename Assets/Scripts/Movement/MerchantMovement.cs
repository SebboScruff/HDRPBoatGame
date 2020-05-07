using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is very similar to the enemy script,
 * except the merchants flee the player rather than
 * chase, and cannot fire
 * 
 */

public class MerchantMovement : BoatMovement
{
    public GameObject player;

    public Transform rayOrigin;
    [Range(30, 80)]
    public float detectionRayLength = 50f;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        lootAmount = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        //Debug.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRayLength, Color.green);
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, detectionRayLength) == true)
        {
            //Debug.Log("Merchant Ray Hit Something");

            if (hit.collider.tag == "Terrain") // if the ray hits the terrain, the merchant will turn around 180 degrees
            {
                transform.rotation = Quaternion.LookRotation(transform.position - hit.point);
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 75) // of the player is within a certain distance to the merchant ship, it will flee
        { 
        
            transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        }

        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f); // reset x and z rotations just in case
    }
}
