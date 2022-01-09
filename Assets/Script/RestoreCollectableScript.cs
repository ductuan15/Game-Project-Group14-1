using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreCollectableScript : MonoBehaviour
{
    private int restoreBlood = 300;
    private int restoreMana = 150;

    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        HeroKnight e = other.GetComponent<HeroKnight>();
        if (e != null)
        {
            e.ChangeHealth(restoreBlood);
            e.ChangeMana(restoreMana);
            Destroy(gameObject);
        }
    }
}
