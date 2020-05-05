using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BoatMovement
{
    npcDetectionManager detectionManager;

    public GameObject player;
    public Transform rayOrigin;

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

        Debug.DrawRay(rayOrigin.position, Vector3.forward, Color.red);

        if (Vector3.Distance(transform.position, player.transform.position) < 75)
        {
            //turn towards the player
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        }

    }
}
