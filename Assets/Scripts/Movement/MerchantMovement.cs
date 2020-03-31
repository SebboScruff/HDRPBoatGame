using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantMovement : BoatMovement
{
    public GameObject player;

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
        if(Vector3.Distance(transform.position, player.transform.position) < 75)
        { 
        
            transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        }

    }
}
