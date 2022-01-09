using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{
    private HeroKnight heroKnight;
    public Text textLevel;
    // Start is called before the first frame update
    void Start()
    {
        heroKnight = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        textLevel.text = "Level: " + heroKnight.heroLevel.ToString();
    }
}
