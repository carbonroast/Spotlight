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
        float walkx = Input.GetAxisRaw("Horizontal");
        float walkz = Input.GetAxisRaw("Vertical");
        input = new Vector3(walkx, walkz, 0);

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
            Debug.Log("fire");
            RaycastHit hit;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(this.transform.position);
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            Debug.Log("Ray Origin :" + ray.origin + " Direction :" +  ray.direction );
            Debug.DrawLine(mainCamera.transform.position, this.transform.position, Color.blue);
            if (Physics.Raycast(ray, out hit, 20f))
            {
                GameObject objectHit = hit.transform.gameObject;
                Debug.Log("hit " + objectHit.gameObject.name);
            }
        }

    }
}
