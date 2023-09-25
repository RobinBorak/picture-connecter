using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureGenerator : MonoBehaviour
{

  public class Picture
  {
    public string name;
    public string counterName;
    public bool successful = false;

    public BoxCollider2D collider;
    public BoxCollider2D counterCollider;

    public Picture(string name, string counterName)
    {
      this.name = name;
      this.counterName = counterName;
    }

    public void SetSuccessful(bool successful)
    {
      this.successful = successful;
    }

    public void SetCollider(BoxCollider2D collider)
    {
      this.collider = collider;
    }

    public void SetCounterCollider(BoxCollider2D counterCollider)
    {
      this.counterCollider = counterCollider;
    }
  }

  public GameObject picturePrefab;
  public List<Picture> pictures = new List<Picture>();
  List<int> leftYPositions = new List<int>();
  List<int> rightYPositions = new List<int>();
  List<int> tmpLeftYPositions = new List<int>();
  List<int> tmpRightYPositions = new List<int>();


  public int level = 1;
  private int MAX_LEVEL = 3;

  void Level1()
  {
    pictures.Add(new Picture("cat", "hat"));
  }

  void Level2()
  {
    pictures.Add(new Picture("cat", "hat"));
    pictures.Add(new Picture("mouse", "house"));
  }


  void Level3()
  {
    pictures.Add(new Picture("cat", "hat"));
    pictures.Add(new Picture("mouse", "house"));
    pictures.Add(new Picture("saw", "train"));
  }


  void Init()
  {

    pictures.Clear();

    switch (level)
    {
      case 1:
        Level1();
        break;
      case 2:
        Level2();
        break;
      case 3:
        Level3();
        break;
      default:
        Level1();
        break;
    }

    leftYPositions.Clear();
    leftYPositions.Add(1);
    leftYPositions.Add(-2);
    leftYPositions.Add(-5);

    rightYPositions.Clear();
    rightYPositions.Add(1);
    rightYPositions.Add(-2);
    rightYPositions.Add(-5);

    InstantiatePictures();
  }

  public void Reset()
  {
    DestroyPictures();
    Init();
  }

  void DestroyPictures()
  {
    foreach (Picture picture in pictures)
    {
      Destroy(GameObject.Find(picture.name));
      Destroy(GameObject.Find(picture.counterName));
    }
  }

  void InstantiatePictures()
  {
    tmpLeftYPositions.Clear();
    tmpRightYPositions.Clear();

    tmpLeftYPositions.AddRange(leftYPositions);
    tmpRightYPositions.AddRange(rightYPositions);
    foreach (Picture picture in pictures)
    {
      string side = Random.Range(0, 2) == 0 ? "left" : "right";
      int randomLeftYPosition = Random.Range(0, tmpLeftYPositions.Count);
      int randomRightYPosition = Random.Range(0, tmpRightYPositions.Count);

      GameObject pictureGO = InstantiatePicture(picture.name, side, side == "left" ? tmpLeftYPositions[randomLeftYPosition] : tmpRightYPositions[randomRightYPosition]);
      GameObject counterPictureGO = InstantiatePicture(picture.counterName, side == "left" ? "right" : "left", side == "left" ? tmpRightYPositions[randomRightYPosition] : tmpLeftYPositions[randomLeftYPosition]);

      picture.SetCollider(pictureGO.GetComponent<BoxCollider2D>());
      picture.SetCounterCollider(counterPictureGO.GetComponent<BoxCollider2D>());

      tmpLeftYPositions.RemoveAt(randomLeftYPosition);
      tmpRightYPositions.RemoveAt(randomRightYPosition);
    }


  }

  GameObject InstantiatePicture(string asset, string side, int yPos)
  {
    // Instantiate picture on left or right side
    GameObject picture = Instantiate(picturePrefab);
    picture.name = asset;

    // Load sprite from Resources/Sprites folder and set it as the picture's sprite
    Sprite sprite = Resources.Load<Sprite>("Sprites/" + asset);
    SpriteRenderer renderer = picture.GetComponent<SpriteRenderer>();
    renderer.sprite = sprite;

    // Calculate left and right pos based on screen width
    float leftPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
    float rightPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    int xPos = side == "left" ? (int)(leftPos + 1.3f) : (int)(rightPos - 1.3f);
    picture.transform.position = new Vector3(xPos, yPos, 0);

    // Set sprite size to match screen size. 2 pictures per row. 3 rows
    float widthValue = (rightPos - leftPos) / sprite.bounds.size.x / 2;

    float screenHeight = Camera.main.orthographicSize * 2;
    float spriteHeight = sprite.bounds.size.y;
    float heightValue = screenHeight / sprite.bounds.size.x / 3;

    // width and height should have the same % difference to the original sprite size
    // so that the sprite doesn't look stretched
    float scale = widthValue < heightValue ? widthValue : heightValue;
    picture.transform.localScale = new Vector3(scale, scale, 0);

    // Set collider size to match sprite size
    BoxCollider2D collider = picture.GetComponent<BoxCollider2D>();
    collider.size = sprite.bounds.size;

    return picture;

  }

  public void SetLevel(int level)
  {
    if(level > MAX_LEVEL)
    {
      level = MAX_LEVEL;
    }
    else if(level < 1)
    {
      level = 1;
    }
    this.level = level;
  }
}
