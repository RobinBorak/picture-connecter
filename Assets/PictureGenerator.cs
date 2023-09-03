using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureGenerator : MonoBehaviour
{
    public GameObject picturePrefab;
    // Start is called before the first frame update
    void Start()
    {

        InstantiatePicture("mouse", "left", -5);
        InstantiatePicture("cat", "left", 1);
        InstantiatePicture("saw", "left", -2);

        InstantiatePicture("hat", "right", 1);
        InstantiatePicture("train", "right", -2);
        InstantiatePicture("house", "right", -5);


    }

    void InstantiatePicture(string asset, string side, int yPos)
    {
        // Instantiate picture on left or right side
        GameObject picture = Instantiate(picturePrefab);
        picture.name = asset;

        int xPos = side == "left" ? -3 : 15;
        picture.transform.position = new Vector3(xPos, yPos, 0);

        // Load sprite from Resources/Sprites folder and set it as the picture's sprite
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + asset);
        SpriteRenderer renderer = picture.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }
}
