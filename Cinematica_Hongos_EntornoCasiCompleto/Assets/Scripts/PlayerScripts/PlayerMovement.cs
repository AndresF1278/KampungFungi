using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{ 
    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    public bool muerto = false;
   public string idScena;
    

    [Space]
    [Header("Variables de movimiento")]
    private float _velocidad;
    public float Velocidad { get => _velocidad; set => _velocidad = value; }

    [Space]
    [Header("Variables de Compra Item")]
    [SerializeField] private bool puedeComprar;

    [Space]
    [Header("Variables de animacion")]
    private Animator animatorPlayer;
    [SerializeField] bool IsMoving;

    public bool estaEnDialogo;

    void Start()
    {
        ObtenerVariablesIniciales();
        estaEnDialogo = true;

    }
    void ObtenerVariablesIniciales()
    {
        rb = this.GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
    }

   

    void FixedUpdate()
    {
        if (muerto)
        {
            rb.velocity = Vector3.zero;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0 )
        {
            Movement();
            IsMoving = true;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            IsMoving = false;
        }

        animatorPlayer.SetBool("IsMoving", IsMoving);
        
        if(GetComponent<HealthPlayer>().currentHealth == 0)
        {
            animatorPlayer.SetBool("IsMoving", false);
        }

    }

    void Movement()
    {
        if (!estaEnDialogo)
        {
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            float verticalVelocity = rb.velocity.y;
            rb.velocity = new Vector3(horizontal * _velocidad * Time.deltaTime, Mathf.Clamp(rb.velocity.y, -10, 1), vertical * _velocidad * Time.deltaTime);
            if (direction != Vector3.zero)
            {
                Rotate(direction);
            }
        }
        

    }

    void Rotate(Vector3 direction)
    {
        // Rota el personaje hacia la dirección deseada, limitando su rotación a mirar hacia adelante, atrás, izquierda y derecha
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        rb.rotation = targetRotation;
    }


}
