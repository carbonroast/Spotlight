using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
