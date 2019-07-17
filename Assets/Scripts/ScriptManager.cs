using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptManager : MonoBehaviour
{
    public List<Component> lobbyComponenets;
    public List<Component> gameComponents;
    // Start is called before the first frame update
    private void Awake()
    {
        lobbyComponenets = new List<Component>();
        gameComponents = new List<Component>();
    }

    public void SetActiveLobbyScripts()
    {
        ActiveScripts(lobbyComponenets, true);
        ActiveScripts(gameComponents, false);
    }

    public void SetActiveGameScripts()
    {
        ActiveScripts(gameComponents, true);
        ActiveScripts(lobbyComponenets, false);
    }

    public void ActiveScripts(List<Component> scripts, bool active)
    {
        foreach (Component component in scripts)
        {
            if (active)
            {
                GetComponent<MonoBehaviour>().enabled = true;
            }
            else
            {
                GetComponent<MonoBehaviour>().enabled = false;
            }
        }
    }
}
