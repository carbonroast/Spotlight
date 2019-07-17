using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public GameObject scene;
    public GameObject game;
    public GameObject lobby;
    public static Scene SMain;
    public static Scene Sgame;
    public static Scene Slobby;
    public static Stack<Scene> sceneView;

    private void Awake()
    {
        sceneView = new Stack<Scene>();
    }
    // Start is called before the first frame update
    void Start()
    {

        scene = GameObject.Find("MainmenuScene");
        SMain = new Scene(scene.gameObject, scene.transform.position);
        AddMainScene();

        game = GameObject.Find("GameScene");
        Sgame = new Scene(game.gameObject, game.transform.position);

        lobby = GameObject.Find("LobbyScene");
        Slobby = new Scene(lobby.gameObject, lobby.transform.position);


    }

    public static void AddMainScene()
    {
        AddScene(SMain);
    }

    public static void AddLobbyScene()
    {
        AddScene(Slobby);
    }

    public static void AddGameScene()
    {
        AddScene(Sgame);
    }

    static void AddScene(Scene scene)
    {
        if(sceneView.Count > 0)
        {
            sceneView.Peek().sceneGO.transform.position = sceneView.Peek().location;
            foreach (Transform child in sceneView.Peek().sceneGO.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        sceneView.Push(scene);
        scene.sceneGO.transform.position = new Vector2(0, 0);
        foreach (Transform child in sceneView.Peek().sceneGO.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    

    static void PopScene()
    {
        sceneView.Peek().sceneGO.transform.position = sceneView.Peek().location;
        foreach (Transform child in sceneView.Peek().sceneGO.transform)
        {
            child.gameObject.SetActive(false);
        }
        sceneView.Pop();
        sceneView.Peek().sceneGO.transform.position = new Vector2(0, 0);
        foreach (Transform child in sceneView.Peek().sceneGO.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
