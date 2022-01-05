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
    [Header("Skill1")]
    public Image skill2Image;
    public float countdown2 = 7f;
    bool isCountdown2 = false;

    //Skill 3
    [Header("Skill1")]
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

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Skill1();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            Skill2();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            Skill3();
        }
    }

    private void Skill1(){
        if(isCountdown1 == true){
            Debug.Log("Sword Art is countdown!");
        }
        else{
            isCountdown1 = true;
            skill1Image.fillAmount = 0;
        }
    }
    private void Skill2(){
        if(isCountdown2 == true){
            Debug.Log("Shield is countdown!");
        }
        else{
            isCountdown2 = true;
            skill2Image.fillAmount = 0;
        }
    }
    private void Skill3(){
        if(isCountdown3 == true){
            Debug.Log("Ultimate is countdown!");
        }
        else{
            isCountdown3 = true;
            skill3Image.fillAmount = 0;
        }
    }
}
