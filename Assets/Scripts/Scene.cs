using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene 
{
    public Vector2 location;
    public GameObject sceneGO;
    public Scene(GameObject go, Vector2 loc)
    {
        location = loc;
        sceneGO = go;
    }
}
