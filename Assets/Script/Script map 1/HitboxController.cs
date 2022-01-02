using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        HeroKnight controller = other.GetComponent<HeroKnight>();

        if (controller != null)
        {
            controller.ChangeHealth(Damage);
        }
    }
}
