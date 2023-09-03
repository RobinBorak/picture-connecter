using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{

    public LineGenerator lineGenerator;
    public GameOverScreen gameOverScreen;
    public PictureGenerator PictureGenerator;

    // When clicking this button. Close the game over screen and start a new game.
    public void Setup()
    {
        gameOverScreen.SetActive(false);
        lineGenerator.Reset();
        PictureGenerator.Reset();
    }

    void Start()
    {
        Debug.Log("PlayAgain Start");
        //find button component
        GameObject button = GameObject.Find("PlayAgainButton");
        //add listener to button
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Setup);

    }

}
