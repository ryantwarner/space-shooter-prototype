using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject enemyPrefab;
    public int enemyCount = 10;
    
	void Start () {
		for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, transform);
            transform.GetChild(i + 1).GetComponent<EnemyBase>().enemyIndex = i;
            transform.GetChild(i + 1).transform.position = new Vector3(Random.Range(150.0f, -150.0f), 0, Random.Range(150.0f, -150.0f));
            transform.GetChild(i + 1).Rotate(new Vector3(0, Random.Range(0.0f, 359.9f), 0));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int EnemiesRemaining()
    {
        int count = 0;
        for (int i = 1; i < transform.childCount; i++)
        {
            count += transform.GetChild(i).gameObject.activeSelf == false ? 0 : 1;
        }
        return count;
    }
}
