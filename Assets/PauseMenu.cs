using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    
    public AudioMixer generalVolume, spatialVolume;

    public void SetGeneralVolume(float volume) 
    {
        generalVolume.SetFloat("GeneralVolume", volume);
    }

    public void SetSpatialVolume(float volume) 
    {
        spatialVolume.SetFloat("SpatialVolume", volume);
    }

}
