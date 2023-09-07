using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    public GameOverScreen gameOverScreen;
    public GameObject PictureGenerator;
    List<PictureGenerator.Picture> pictures;
    Line activeLine;
    GameObject newLine;
    bool gameOver = false;

    void Start()
    {
        pictures = PictureGenerator.GetComponent<PictureGenerator>().pictures;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameOver)
            {
                return;
            }
            Debug.Log("mouse down");

            newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Active line is null when clicking playagain button.
            if (gameOver || activeLine == null)
            {
                return;
            }

            Debug.Log("mouse up");
            List<Vector2> points = activeLine.GetPoints();

            if (points.Count > 1)
            {
                if (isPointsSuccessful(points))
                {
                    Debug.Log("Success!");
                }
                else
                {
                    Debug.Log("Fail no points successful!");
                    Destroy(newLine);
                }
            }
            else
            {
                Debug.Log("Fail no lines exists!");
                Destroy(newLine);
            }

            if (isAllSuccessful())
            {

                GameOver();
            }

            activeLine = null;

        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }

    }

    bool isPointsSuccessful(List<Vector2> points)
    {
        Vector2 firstPoint = points[0];
        Vector2 lastPoint = points[points.Count - 1];

        foreach (PictureGenerator.Picture picture in pictures)
        {
            if (picture.collider.bounds.Contains(firstPoint))
            {
                Debug.Log("first point inside left image");
                if (picture.counterCollider.bounds.Contains(lastPoint))
                {
                    if (picture.successful)
                    {
                        Debug.Log("Already successful!");
                        return false;
                    }
                    Debug.Log("Success!");
                    picture.SetSuccessful(true);
                    return true;
                }
            }
            else if (picture.counterCollider.bounds.Contains(firstPoint))
            {
                Debug.Log("first point inside right image");
                if (picture.collider.bounds.Contains(lastPoint))
                {
                    if (picture.successful)
                    {
                        Debug.Log("Already successful!");
                        return false;
                    }
                    Debug.Log("Success!");
                    picture.SetSuccessful(true);
                    return true;
                }
            }
        }
        return false;
    }

    bool isAllSuccessful()
    {
        foreach (PictureGenerator.Picture picture in pictures)
        {
            if (!picture.successful)
            {
                return false;
            }
        }
        return true;
    }

    public void Reset()
    {
        gameOver = false;
        foreach (GameObject line in GameObject.FindGameObjectsWithTag("Line"))
        {
            Destroy(line);
        }
    }

    void GameOver()
    {
        gameOver = true;
        gameOverScreen.Setup();
    }
}
