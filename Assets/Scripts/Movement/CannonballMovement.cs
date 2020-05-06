//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballMovement : MonoBehaviour
{
    [Range(25,100)]
    public float moveSpeed = 50.0f;

    int duration = 3;

    void Awake()
    {
        InvokeRepeating("DestroyTimer", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if(duration <= 0)
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
            switch(boatColl.boatType)
            {
                case Boats.ENEMY_1:
                    boatScript.Die();
                    break;
                case Boats.MERCHANT_1:
                    boatScript.Die();
                    break;
                case Boats.PLAYER:
                    boatScript.TakeDamage(20);
                    break;
                default:
                    break;
            }
        }
        Destroy(gameObject); // destroys the cannonball regardless
    }

    void DestroyTimer()
    {
        duration--;
    }
}
