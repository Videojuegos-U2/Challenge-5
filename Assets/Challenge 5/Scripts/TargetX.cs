//Gabriela Rosas Castillo
//17-10-2022
//GDGS2102
//Creación de videojuegos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;

    public float timeOnScreen = 1.0f;

    private float minValueX = -3.75f; // Valor de X
    private float minValueY = -3.75f; // Valor de Y
    private float spaceBetweenSquares = 2.5f; // La distancia entre los centros de los cuadrados en el tablero de juego
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        transform.position = RandomSpawnPosition(); 
        StartCoroutine(RemoveObjectRoutine()); // Iniciar el temporizador antes de que el objetivo abandone la pantalla

    }

    // Cuando se hace clic en el objetivo, destrúyelo, actualiza la puntuación y genera una explosión
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            Destroy(gameObject);
            gameManagerX.UpdateScore(pointValue);
            Explode();
        }
               
    }

    // Posición de generación aleatoria basada en un índice aleatorio de 0 a 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Genera un índice cuadrado aleatorio de 0 a 3, que determina en qué cuadrado aparecerá el objetivo
    int RandomSquareIndex ()
    {
        return Random.Range(0, 4);
    }


    // Si el objetivo que NO es el objeto defectuoso choca con el sensor, se activa el fin del juego.
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
        {
            gameManagerX.GameOver();
        } 

    }

    // Mostrar partículas de explosión en la posición del objeto
    void Explode ()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }

    // Después de un retraso, mueve el objeto detrás del fondo para que choque con el objeto Sensor
    IEnumerator RemoveObjectRoutine ()
    {
        yield return new WaitForSeconds(timeOnScreen);
        if (gameManagerX.isGameActive)
        {
            transform.Translate(Vector3.forward * 5, Space.World);
        }

    }

}
