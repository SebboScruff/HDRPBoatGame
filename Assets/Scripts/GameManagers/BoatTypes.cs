using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boats // simple enum for the purposes of collision detections, causing different behaviours based on which boat collides
{
    PLAYER,
    ENEMY_1,
    MERCHANT_1
}

public class BoatTypes : MonoBehaviour
{
    public Boats boatType; // initialize the enum in a class
}
