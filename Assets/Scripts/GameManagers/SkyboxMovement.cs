using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class SkyboxMovement : MonoBehaviour
{
    public Volume volume; // the volume used for the visual environment
    public float cloudMoveSpeed = 0.4f;

    public WindManager windManager; // this is used so that the storm bool can be accessed

    // Update is called once per frame
    void Update()
    {
        HDRISky sky;
        volume.profile.TryGet(out sky);
        sky.rotation.value += Time.deltaTime * cloudMoveSpeed; // essentially just changes the skybox object's y rotation by a small amount every frame, giving the illusion of moving clouds

        if(windManager.isStormHappening == true)
        {
            sky.exposure.value = -4; // reducing the exposure darkens the sky, making it seem like a storm is happening
        }
        else
        {
            sky.exposure.value = 1; // 1 exposure makes the sky brighter when it isn't a storm
        }
    }
}
