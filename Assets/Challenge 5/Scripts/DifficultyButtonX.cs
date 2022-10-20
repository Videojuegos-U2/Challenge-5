//Gabriela Rosas Castillo
//17-10-2022
//GDGS2102
//Creación de videojuegos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour
{
    private Button button;
    private GameManagerX gameManagerX;
    public int difficulty;

    // Se llama al inicio antes de la primera actualización del cuadro.
    void Start()
    {
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    
    // Cuando se hace clic en un botón, llama al método StartGame()
    //y pasarle el valor de dificultad (1, 2, 3) desde el boton
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked");
        gameManagerX.StartGame(difficulty);
    }



}
