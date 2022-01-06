using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject optionMenu;
    // player for save
  

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void resume()
    {
        GameManager._instance.resume();
        this.gameObject.SetActive(false);
    }
    public void playGame()
    {
        GameManager._instance.checkNew = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void option()
    {
        optionMenu.SetActive(true);
       // Debug.Log("option");
    }
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void Pause()
    {
        GameManager._instance.pause();
    }
    public void saveGame()
    {
        GameManager._instance.saveGame();
    }
    public void loadGame()
    {
        GameManager._instance.checkNew = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager._instance.loadGame();


    }
}
