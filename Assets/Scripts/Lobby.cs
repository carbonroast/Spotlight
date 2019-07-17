using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Luminosity.IO
{ 

    public class Lobby : MonoBehaviour
    {
        public List<PlayerID> players;
        public static Dictionary<PlayerID, GameObject> hunterTeam;
        public static Dictionary<PlayerID, GameObject> npcTeam;

        public Sprite sprite_start;
        public Sprite npc;
        public Sprite hunter;

        public string[] controllers;

        public Dictionary<string, GameObject> controllersGO = new Dictionary<string, GameObject>();

        public List<PlayerID> listOfPlayers = new List<PlayerID> { PlayerID.One, PlayerID.Two, PlayerID.Three, PlayerID.Four };

        public void Awake()
        {
            hunterTeam = new Dictionary<PlayerID, GameObject>();
            npcTeam = new Dictionary<PlayerID, GameObject>();

            CreatePlayers();
        }

        public void Start()
        {
            

        }
        public void Update()
        {
            DetectControllers();
            DisplayStart();
        }

        public void check()
        {
            
            print("NPC TEAM- " + npcTeam.Count + " | HUNTER TEAM- " + hunterTeam.Count);
            foreach (KeyValuePair<PlayerID, GameObject> npc in npcTeam)
            {
                print("NPC Player- " + npc.Key.ToString());
            }
            foreach (KeyValuePair<PlayerID, GameObject> hunter in hunterTeam)
            {
                print("HUNTER Player- " + hunter.Key.ToString());
            }

        }

        public void DetectControllers()
        {
            controllers = InputManager.GetJoystickNames();
            for (int i=0; i < controllers.Length; i++)
            {

                GameObject go = controllersGO["Player " + (i+1)];
                if (controllers[i] == "")
                {
                    //deactivate
                    go.SetActive(false);
                }
                else
                {
                    //activate is not already active
                    if(go.activeInHierarchy == false)
                    {
                        go.SetActive(true);
                    }
                }
            }
        }

        public void CreatePlayers()
        {
            
            for(int i=0; i < listOfPlayers.Count; i++)
            {
                GameObject go = new GameObject("Player " + (i+1));
                go.AddComponent<LobbyControls>();
                go.SetActive(false);
                go.GetComponent<LobbyControls>().npc = npc;
                go.GetComponent<LobbyControls>().hunter = hunter;
                go.GetComponent<LobbyControls>().player = listOfPlayers[i];
                controllersGO.Add("Player " + (i+1), go);
                go.AddComponent<Player>().enabled = false;
                go.AddComponent<Sniper>().enabled = false;
                go.AddComponent<ScriptManager>();
                DontDestroyOnLoad(go);  
            }
        }

        public void AddPlayer(Dictionary<PlayerID,GameObject> team,  PlayerID player, GameObject playerGO)
        {
            team.Add(player,playerGO);
            print("Tryed to add Player " + player.ToString());
        }

        public void RemovePlayer(Dictionary<PlayerID, GameObject> team, PlayerID player, GameObject playerGO)
        {
            PlayerID ? target = null;
            
            foreach (KeyValuePair<PlayerID, GameObject> joinedPlayer in team)
            {
                if(player == joinedPlayer.Key)
                {
                    target = player;
                    print("Removed Player " + player.ToString());
                }
            }
            if(target != null)
            {
                team.Remove(target ?? default(PlayerID));
            }
            
        }


        public void DisplayStart()
        {
            if(hunterTeam.Count > 0 & npcTeam.Count > 0)
            {
                bool start = false;
                this.transform.Find("Start").GetComponent<TextMeshProUGUI>().text = "Start";
                this.transform.Find("Start").GetComponentInChildren<SpriteRenderer>().sprite = sprite_start;
                foreach (KeyValuePair<PlayerID,GameObject> player in hunterTeam)
                {
                    if (InputManager.GetButtonDown("Start Button", player.Key))
                    {
                        start = true;
                    }
                }
                foreach (KeyValuePair<PlayerID, GameObject> player in npcTeam)
                {
                    if (InputManager.GetButtonDown("Start Button", player.Key))
                    {
                        start = true;
                    }
                }
                if (start)
                {
                    /*
                    foreach (KeyValuePair<PlayerID, GameObject> player in hunterTeam)
                    {
                        player.Value.GetComponent<ScriptManager>().SetActiveGameScripts();
                    }
                    foreach (KeyValuePair<PlayerID, GameObject> player in npcTeam)
                    {
                        player.Value.GetComponent<ScriptManager>().SetActiveGameScripts();
                    }
                    */
                    Debug.Log("GAME STARTING");
                    ViewController.AddGameScene();
                }
            }
            else
            {
                this.transform.Find("Start").GetComponent<TextMeshProUGUI>().text = null;
                this.transform.Find("Start").GetComponentInChildren<SpriteRenderer>().sprite = null;
            }
        }




    }


}