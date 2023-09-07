using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAgain : MonoBehaviour, IPointerClickHandler
{
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("PlayAgain Click");
        Setup();
    }

    public LineGenerator lineGenerator;
    public GameOverScreen gameOverScreen;
    public PictureGenerator PictureGenerator;

    // When clicking this button. Close the game over screen and start a new game.
    public void Setup()
    {
        PictureGenerator.Reset();
        gameOverScreen.SetActive(false);
        lineGenerator.Reset();
    }

}
