using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager
{
    public bool checkNew = true;
    private static GameManager Instance;
    public static GameManager _instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new GameManager();

            }
            return Instance;
        }
    }

    //Pause
    public void pause() 
    {
        Time.timeScale = 0;


    }
    public void resume()
    {
        Time.timeScale = 1;

    }
    public void saveGame()
    {
        Debug.Log("Save Game");
        string path = Path.Combine(Application.persistentDataPath, "player.hd");
        FileStream file = File.Create(path);
        int index = SceneManager.GetActiveScene().buildIndex;

        //Format to binary
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, index);
        file.Close();
        Debug.Log(path);
    }
    public void loadGame()
    {
        Debug.Log("loadGame");
        string path = Path.Combine(Application.persistentDataPath, "player.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            int data = (int)formatter.Deserialize(file);
            file.Close();

            SceneManager.LoadScene(data);
            Time.timeScale = 1;


        }

    }
    public void selectMap1()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void selectMap2()
    {
        SceneManager.LoadScene(2); 
        Time.timeScale = 1;
    }
    public void selectMap3()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

}

