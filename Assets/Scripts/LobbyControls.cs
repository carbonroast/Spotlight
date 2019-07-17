using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luminosity.IO
{
    public class LobbyControls : MonoBehaviour
    {
        public PlayerID player;
        public Sprite hunter;
        public Sprite npc;
        public Dictionary<PlayerID,GameObject> currentTeam;
        public Utils.lobbyStatus status;
        public GameObject lobby;
        public GameObject sprite;
        public ScriptManager sm;
        public bool returnToNeutral;

        // Start is called before the first frame update
        private void Awake()
        {

            returnToNeutral = true;
            status = Utils.lobbyStatus.Waiting;

        }
        void Start()
        {
            lobby = GameObject.Find("LobbyUI");
            sprite = GameObject.Find("LobbyUI/Player " + player.ToString());
            sm = this.GetComponent<ScriptManager>();
            sm.lobbyComponenets.Add(this.GetComponent<LobbyControls>());
            sm.gameComponents.Add(this.GetComponent<CapsuleCollider>());
            sm.gameComponents.Add(this.GetComponent<Animator>());
            sm.gameComponents.Add(this.GetComponent<Player>());

        }

        
        // Update is called once per frame
        void Update()
        {
            if(status == Utils.lobbyStatus.Waiting)
            {
                if (InputManager.GetButtonDown("Action Bottom", player))
                {
                    status = Utils.lobbyStatus.Joined;

                    sprite.GetComponentInChildren<SpriteRenderer>().sprite = npc;
                    
                    lobby.GetComponent<Lobby>().AddPlayer(Lobby.npcTeam, player, this.gameObject);
                    currentTeam = Lobby.npcTeam;
                }
            }
            if(status == Utils.lobbyStatus.Joined)
            {
                float var = Mathf.Round(InputManager.GetAxis("Vertical", player));
                if (var != 0)
                {
                    if (returnToNeutral)
                    {
                        if (currentTeam == Lobby.npcTeam)
                        {
                            print("Hunter Team");
                            lobby.GetComponent<Lobby>().RemovePlayer(currentTeam, player, this.gameObject);
                            currentTeam = Lobby.hunterTeam;
                            lobby.GetComponent<Lobby>().AddPlayer(currentTeam, player, this.gameObject);

                            sm.gameComponents.Add(this.GetComponent<Sniper>());
                            sm.gameComponents.Remove(this.GetComponent<Player>());

                            sprite.GetComponentInChildren<SpriteRenderer>().sprite = hunter;
                        }
                        else if (currentTeam == Lobby.hunterTeam)
                        {
                            print("NPC Team");
                            lobby.GetComponent<Lobby>().RemovePlayer(currentTeam, player, this.gameObject);
                            currentTeam = Lobby.npcTeam;
                            lobby.GetComponent<Lobby>().AddPlayer(currentTeam, player, this.gameObject);

                            sm.gameComponents.Add(this.GetComponent<Player>());
                            sm.gameComponents.Remove(this.GetComponent<Sniper>());

                            sprite.GetComponentInChildren<SpriteRenderer>().sprite = npc;
                        }
                        returnToNeutral = false;
                    }


                }
                if(var == 0)
                {
                    returnToNeutral = true;
                }
                if (InputManager.GetButtonDown("Action Right", player))
                {
                    print("right circle");
                    lobby.GetComponent<Lobby>().RemovePlayer(currentTeam, player, this.gameObject);
                    sprite.GetComponentInChildren<SpriteRenderer>().sprite = null;
                    currentTeam = null;
                    status = Utils.lobbyStatus.Waiting;
                }
            }
            


            if (InputManager.GetButtonDown("Action Left", player))
            {
                
                lobby.GetComponent<Lobby>().check();


            }
        }

        public void SetSprite()
        {

        }

        public void OnEnable()
        {
            print("ENABLED");
        }

        public void OnDisable()
        {
            print("DISABLED");
            if(currentTeam != null)
            {
                lobby.GetComponent<Lobby>().RemovePlayer(currentTeam, player, this.gameObject);
                sprite.GetComponentInChildren<SpriteRenderer>().sprite = null;
                currentTeam = null;
                status = Utils.lobbyStatus.Waiting;
            }
            
        }


    }
}