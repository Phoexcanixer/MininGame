using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    public Transform Floor;
    public GameObject OrePrefab; // drag the ore prefab into this field in the inspector
    public int NumOres = 10; // number of ores to generate

    void Start()
    {
        // generate the ores
        for (int i = 0; i < NumOres; i++)
        {
            // generate a random position within the bounds of the plane
            float xPos = Random.Range(-Floor.localScale.x / 2 , Floor.localScale.x / 2) * Floor.localScale.x + Floor.position.x;
            float zPos = Random.Range(-Floor.localScale.z / 2 , Floor.localScale.z / 2) * Floor.localScale.z + Floor.position.z;

            // create an instance of the ore prefab at the random position
            var ore = Instantiate(OrePrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
            ore.transform.SetParent(Floor, true);
        }
    }
}
