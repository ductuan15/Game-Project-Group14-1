using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuController : MonoBehaviour
{
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
    public void selectMap1()
    {
        sound.PlayOneShot(click);
        GameManager._instance.selectMap1();
    }
    public void selectMap2()
    {
        sound.PlayOneShot(click);
        GameManager._instance.selectMap2();
    }
    public void selectMap3()
    {
        sound.PlayOneShot(click);
        GameManager._instance.selectMap3();
    }
    public void playClick()
    {
        sound.PlayOneShot(click);

    }
}
