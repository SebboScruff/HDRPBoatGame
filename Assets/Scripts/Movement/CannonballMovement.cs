//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballMovement : MonoBehaviour
{
    [Range(25,100)]
    public float moveSpeed = 50.0f;

    int duration = 3; // limits the amount of time a specific ball can exist so they don't fly indefinitely

    void Awake()
    {
        InvokeRepeating("DestroyTimer", 0f, 1f); // starts the ball duration timer immediately after being instantiated
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); // constantly moves the ball forwards
        if(duration <= 0) // destroys the ball after it has existed for enough time
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        BoatTypes boatColl = coll.gameObject.GetComponent<BoatTypes>(); // gets the type of boat from the boatType enum
        BoatMovement boatScript = coll.gameObject.GetComponent<BoatMovement>();

        if(boatColl != null && boatScript != null) // only called if the boatColl is actually present
        {
            switch(boatColl.boatType) // cycles through each element in the boat type enum
            {
                case Boats.ENEMY_1:
                    boatScript.Die(); // enemies are instantly destroyed
                    break;
                case Boats.MERCHANT_1:
                    boatScript.Die(); // merchants are instantly destroyed
                    break;
                case Boats.PLAYER:
                    boatScript.TakeDamage(20); // the player takes 20 damage
                    break;
                default:
                    break;
            }
        }
        Destroy(gameObject); // destroys the cannonball regardless
    }

    void DestroyTimer()
    {
        duration--; // removes 1 second from the duration every second
    }
}
