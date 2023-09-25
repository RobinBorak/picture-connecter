using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    void Start()
    {
        // For performance reasons, the GameOverScreen is active in the scene. here we deactivate it.
        // This increased performance to active it from 1700ms to 0-2ms.
        //gameObject.SetActive(false);
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }


}
