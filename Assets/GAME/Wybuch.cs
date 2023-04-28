using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wybuch : MonoBehaviour
{
    [Range(0,10)][SerializeField] private float promien = 5;
    [Range(0,30)][SerializeField] private float sila = 20;
    [Space(10)]
    [SerializeField] private LayerMask warstwyWybuchu;
    [SerializeField] private GameObject efektWybuchu;
    private Collider[] trafioneObiekty;
    private void OnCollisionEnter(Collision col)
    {
        Vector3 punktWybuchu = col.contacts[0].point;

        var go = Instantiate(efektWybuchu, punktWybuchu, Quaternion.identity);
        go.transform.rotation = Quaternion.LookRotation(col.contacts[0].normal.normalized);

        WykonajWybuch(punktWybuchu);
        Destroy(gameObject);
        Destroy(go, 2f);
    }

    private void WykonajWybuch(Vector3 punktWybuchu)
    {
        trafioneObiekty = Physics.OverlapSphere(punktWybuchu, promien, warstwyWybuchu);

        foreach (Collider hitCol in trafioneObiekty)
        {

            if (hitCol.CompareTag("Zombie"))
            {
                hitCol.GetComponentInParent<EnemyHealthbar>().hpSlider.enabled = false;
               
            }

            NavMeshAgent agent = hitCol.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                hitCol.GetComponent<EnemyHealthbar>().hpSlider.enabled = false;
                agent.enabled = false;
            }

            Animator animator = hitCol.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("umieranie", true);
                Destroy(hitCol.gameObject, 5);
            }

            Rigidbody rid = hitCol.GetComponent<Rigidbody>();
            if (rid != null)
            {
                rid.isKinematic = false;
            }

            if (hitCol.GetComponent<Rigidbody>() != null)
            {
                hitCol.GetComponent<Rigidbody>().AddExplosionForce(sila, punktWybuchu, promien, 1, ForceMode.Impulse);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
