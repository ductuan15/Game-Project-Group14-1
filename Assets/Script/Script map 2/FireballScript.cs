using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
     new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (transform.position.magnitude > 1000.0f)
        // {
        //     Destroy(gameObject);
        // }
    }
    public void Launch(Vector2 direction, float force)
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
