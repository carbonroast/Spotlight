using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class NPC : Humanoid {

    public Animator anim;
    public Vector2 cooldownRange;
    public Vector2 searchRadius;
    public bool idle;
    public Vector3 newLocation;
    public float newActionTime;
    public NavMeshPath path;
    public NavMeshAgent agent;
    public bool animationRun;

    // Use this for initialization
    public void Start () {
        alive = true;
        animationRun = false;
        anim = GetComponent<Animator>();
        SetPrefabHitBox();
        NavMeshAgentSettings();
        GetComponent<Rigidbody>().useGravity = false;
        float startTime = Random.Range(0.0f, 4.0f);
        Invoke("AiControls", startTime);
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

    public void SetPrefabHitBox()
    {
        CapsuleCollider CC = GetComponent<CapsuleCollider>();
        CC.isTrigger = true;
        CC.center = new Vector3(0, 1, 0);
        CC.radius = 1;
        CC.height = 2;
        CC.direction = 1;
    }
    public void NavMeshAgentSettings()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.angularSpeed = 999;
        agent.acceleration = 999;
        agent.stoppingDistance = 0.2f;
        agent.autoBraking = false;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agent.speed = speed;
        path = new NavMeshPath();
    }

    public float WalkCooldown(Vector2 cooldownRange)
    {
        return Random.Range(cooldownRange.x, cooldownRange.y);
    }

    public void AiControls()
    {
        newLocation = NextLocation(searchRadius);
        Walk();
        StartCoroutine("Stop");
    }

    public Vector3 NextLocation(Vector2 searchRadius)
    {
        float radius = Random.Range(searchRadius.x, searchRadius.y);
        float angle = Random.Range(0.0f, 360.0f);

        float x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + this.transform.position.x;
        float z = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + this.transform.position.z;

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

    public void Killed()
    {
        alive = false;
    }

    public override void Death()
    {
        if(!animationRun)
        {
            CancelInvoke();
            SetAnimation("alive", alive);
            PlayAnimation("hit", 0, 0f);
            agent.isStopped = true;
            StartCoroutine("Fade");
            animationRun = true;
        }

    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = this.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            c.a = f;
            Debug.Log(c.a);
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = c;
            yield return null;
        }
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
