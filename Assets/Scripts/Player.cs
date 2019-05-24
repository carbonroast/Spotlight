using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Player : Humanoid {
    public Animator anim;
    public Vector3 input;
    public bool fire;
    public List<GameObject> OverlappingNpcs;
    public string player;
    // Use this for initialization
    public void Start()
    {
        anim = GetComponent<Animator>();
        SetPrefabHitBox();
        GetComponent<Rigidbody>().useGravity = false;
    }

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
        input = new Vector3(walkx, 0, walkz).normalized;

        fire = Input.GetButtonDown("Fire1");
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
                //OverlappingNpcs[0].gameObject.GetComponent<Humanoid>().alive = false;
                OverlappingNpcs.RemoveAt(0);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            //Debug.Log("enter");
            OverlappingNpcs.Add(other.gameObject);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            //Debug.Log("exit");
            OverlappingNpcs.Remove(other.gameObject);
        }
    }
}
