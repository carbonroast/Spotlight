using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Luminosity.IO
{ 

    public class Lobby : MonoBehaviour
    {
        public List<PlayerID> players;
        public static HashSet<PlayerID> hunterTeam = new HashSet<PlayerID>();
        public static HashSet<PlayerID> npcTeam = new HashSet<PlayerID>();

        public Sprite sprite_start;
        public Sprite npc;
        public Sprite hunter;

        public string[] controllers;

        public Dictionary<string, GameObject> controllersGO = new Dictionary<string, GameObject>();

        public List<PlayerID> listOfPlayers = new List<PlayerID> { PlayerID.One, PlayerID.Two, PlayerID.Three, PlayerID.Four };

        public void Awake()
        {
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
            foreach (PlayerID npc in npcTeam)
            {
                print("NPC Player- " + npc.ToString());
            }
            foreach (PlayerID hunter in hunterTeam)
            {
                print("HUNTER Player- " + hunter.ToString());
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
            }
        }

        public void AddPlayer(HashSet<PlayerID> team, PlayerID player)
        {
            team.Add(player);
            print("Tryed to add Player " + player.ToString());
        }

        public void RemovePlayer(HashSet<PlayerID> team, PlayerID player)
        {
            PlayerID ? target = null;
            
            foreach (PlayerID joinedPlayer in team)
            {
                if(player == joinedPlayer)
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
                
                this.transform.Find("Start").GetComponent<TextMeshProUGUI>().text = "Start";
                this.transform.Find("Start").GetComponentInChildren<SpriteRenderer>().sprite = sprite_start;
                foreach (PlayerID player in hunterTeam)
                {
                    if (InputManager.GetButtonDown("Start Button", player))
                    {
                        Utils.Play();
                    }
                }
                foreach (PlayerID player in npcTeam)
                {
                    if (InputManager.GetButtonDown("Start Button", player))
                    {
                        Utils.Play();
                    }
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