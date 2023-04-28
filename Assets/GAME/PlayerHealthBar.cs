using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public Slider hpSlider;

    private void Awake()
    {
        hpSlider = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<Slider>();
    }

    void Start()
    {
        currentHP = maxHP;
        hpSlider.value = maxHP;
    }

    void Update()
    {
        SetCurrentHP();
        if (currentHP <= 0)
            Debug.Log("0");
    }
    public void SetCurrentHP()
    {
        hpSlider.value = currentHP;
    }
    public void ReceiveDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Death();
        }
    }
    void Death()
    {

        Debug.Log("Player Died");
    }
}
