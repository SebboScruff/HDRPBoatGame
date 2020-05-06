using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRayLength, Color.green);
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, detectionRayLength) == true)
        {
            Debug.Log("Enemy Ray Hit Something");

            if (hit.collider.tag == "Terrain")
            {
                transform.rotation = Quaternion.LookRotation(transform.position - hit.point);
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 75)
        { 
        
            transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        }

        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
}
