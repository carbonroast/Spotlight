using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptManager : MonoBehaviour
{
    List<Component> lobbyComponenets;
    List<Component> gameComponents;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveLobbyScripts()
    {

    }

    public void SetActiveGameScripts()
    {
        foreach(Component component in lobbyComponenets)
        {

        }

    }

    public void ActiveScripts(List<Component> scripts, bool active)
    {
        foreach (Component component in scripts)
        {

        }
    }
}
