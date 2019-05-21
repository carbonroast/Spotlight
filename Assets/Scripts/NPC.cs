using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Humanoid {

    public Vector2 cooldownRange;
    public Vector2 searchRadius;
    //public Vector2 walkDurationRange;
    public Vector3 currentPosition;
    public bool idle;
    public Vector3 newLocation;
    //public float duration;
    //public Vector3 direction;
    public float newActionTime;
    public NavMeshPath path;
    public NavMeshAgent agent;


    // Use this for initialization
    public override void Start () {
        base.Start();
        path = new NavMeshPath();
        agent.updateRotation = false;
        Invoke("AiControls", 1.0f);

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }
    void FixedUpdate()
    {
        if (!idle)
        {
            CheckIfWalking();
        }   
    }


    public float WalkCooldown(Vector2 cooldownRange)
    {
        return Random.Range(cooldownRange.x, cooldownRange.y);
    }

    public void AiControls()
    {

        currentPosition = this.transform.position;
        //duration = AiWalkDuration(walkDurationRange);

        newLocation = NextLocation(searchRadius);
        //Debug.Log("Moving " + Time.time);
        //Debug.Log("Moving until " + (Time.time + duration));

        idle = false;
        Walk();
        StartCoroutine("Stop");

        
    }

    public Vector3 NextLocation(Vector2 searchRadius)
    {
        float radius = Random.Range(searchRadius.x, searchRadius.y);
        float angle = Random.Range(0, 360);

        float x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + this.currentPosition.x;
        float z = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + this.currentPosition.z;
        //float walkx = Mathf.Floor(Random.Range(-2f, 2f)) / 2;
        //float walkz = Mathf.Floor(Random.Range(-2f, 2f)) / 2; 
        //Debug.Log("Current Position " + currentPosition + " " + walkx + " " + walkz);
        //Debug.Log(new Vector3(currentPosition.x + walkx, 0, currentPosition.z + walkz).normalized);
        bool check = NavMesh.CalculatePath(this.transform.position, new Vector3(x, 0, z), NavMesh.AllAreas, path);

        Debug.DrawLine(this.transform.position, new Vector3(x, 0, z), Color.cyan, 4f);
        if (check)
        {
            //Debug.Log("Path is Good");
            return new Vector3(x, 0, z);
        }
        else
        {
            //Debug.Log("False Trying again");
            return NextLocation(searchRadius);
        }
    }

    //public float AiWalkDuration(Vector2 walkDurationRange)
    //{
    //    return Random.Range(walkDurationRange.x, walkDurationRange.y);
    //}
    public void CheckIfWalking()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                idle = true;
            }
        }
    }
    public override void  Walk()
    {
        //Debug.DrawLine(this.transform.position, direction, Color.red, 4f);
        //this.transform.Translate(direction * Time.fixedDeltaTime * speed);
        agent.destination = newLocation;
    }

    IEnumerator Stop()
    {
        while (!idle)
        {
            yield return null;
        }
        //Debug.Log("Stopped at " + Time.time);
        //this.transform.Translate(new Vector3(0, 0, 0));
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
