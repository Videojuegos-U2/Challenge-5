//Gabriela Rosas Castillo
//17-10-2022
//GDGS2102
//Creación de videojuegos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectX : MonoBehaviour
{
    void Start()
    {
        //Destruir los objetos en 2 segundos
        Destroy(gameObject, 2); 
    }


}
