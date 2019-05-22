using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Humanoid {

    public Animator anim;
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
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        path = new NavMeshPath();
        Debug.Log("AGENT: " + agent.hasPath);
        anim = GetComponent<Animator>();
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
        newLocation = NextLocation(searchRadius);
        Walk();
        StartCoroutine("Stop");
    }

    public Vector3 NextLocation(Vector2 searchRadius)
    {
        float radius = Random.Range(searchRadius.x, searchRadius.y);
        float angle = Random.Range(0, 360);

        float x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + this.currentPosition.x;
        float z = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + this.currentPosition.z;

        bool check = NavMesh.CalculatePath(this.transform.position, new Vector3(x, 0, z), NavMesh.AllAreas, path);

        Debug.DrawLine(this.transform.position, new Vector3(x, 0, z), Color.cyan, 4f);
        if (check)
        {
            return new Vector3(x, 0, z);
        }
        else
        {
            return NextLocation(searchRadius);
        }
    }


    public void CheckIfWalking()
    {
        if (!agent.pathPending)
        {
            //Debug.Log(agent.hasPath + " " + agent.velocity.sqrMagnitude);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    idle = true;
                }
            }
        }
    }
    public override void  Walk()
    {
        idle = false;
        SetAnimation("idle", idle);
        PlayAnimation("idle", 0, 0f);
        agent.destination = newLocation;

    }

    IEnumerator Stop()
    {
        while (!idle)
        {
            yield return null;
        }
        newActionTime = WalkCooldown(cooldownRange);
        SetAnimation("idle", idle);
        PlayAnimation("idle", 0, 0f);
        Invoke("AiControls", newActionTime);
    }

    public override void Death()
    {
        alive = false;
        SetAnimation("alive", alive);
        PlayAnimation("hit", 0, 0f);
        agent.Stop();
        //this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetAnimation(string name, bool boolean)
    {
        anim.SetBool(name, boolean);
    }
    public void PlayAnimation(string name, int layer, float time)
    {
        anim.Play(name, layer, time);
    }
}
