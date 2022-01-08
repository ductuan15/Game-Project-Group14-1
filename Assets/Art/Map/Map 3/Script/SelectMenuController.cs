using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectMap1()
    {
        GameManager._instance.selectMap1();
    }
    public void selectMap2()
    {
        GameManager._instance.selectMap2();
    }
    public void selectMap3()
    {
        GameManager._instance.selectMap3();
    }
}
