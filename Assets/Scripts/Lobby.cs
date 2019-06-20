using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Luminosity.IO
{ 

    public class Lobby : MonoBehaviour
    {
        public List<PlayerID> players;
        public List<PlayerID> joinedPlayers;

        public Sprite sprite_player;
        public Sprite sprite_start;

        public string[] controllers;

        public Dictionary<string, GameObject> controllersGO = new Dictionary<string, GameObject>();

        public List<PlayerID> listOfPlayers = new List<PlayerID> { PlayerID.One, PlayerID.Two, PlayerID.Three, PlayerID.Four };

        public void Awake()
        {
          
        }

        public void Start()
        {
            string s = 3.ToString();
            print(s);
            AddPlayers();

        }
        public void Update()
        {
            DetectControllers();
            foreach (PlayerID player in players)
            {
                if (InputManager.GetButtonDown("Action Bottom", player))
                {
                    PlayerJoined(player);
                    
                }
            }

            foreach (PlayerID player in players)
            {
                if (InputManager.GetButtonDown("Action Right", player))
                {
                    RemovePlayer(player);

                }
            }

            foreach (PlayerID player in joinedPlayers)
            {
                if(InputManager.GetButtonDown("Start Button", player))
                {
                    Utils.Play();
                }
            }
            
        }

        public void DetectControllers()
        {
            controllers = InputManager.GetJoystickNames();
            for (int i=0; i < controllers.Length; i++)
            {

                GameObject go = controllersGO["Player " + i];
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

        public void AddPlayers()
        {
            
            for(int i=0; i < listOfPlayers.Count; i++)
            {
                
                players.Add(listOfPlayers[i]);
                
                GameObject go = new GameObject("Player " + i);

                go.AddComponent<LobbyControls>().player = listOfPlayers[i];
                go.SetActive(false);
                controllersGO.Add("Player " + i, go);
            }
        }


        public void DisplayStart()
        {
            if(joinedPlayers.Count >= 2)
            {
                
                this.transform.Find("Start").GetComponent<TextMeshProUGUI>().text = "Start";
                this.transform.Find("Start").GetComponentInChildren<SpriteRenderer>().sprite = sprite_start;
            }
            else
            {
                this.transform.Find("Start").GetComponent<TextMeshProUGUI>().text = null;
                this.transform.Find("Start").GetComponentInChildren<SpriteRenderer>().sprite = null;
            }
        }

        public void PlayerJoined(PlayerID player)
        {

            joinedPlayers.Add(player);
            Debug.Log(player.ToString() + " pressed key");
            this.transform.Find(player.ToString()).GetComponentInChildren<TextMeshProUGUI>().text = "Hello";
            this.transform.Find(player.ToString()).GetComponentInChildren<SpriteRenderer>().sprite = sprite_player;
            
        }

        public void RemovePlayer(PlayerID player)
        {
            this.transform.Find(player.ToString()).GetComponentInChildren<TextMeshProUGUI>().text = "Player " + player.ToString();
            this.transform.Find(player.ToString()).GetComponentInChildren<SpriteRenderer>().sprite = null;
            joinedPlayers.Remove(player);
        }

    }


}