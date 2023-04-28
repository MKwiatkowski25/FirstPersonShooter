using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armata : MonoBehaviour
{
    [SerializeField] private Transform miejsceStrzalu;
    [SerializeField] private float dist=1;
    [SerializeField] private GameObject kula;
    [SerializeField] private Transform miejsceKuli;
    private float Sila = 10;
    private float nastWystrzal=0;
    void Wystrzal()
    {
        if (Vector3.Distance(miejsceStrzalu.position, transform.position) < dist && Input.GetKeyDown(KeyCode.E) && Time.time > nastWystrzal)
        {
            print("Wystrzal");
            var go = Instantiate(kula, miejsceKuli.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(miejsceKuli.forward * Sila, ForceMode.Impulse);
            nastWystrzal = Time.time + 5;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wystrzal();
    }
}
