using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealthbar : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public Slider hpSlider;

    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();
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
    public void Death()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool("umieranie", true);
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            gameObject.GetComponent<EnemyHealthbar>().hpSlider.enabled = false;
            agent.enabled = false;
        }
        Destroy(gameObject, 5);
        Debug.Log("Died");
    }


}
