using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject optionMenu;
    AudioSource sound;
    public AudioClip click;
    // player for save
  

    // Start is called before the first frame update

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void resume()
    {
        this.playClick();
        GameManager._instance.resume();
        this.gameObject.SetActive(false);
    }
  /*  public void playGame()
    {
        GameManager._instance.checkNew = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    public void option()
    {

        

        optionMenu.SetActive(true);
       // Debug.Log("option");
    }
    public void quitGame()
    {

        this.playClick();

        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void Pause()
    {
        this.playClick();

        GameManager._instance.pause();
    }
    public void saveGame()
    {

        this.playClick();

        GameManager._instance.saveGame();
    }
    public void loadGame()
    {
        this.playClick();


        GameManager._instance.checkNew = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager._instance.loadGame();


    }
    public void backtoMenu()
    {
        this.playClick();

        SceneManager.LoadScene(0);
    }
    public void playClick()
    {
        sound.PlayOneShot(click);
    }
}
