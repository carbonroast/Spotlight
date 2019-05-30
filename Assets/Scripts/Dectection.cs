using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dectection : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {

        if (!other.GetComponent<Humanoid>().alive)
        {
            //dead
            other.GetComponent<Humanoid>().Death();
        }
    }
}
