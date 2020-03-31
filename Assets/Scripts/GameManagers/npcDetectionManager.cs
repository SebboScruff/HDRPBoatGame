using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class npcDetectionManager : MonoBehaviour
{
    private GameObject currentTarget;
    public GameObject CurrentTarget
    {
        get { return currentTarget; }
        set { currentTarget = value; }
    }

    //LAND DETECTION
    public Transform landDetector;
    [Range(25,250)]
    public float landDetectorDistance = 50.0f;
    RaycastHit hit;


    //BOAT DETECTION
    List<Collider> detectedBoats = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(landDetector.position, landDetector.TransformDirection(Vector3.forward), landDetectorDistance);

        Debug.DrawRay(landDetector.position, landDetector.TransformDirection(Vector3.forward) * 250, Color.red);
        Debug.Log(detectedBoats.Count);

        UpdateListPriority();

        //currentTarget = detectedBoats[1].gameObject;
    }

    void OnCollisionEnter(Collision coll)
    {
        currentTarget = coll.gameObject;
        if(detectedBoats.Contains(coll.collider) == false && coll.gameObject.name != "EnemyShip") 
        {
            detectedBoats.Add(coll.collider);
            Debug.Log("new boat detected");
        }
    }

    void OnCollisionExit(Collision coll)
    {
        // If the detected exit is in the list, it will be removed
        if(detectedBoats.Contains(coll.collider) == true)
        {
            detectedBoats.Remove(coll.collider);
            Debug.Log("Boat just left");
        }
    }

    void UpdateListPriority()
    {
        //update the priority of each boat in the list
        for(int i = 0; i < detectedBoats.Count; i++)
        {
            //TODO sort the elements in the list in descending order of current loot * 1/distanceToBoat
            BoatMovement boatMovement = detectedBoats[i].GetComponent<BoatMovement>();
            float priority = boatMovement.lootAmount * (1 / (Vector3.Distance(transform.position, detectedBoats[i].transform.position))); //current loot * reciprocal of distance to NPC


        }

        //var distance = detectedBoats.Select(x => (x.transform.position - gameObject.transform.position).magnitude); //TODO read into this and learn what it does
    }
}
