using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid {

    public Vector3 input;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        GetInputs();
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
    }

    public override void Walk()
    {

        this.transform.Translate( input * Time.fixedDeltaTime * speed);
    }
}
