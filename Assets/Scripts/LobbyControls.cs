using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luminosity.IO
{
    public class LobbyControls : MonoBehaviour
    {
        public PlayerID player;
        public Sprite sniper;
        public Sprite npc;
        public Sprite currentSprite;

        public GameObject lobby;
        // Start is called before the first frame update
        private void Awake()
        {
            lobby = GameObject.Find("Lobby");
        }
        void Start()
        {
           
                
            // Lobby.
            //currentSprite = sniper;
        }

        
        // Update is called once per frame
        void Update()
        {
           float var = Mathf.Round(InputManager.GetAxis("Vertical", player));
        }

        public void SetSprite()
        {
            //if()
        }


    }
}