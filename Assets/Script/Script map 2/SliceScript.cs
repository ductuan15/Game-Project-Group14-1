using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        HeroKnight e = other.collider.GetComponent<HeroKnight>();
        if (e != null)
        {
            Debug.Log("Heeeeeeyyy");
            e.ChangeHealth(-200);
        }
        Destroy(gameObject);
    }
}
