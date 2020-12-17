using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject addEnergyPrefab;
    public GameObject minusEnergyPrefab;

    public int numberOfspawn = 2;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfspawn; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-15, 15),0,Random.Range(-15, 15));
            Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            if (Random.Range(0, 2) < 1)
            {
                Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            }
            else
            {
                Instantiate(minusEnergyPrefab, randomPos, Quaternion.identity);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
