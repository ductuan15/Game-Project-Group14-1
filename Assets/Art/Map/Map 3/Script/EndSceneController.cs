using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
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
    public void backtoMenu()
    {
        sound.PlayOneShot(click);
        SceneManager.LoadScene(0);
    }
}
