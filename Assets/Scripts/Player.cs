using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid {

    public Vector3 input;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    void FixedUpdate()
    {
        walk();
    }

    public void GetInput()
    {
        float walkx = Input.GetAxisRaw("Horizontal");
        float walkz = Input.GetAxisRaw("Vertical");
        input = new Vector3(walkx, 0, walkz);
    }

    public override void walk()
    {

        this.transform.Translate( input * Time.fixedDeltaTime * speed);
    }
}
