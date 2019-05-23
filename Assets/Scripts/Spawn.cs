using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int units;
    public List<GameObject> spawnables;
    [HideInInspector]
    public GameObject map;



    //Unit Variables
    public Vector2 cooldownRange;
    public Vector2 searchRadius;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map");
        for(int i = 0; i < units; i++)
        {
            Vector3 location = SpawnAt();
            spawnNPC(location, i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 SpawnAt()
    {
        float randomX = Random.Range(map.transform.position.x - (map.transform.localScale.x * 5), map.transform.position.x + (map.transform.localScale.x * 5));
        float randomZ = Random.Range(map.transform.position.y - (map.transform.localScale.z * 5), map.transform.position.z + (map.transform.localScale.z * 5));
        return new Vector3(randomX, 0, randomZ);
    }

    public void spawnNPC(Vector3 location, int unitNumber)
    {

        int randomPrefab = Random.Range(0, spawnables.Count);
        GameObject go = Instantiate(spawnables[randomPrefab],location,Quaternion.Euler(0, Random.Range(0.0f, 360.0f),0));
        go.name = "unit " + unitNumber;
        go.layer = LayerMask.NameToLayer("Unit");
        go.AddComponent<NPC>();
        go.GetComponent<NPC>().speed = speed;
        go.GetComponent<NPC>().cooldownRange = cooldownRange;
        go.GetComponent<NPC>().searchRadius = searchRadius;
    }
}
