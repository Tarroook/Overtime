using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{

    Health hpScript;
    public GameObject hpBar;
    Slider slider;
    Animator anim;
    PlayerDamage PlayerD;


    private void OnEnable()
    {
        hpScript = gameObject.GetComponent<Health>();
        hpScript.onDamage += startAnimation;
    }
    private void OnDisable()
    {
        hpScript.onDamage -= startAnimation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        slider = hpBar.GetComponent<Slider>();
        anim = hpBar.transform.Find("box").GetComponent<Animator>();
        setMaxHealth(100);
        PlayerD = GetComponent<PlayerDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = (1 / (float)PlayerD.iframes);
        slider.value = hpScript.health; 
    }
    void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health; 
    }

    void startAnimation()
    {
        anim.SetTrigger("damage");

    }
}


