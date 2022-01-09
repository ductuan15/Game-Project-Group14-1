using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySystem : MonoBehaviour
{
    //Skill 1
    [Header("Skill1")]
    public Image skill1Image;
    public float countdown1 = 5f;
    bool isCountdown1 = false;

    //Skill 2
    [Header("Skill2")]
    public Image skill2Image;
    public float countdown2 = 20.0f;
    bool isCountdown2 = false;

    //Skill 3
    [Header("Skill3")]
    public Image skill3Image;
    public float countdown3 = 9f;
    bool isCountdown3 = false;

    // Start is called before the first frame update
    void Start()
    {
        skill1Image.fillAmount = 1;
        skill2Image.fillAmount = 1;
        skill3Image.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCountdown1 == true){
            skill1Image.fillAmount += 1 / countdown1 * Time.deltaTime;
        }
        if(skill1Image.fillAmount  == 1){
            isCountdown1 = false;
        }

        if(isCountdown2 == true){
            skill2Image.fillAmount += 1 / countdown2 * Time.deltaTime;
        }
        if(skill2Image.fillAmount  == 1){
            isCountdown2 = false;
        }

        if(isCountdown3 == true){
            skill3Image.fillAmount += 1 / countdown3 * Time.deltaTime;
        }
        if(skill3Image.fillAmount  == 1){
            isCountdown3 = false;
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            Skill1();
        }
        if(Input.GetKeyDown(KeyCode.W)){
            Skill2();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            Skill3();
        }
    }

    public void Skill1(){
        if(isCountdown1 == true){
            return;
        }
        else{
            isCountdown1 = true;
            skill1Image.fillAmount = 0;
        }
    }
    public void Skill2(){
        if(isCountdown2 == true){
            return;
        }
        else{
            isCountdown2 = true;
            skill2Image.fillAmount = 0;
        }
    }
    public void Skill3(){
        if(isCountdown3 == true){
            return;
        }
        else{
            isCountdown3 = true;
            skill3Image.fillAmount = 0;
        }
    }
}
