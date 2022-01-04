using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSize(float value){
        transform.GetChild(1).localScale = new Vector3(value, 1);
    }
}
