using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Humanoid {

    public Vector2 cooldownRange;
    public Vector3 currentPosition;
    public bool idle;
    public Vector3 newLocation;
    public float duration;
    public Vector3 direction;
    public float newActionTime;
   


    // Use this for initialization
    public override void Start () {
        base.Start();
        Invoke("AiControls", 1.0f);

	}
	
	// Update is called once per frame
	void Update () {

	}

     void FixedUpdate()
    {
        if (!idle)
        {
            Walk();
        }   
    }


    public float WalkCooldown(Vector2 cooldownRange)
    {
        return Random.Range(cooldownRange.x, cooldownRange.y);
    }

    public void AiControls()
    {

        currentPosition = this.transform.position;
        duration = AiWalkDuration();
 
        direction = AiWalkDirection();
        //Debug.Log("Moving " + Time.time);
        //Debug.Log("Moving until " + (Time.time + duration));

        idle = false;
        StartCoroutine("Stop");

        
    }

    public Vector3 AiWalkDirection()
    {
        float walkx = Mathf.Floor(Random.Range(-2f, 2f)) / 2;
        float walkz = Mathf.Floor(Random.Range(-2f, 2f)) / 2; 
        //Debug.Log("Current Position " + currentPosition + " " + walkx + " " + walkz);
        //Debug.Log(new Vector3(currentPosition.x + walkx, 0, currentPosition.z + walkz).normalized);
        return new Vector3(walkx, 0, walkz);
    }

    public float AiWalkDuration()
    {
        return Random.Range(4, 6);
    }

    public override void  Walk()
    {
        this.transform.Translate(direction * Time.fixedDeltaTime * speed);
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(duration);
        //Debug.Log("Stopped at " + Time.time);
        this.transform.Translate(new Vector3(0, 0, 0));
        newActionTime = WalkCooldown(cooldownRange);
        //Debug.Log("Next Movement at " + (Time.time + newActionTime));
        idle = true;
        Invoke("AiControls", newActionTime);
    }

    public override void Death()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
