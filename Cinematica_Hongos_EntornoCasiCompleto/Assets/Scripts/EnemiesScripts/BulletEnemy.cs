using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private Vector3 direccionPlayer;
    public float Speed { get => speed; set => speed = value; }
    public Vector3 DireccionPlayer { get => direccionPlayer; set => direccionPlayer = value; }


    



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Shot(DireccionPlayer);
    }

    public void Shot(Vector3 direccion)
    {
        direccion = DireccionPlayer;
        rb.velocity = speed * DireccionPlayer;    

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        AudioManager.instance.playSfx("BalaGolpea");
        gameObject.SetActive(false);
        
    }

    private void OnDisable()
    {
        AudioManager.instance.playSfx("BalaSeDestruye");
    }

}
