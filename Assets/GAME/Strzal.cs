using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strzal : MonoBehaviour
{
    private float odstep = 0.5f;
    private float nastStrzal = 0;
    private RaycastHit cel;
    Animator anim;

    [Header("Zmienne strza³u")]
    [SerializeField] private Transform kamera;
    [SerializeField] private GameObject efektStrzalu;
    [SerializeField] private GameObject karabin;
    [SerializeField] private AudioClip dzwiekStrzalu;
    [Header("Zmienne granatu")]
    [SerializeField] private GameObject granatPref;
    [SerializeField] private Transform miejsceGranatu;
    [Space(10)]
    [Range(0,25)][SerializeField] private float silaRzutu = 10;
    private AudioSource audio;
    private float nastGranat = 0;
    void StrzalRaycast()
    {
        if (Physics.Raycast(kamera.position, kamera.forward, out cel))
        {
            Debug.DrawRay(kamera.position, kamera.forward * 100f, Color.blue, 10f);
            var go = Instantiate(efektStrzalu, cel.point, Quaternion.identity);
            go.transform.rotation = Quaternion.LookRotation(cel.normal.normalized);

            print(cel.transform.name);

            cel.collider.GetComponentInParent<EnemyHealthbar>()?.ReceiveDamage(10);
            
        }
    }
    private void SprawdzStrzal()
    {
        if (Input.GetButton("Fire1") && Time.time >= nastStrzal)
        {
            anim.SetTrigger("strzal");
            print("Strzelam!");
            nastStrzal = Time.time + odstep;
            audio.PlayOneShot(dzwiekStrzalu);
            StrzalRaycast();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = karabin.GetComponent<Animator>();
    }

    void RzutGranatem()
    {
        var go = Instantiate(granatPref, miejsceGranatu.position, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(miejsceGranatu.forward * silaRzutu, ForceMode.Impulse);
    }

    void SprawdzRzut()
    {
        if (Input.GetKeyDown(KeyCode.G) && Time.time >= nastGranat)
        {
            print("Rzucam!");
            nastGranat = Time.time + 2;
            RzutGranatem();
        }
    }

    void SprawdzAnimacje()
    {
        Animator anim = karabin.GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("widok");
            nastStrzal = Time.time + 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SprawdzAnimacje();
        SprawdzStrzal();
        SprawdzRzut();
    }
}
