using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour {

    public float speed;
    public bool alive;


    public virtual void Walk() { }
    
    //public virtual void Killed() { }

    public virtual void Death() { }
}
