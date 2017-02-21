using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    private int health = 100;
    private GameObject player;
    private Transform playerPosition;

    public GameObject shotSpawner;
    public GameObject shotPrefab;

    private float lastShot = 0f;
    public float shotInterval = 10f;
    public float intervalNegOffset = -5.0f;
    public float intervalPosOffset = 5.0f;
    public float rateOfFire = 5f;
    public float maxDistance = 8.0f;
    public float minDistance = 2.0f;
    public float shotDelay = 0.5f;

    public GameObject[] shots;
    private int shotIndex = 0;
    private GameObject shotBin;

    public GameObject effects;
    //public GameObject explosionPrefab;
    //private GameObject explosion;
    //private bool exploded = false;

    public int enemyIndex = 0;

    void Start () {
        effects = GameObject.Find("Effects");
        shotBin = GameObject.Find("ShotBin");
        GameObject shotContainer = new GameObject();
        shotContainer.name = transform.name + " shotbin";
        shotContainer.transform.parent = shotBin.transform;
        player = GameObject.Find("Player");
        playerPosition = player.transform;
        //explosion = Instantiate(explosionPrefab, transform.parent);
        //explosion.SetActive(false);
        SetupShots();
	}
	
    void SetupShots()
    {
        shots = new GameObject[10];
        for (int i = 0; i < shots.Length; i++)
        {
            shots[i] = Instantiate(shotPrefab, shotBin.transform.GetChild(enemyIndex).transform);
            shots[i].SetActive(false);
        }
    }

	void Update () {
        playerPosition = player.transform;
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= maxDistance)
        {
            Quaternion rotationAngle = Quaternion.LookRotation(playerPosition.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * 4);
            Vector3 targetDir = playerPosition.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            if (dist >= minDistance)
            {
                transform.position += transform.forward * 5 * Time.deltaTime;
            }


            if (angle <= 10.0f && angle >= -10.0f)
            {
                if (lastShot <= 0) {
                    StartCoroutine("delayShot");
                    lastShot = shotInterval + Random.Range(intervalNegOffset, intervalPosOffset);
                } else
                {
                    lastShot -= rateOfFire * Time.deltaTime;
                }
            }
        }
    }

    IEnumerator delayShot()
    {
        yield return new WaitForSeconds(shotDelay);
        _Fire();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            effects.gameObject.GetComponent<Effects>().Explode(transform.position);
            gameObject.SetActive(false);
        }
    }

    public void _Fire()
    {
        if (shots[shotIndex].gameObject.activeSelf == false)
        {
            shots[shotIndex].transform.position = shotSpawner.transform.position;
            shots[shotIndex].GetComponent<Shot>().Shoot(shotSpawner.transform);
        }
        shotIndex = shotIndex < shots.Length - 1 ? shotIndex + 1 : 0;
    }
}
