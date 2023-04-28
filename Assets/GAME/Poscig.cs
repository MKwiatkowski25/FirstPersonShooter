using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Poscig : MonoBehaviour
{
    bool alreadyAttacked = false;


    [SerializeField] private Transform pozycjaGracza;
    private NavMeshAgent mojAgent;
    private Collider[] znalezioneObiekty;
    private float promien = 20;
    private Animator mojAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mojAgent = GetComponent<NavMeshAgent>();
        mojAnimator = GetComponent<Animator>();
    }

    private void WyszukajCel()
    {
        mojAgent.SetDestination(transform.position);
        if (mojAnimator != null)
        {
            mojAnimator.SetBool("sciganie", false);
        }


        znalezioneObiekty = Physics.OverlapSphere(transform.position, promien);

        foreach (Collider obiekt in znalezioneObiekty)
        {
            if (obiekt.CompareTag("Gracz"))
            {
                mojAgent.SetDestination(obiekt.transform.position);
                if (mojAnimator != null)
                {
                    mojAnimator.SetBool("sciganie", true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        WyszukajCel();
        AtakPrzycisk();
    }

    void AtakPrzycisk()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (mojAnimator != null)
            {
                mojAnimator.SetTrigger("atak");
            }
        }
    }

    void Attack()
    {
        float distance = Vector3.Distance(pozycjaGracza.position, transform.position);
        if (distance < 2)
        {
            if (alreadyAttacked == false)
            {
                GameObject.FindGameObjectWithTag("Gracz").GetComponent<PlayerHealthBar>().ReceiveDamage(20);
                alreadyAttacked = true;
            }
            mojAnimator.SetTrigger("atak");

        }
        if (distance > 10)
        {
            alreadyAttacked = false;
        }
    }

}
