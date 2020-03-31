using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BoatMovement
{
    npcDetectionManager detectionManager;

    public GameObject player;

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

        if (Vector3.Distance(transform.position, player.transform.position) < 75)
        {
            //turn towards the player
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        }
    }
}
