using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    public GameObject explosionPrefab;
    public int explosionCount = 10;
    private GameObject[] explosions;
    private int explosionIndex = 0;

    public GameObject explosionsParent;

	// Use this for initialization
	void Start () {
        explosions = new GameObject[explosionCount];
        for (int i = 0; i < explosionCount; i++)
        {
            explosions[i] = Instantiate(explosionPrefab, explosionsParent.transform);
        }
	}
	
	public void Explode(Vector3 position)
    {
        explosions[explosionIndex].transform.position = position;
        explosions[explosionIndex].SetActive(true);
        explosionIndex = (explosionIndex < explosions.Length) ? explosionIndex + 1 : 0;
    }

	void Update () {
		
	}
}
