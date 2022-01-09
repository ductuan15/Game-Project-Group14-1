using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashingScript : MonoBehaviour
{
    private HeroKnight heroKnight;
    // Start is called before the first frame update
    void Start()
    {
        heroKnight = GameObject.Find("HeroKnight").GetComponent<HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        MonsterController e = other.collider.GetComponent<MonsterController>();
        if (e != null)
        {
            e.ChangeHealth(-heroKnight.heroDamage);
        }
        Destroy(gameObject);
    }
}
