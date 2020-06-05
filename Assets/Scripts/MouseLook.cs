using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //velocidad del movimiento de la camara
    public float mouseSensitivity = 100f;
    //le pasamos la referencia del player
    public Transform playerBody;
    //
    float xRotation = 0f;

    void Start()
    {
        //para que el mouse no se mueva por la pantalla lo dejamos lokeado en un punto y asi no puede moverse y hacer clics donde no debe
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //chequeamos cuando movemos el raton y le damos la velocidad
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //esto es para si queremos la camara invertida o no cambiamos el - por un + y va al reves
        xRotation -= mouseY;
        //para que no podamos rotar la camara de mas y se ponga del reves
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //cuando movemos el raton tmb movemos al player
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
