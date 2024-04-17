using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;
    private int countSpace = 0;

    IEnumerator SpawnCustomers(float time)
    {

        yield return new WaitForSeconds(time);
        while (true)
        {
            if (countSpace <= 8)
            {
                float randomNumber = Random.Range(1, 4);
                for (int i = 0; i < randomNumber; i++)
                {
                    Instantiate(customerPrefab, transform.GetChild(i));
                }
                time = Random.Range(10, 30);
                countSpace++;
                yield return new WaitForSeconds(time);
            }
        }
        
        
        
    }
    private void Start()
    {
        StartCoroutine(SpawnCustomers(Random.Range(10,30)));
        
    }
}
