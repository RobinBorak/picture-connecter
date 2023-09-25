using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour, IPointerClickHandler
{
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("LevelSelect Click");
        //Get the level from the name of the button.
        int level = int.Parse(gameObject.name);
        Setup(level);
    }

    public LineGenerator lineGenerator;
    public GameOverScreen gameOverScreen;
    public PictureGenerator PictureGenerator;

    // When clicking this button. Close the game over screen and start a new game.
    public void Setup(int level)
    {
        PictureGenerator.SetLevel(level);
        PictureGenerator.Reset();
        gameOverScreen.SetActive(false);
        lineGenerator.Reset();
    }

}
