using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenuController : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeVolume(float index)
    {
        mixer.SetFloat("volume", index);
    }

}
