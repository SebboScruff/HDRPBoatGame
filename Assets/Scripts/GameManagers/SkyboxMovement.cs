using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class SkyboxMovement : MonoBehaviour
{
    public Volume volume;
    public float cloudMoveSpeed = 0.4f;

    //public WindManager windManager;

    // Update is called once per frame
    void Update()
    {
        HDRISky sky;
        volume.profile.TryGet(out sky);
        sky.rotation.value += Time.deltaTime * cloudMoveSpeed;
    }
}
