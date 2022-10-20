//Gabriela Rosas Castillo
//17-10-2022
//GDGS2102
//Creación de videojuegos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timerText;

    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton; 

    public List<GameObject> targetPrefabs;

    public float currentTime;
    public float startingTime=60f;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; // Valor de X
    private float minValueY = -3.75f; // Valor de Y
    
    // Inicie el juego, elimine la pantalla de título, reinicie la puntuación y ajuste la tasa de generación según el botón de dificultad en el que se hizo clic.
    public void StartGame(int difficulty)
    {
        currentTime = startingTime;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text="Time: " + (Mathf.Round(currentTime));

    }

    //Mientras el juego está activo genera un objetivo aleatorio
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            if (currentTime<= 0)
            {
                GameOver();
            }

            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Genere una posición de generación aleatoria basada en un índice aleatorio de 0 a 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Genera un índice cuadrado aleatorio de 0 a 3, que determina en qué cuadrado aparecerá el objetivo
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Actualizar puntaje con valor del objetivo en el que se hizo clic
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "score: "+ score;
    }

    // Detener el juego, mostrar el juego sobre el texto y el botón de reinicio
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Reinicia el juego recargando la escena
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
