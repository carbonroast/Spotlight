using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 input;
    public float speed;
    public bool fire;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
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
        float walkx = Luminosity.IO.InputManager.GetAxisRaw("Horizontal");
        float walkz = Luminosity.IO.InputManager.GetAxisRaw("Vertical");
        //its a sprite
        input = new Vector3(walkx, walkz, 0).normalized;

        fire = Input.GetButtonDown("Fire1");
    }

    public void Move()
    {

        this.transform.Translate(input * Time.fixedDeltaTime * speed);
    }

    public void Fire()
    {
        if (fire)
        {
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
}
