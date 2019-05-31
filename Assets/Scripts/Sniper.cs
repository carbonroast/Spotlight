using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sniper : MonoBehaviour
{
    public Camera mainCamera;
    public int ammo;
    public Vector3 input;
    public float speed;
    public bool fire;
    public Luminosity.IO.PlayerID player;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        SetAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Fire();

    }

    void FixedUpdate()
    {
        Move();
    }

    public void GetInputs()
    {
        float walkx = Mathf.Round(Luminosity.IO.InputManager.GetAxis("Horizontal", player));
        float walkz = Mathf.Round(Luminosity.IO.InputManager.GetAxis("Vertical", player));
        //its a sprite
        input = new Vector3(walkx, walkz, 0).normalized;

        fire = Luminosity.IO.InputManager.GetButtonDown("Action Bottom", player);
    }

    public void Move()
    {

        this.transform.Translate(input * Time.fixedDeltaTime * speed);
    }

    public void Fire()
    {
        if (fire && ammo > 0)
        {
            ammo--;
            SetAmmo();
            RaycastHit hit;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(this.transform.position);
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            Debug.DrawLine(mainCamera.transform.position, this.transform.position, Color.blue);
            LayerMask mask = LayerMask.GetMask("Unit");
            if (Physics.Raycast(ray, out hit, 20f, mask))
            {
                GameObject objectHit = hit.transform.gameObject;
                Debug.Log("hit " + objectHit.gameObject.name);

                objectHit.GetComponent<Humanoid>().Death();
                objectHit.GetComponent<Humanoid>().alive = false;
            }
        }

    }
    public void SetAmmo()
    {
        this.GetComponentInChildren<TextMeshProUGUI>().text = ammo.ToString();
        //this.transform.Find("ammo").GetComponent<TextMeshProUGUI>().text = ammo.ToString();
    }

}
