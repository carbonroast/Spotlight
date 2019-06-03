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
        

        public void Start()
        {

            
            AddPlayers();
        }
        public void Update()
        {
            
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

        public void AddPlayers()
        {
            players.Add(PlayerID.One);
            players.Add(PlayerID.Two);
            //players.Add(PlayerID.Three);
            //players.Add(PlayerID.Four);
        }


        public void DisplayStart()
        {
            if(joinedPlayers.Count >= 2)
            {
                
                this.transform.Find("Start").GetComponent<TextMeshProUGUI>().text = "Start";
            }
        }

        public void PlayerJoined(PlayerID player)
        {

            joinedPlayers.Add(player);
            Debug.Log(player.ToString() + " pressed key");
            this.transform.Find(player.ToString()).GetComponentInChildren<TextMeshProUGUI>().text = "Hello";
            
        }

        public void RemovePlayer(PlayerID player)
        {
            this.transform.Find(player.ToString()).GetComponentInChildren<TextMeshProUGUI>().text = "Player " + player.ToString();
            joinedPlayers.Remove(player);
        }

    }


}