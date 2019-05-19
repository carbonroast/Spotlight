using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid {

    public Vector3 input;
    public bool fire;
    public List<GameObject> OverlappingNpcs;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        GetInputs();
        Attack();
	}

    void FixedUpdate()
    {
        Walk();
    }

    public void GetInputs()
    {
        float walkx = Input.GetAxisRaw("Horizontal");
        float walkz = Input.GetAxisRaw("Vertical");
        input = new Vector3(walkx, 0, walkz);

        fire = Input.GetButtonDown("Fire1");
    }

    public override void Walk()
    {

        this.transform.Translate( input * Time.fixedDeltaTime * speed);
    }

    public void Attack()
    {
        if (fire)
        {
            if(OverlappingNpcs.Count > 0)
            {
                OverlappingNpcs[0].gameObject.GetComponent<Humanoid>().Death();
                OverlappingNpcs.RemoveAt(0);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            Debug.Log("enter");
            OverlappingNpcs.Add(other.gameObject);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            Debug.Log("exit");
            OverlappingNpcs.Remove(other.gameObject);
        }
    }
}
