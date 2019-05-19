using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour {

    public float speed;

    public virtual void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Unit");
    }

    public virtual void Walk() { }
    
    public virtual void Death() { }
}
