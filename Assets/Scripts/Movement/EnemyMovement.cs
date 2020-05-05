﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BoatMovement
{
    npcDetectionManager detectionManager;

    public GameObject player;


    public Transform rayOrigin;
    [Range(30, 80)]
    public float detectionRayLength = 50f;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        detectionManager = GetComponentInChildren<npcDetectionManager>();

        lootAmount = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRayLength, Color.red);
        if(Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, detectionRayLength))
        {
            if(hit.collider.tag == "Player")
            {
                Shoot(0);
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 75)
        {
            //turn towards the player
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        }
    }
}
