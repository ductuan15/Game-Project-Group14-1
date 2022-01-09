using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenuController : MonoBehaviour
{
    public AudioMixer mixer;
    AudioSource sound;
    public AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeVolume(float index)
    {
        mixer.SetFloat("volume", index);
    }
    public void playClick()
    {
        sound.PlayOneShot(click);
    }

}
