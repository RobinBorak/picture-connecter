using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineGenerator : MonoBehaviour
{
    public class ImageObject
    {
        public string colliderName;
        public string counterColliderName;
        public string side;
        public bool successful = false;

        public BoxCollider2D collider;
        public BoxCollider2D counterCollider;

        public ImageObject(string colliderName, string counterColliderName, string side){
            this.colliderName = colliderName;
            this.counterColliderName = counterColliderName;
            this.side = side;

            this.collider = GameObject.Find(colliderName).GetComponent<BoxCollider2D>();
            this.counterCollider = GameObject.Find(counterColliderName).GetComponent<BoxCollider2D>();
        }

        public void SetSuccessful(bool successful){
            this.successful = successful;
        }
    } 


    public GameObject linePrefab;
    public GameOverScreen gameOverScreen;
    List<ImageObject> leftImageObjects = new List<ImageObject>();

    void Start(){
        leftImageObjects.Add(new ImageObject("Cat", "Hat", "left"));
        leftImageObjects.Add(new ImageObject("Mouse", "House", "left"));
        leftImageObjects.Add(new ImageObject("Saw", "Train", "left"));

    }

    Line activeLine;
    GameObject newLine;

    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("mouse down");

            newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();
        }

        if(Input.GetMouseButtonUp(0)){
            activeLine = null;
            List<Vector2> points = newLine.GetComponent<Line>().GetPoints();
            Debug.Log(points.Count);

            if(points.Count > 1){
                if(isPointsSuccessful(points)){
                    Debug.Log("Success!");
                } else {
                    Debug.Log("Fail no points successful!");
                    Destroy(newLine);
                }
            } else {
                Debug.Log("Fail no lines exists!");
                Destroy(newLine);
            }

            if(isAllSuccessful()){
                //Debug.Log("All successful!");
                GameOver();
            }

            //LogAllImageObjects();
        }

        if(activeLine != null){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }

    bool isPointsSuccessful(List<Vector2> points){
        Vector2 firstPoint = points[0];
        Vector2 lastPoint = points[points.Count - 1];

        foreach(ImageObject imageObject in leftImageObjects){
            if(imageObject.collider.bounds.Contains(firstPoint)){
                Debug.Log("first point inside left image");
                if(imageObject.counterCollider.bounds.Contains(lastPoint)){
                    if(imageObject.successful){
                        Debug.Log("Already successful!");
                        return false;
                    }
                    Debug.Log("Success!");
                    imageObject.SetSuccessful(true);
                    return true;
                }
            } else if(imageObject.counterCollider.bounds.Contains(firstPoint)){
                Debug.Log("first point inside right image");
                if(imageObject.collider.bounds.Contains(lastPoint)){
                    if(imageObject.successful){
                        Debug.Log("Already successful!");
                        return false;
                    }
                    Debug.Log("Success!");
                    imageObject.SetSuccessful(true);
                    return true;
                }
            }
        }
        return false;
    }

    bool isAllSuccessful(){
        foreach(ImageObject imageObject in leftImageObjects){
            if(!imageObject.successful){
                return false;
            }
        }
        return true;
    }

    public void Reset(){
        foreach(ImageObject imageObject in leftImageObjects){
            imageObject.SetSuccessful(false);
        }

        foreach(GameObject line in GameObject.FindGameObjectsWithTag("Line")){
            Destroy(line);
        }
    }

    void LogAllImageObjects(){
        foreach(ImageObject imageObject in leftImageObjects){
            Debug.Log(imageObject.colliderName + " " + imageObject.successful);
        }
    }

    void GameOver(){
        gameOverScreen.Setup();
    }
}
