using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rb;
    private float tiempoVida = 5;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        tiempoVida -= Time.deltaTime;

        if (tiempoVida <= 0)
        {
            gameObject.SetActive(false);
        }

        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnDisable()
    {
        tiempoVida = 4;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //AudioManager.instance.playSfx("BalaGolpea");
        gameObject.SetActive (false);
    }
}
