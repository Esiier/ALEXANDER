using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //referencia al CharacterController
    public CharacterController controller;
    //speed de movimiento normal
    public float speedWalk = 14f;
    //speed de movimiento corriendo
    public float speedRun = 24f;
    //guardamos la speed aqui
    private float actualSpeed;
    //fuerza de la gravedad constante
    public float gravity = -19.62f;
    //altura del salto
    public float jumpHeight = 3f;
    //altura del salto en la plataforma
    public float jumpPlatformHeight = 10f;

    //referencia al gameobject para chequear si estamos tocando suelo, la distancia entre el suelo y el player y la LayerMask
    public Transform GroundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    public LayerMask jumpPlatformMask;


    Vector3 velocity;
    //bool para saber si estamos tocando el suelo o no
    bool isGrounded;
    //bool para saber si estamos tocando la plataforma de salto
    bool isInJumpBoost;
    bool iHave2jump;

    void Start()
    {
        
    }

    void Update()
    {
        //cramos una pequeña sphere debajo de nuestros pies para saber si estamos tocando el suelo o no, si lo tocamos se pone a true si no se pone a false
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            iHave2jump = true;
        }

        isInJumpBoost = Physics.CheckSphere(GroundCheck.position, groundDistance, jumpPlatformMask);
        if(isInJumpBoost && velocity.y <0)
        {
            velocity.y = Mathf.Sqrt(jumpPlatformHeight * -2f * gravity);
        }

        //esto pilla referencia a si nos estamos moviendo hacia adelante o atras
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //se encarga de mirar hacia donde estamos moviendonos
        Vector3 move = transform.right * x + transform.forward * z;
        //se mueve depende de las teclas que pillemos + la speed
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            actualSpeed = speedRun;
        }
        else
        {
            actualSpeed = speedWalk;
        }
        controller.Move(move * actualSpeed * Time.deltaTime);

        //salto
        if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //doble salto
        if (Input.GetButtonDown("Jump") && !isGrounded && iHave2jump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            iHave2jump = false;
        }

        //cogemos la gravedad y se la añadimos a la Y del player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
