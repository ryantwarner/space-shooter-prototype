using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupController : MonoBehaviour {

    private float axisHorizontal = 0f;
    private float axisVertical = 0f;
    private float axisHorizontalRight = 0f;
    private float axisVerticalRight = 0f;
    public float axisDeadzone = .2f;

    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    private bool fireButton = false;
    private bool boostButton = false;
    public float boostMultiplier = 2.0f;

    private CharacterController characterController;

    public GameObject shotSpawner;
    public GameObject shotPrefab;

    public GameObject[] shots;
    private int shotIndex = 0;

    public GameObject tail;
    private ParticleSystem tailParciles;
    float defaultParticleSpeed;
    float defaultParticleLifetime;

    private int health = 100;
    private float boost = 100;
    public float boostRechargeRate = 20f;
    public float boostBurnRate = 30f;
    public int boostMax = 100;

    public GameObject effects;

    void Start() {
        effects = GameObject.Find("Effects");
        shots = new GameObject[10];
        for (int i = 0; i < shots.Length; i++)
        {
            shots[i] = Instantiate(shotPrefab);
            shots[i].SetActive(false);
        }
        characterController = GetComponent<CharacterController>();
        tailParciles = tail.GetComponent<ParticleSystem>();
        defaultParticleLifetime = tailParciles.main.startLifetime.constant;
        defaultParticleSpeed = tailParciles.main.startSpeed.constant;
    }

    void Update() {
        if (health > 0)
        {
            GetInput();
            Move();
            Rotate();
            boost = Mathf.Clamp(boost + boostRechargeRate * Time.deltaTime, 0, boostMax);
        } else
        {
            effects.GetComponent<Effects>().Explode(transform.position);
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void LateUpdate()
    {
        if (fireButton)
        {
            Fire();
        }
    }

    void GetInput()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");
        axisHorizontalRight = Input.GetAxis("HorizontalRight");
        axisVerticalRight = Input.GetAxis("VerticalRight");

        fireButton = Input.GetButtonDown("Fire1");
        boostButton = Input.GetButton("Fire3");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Fire()
    {

        if (shots[shotIndex].gameObject.activeSelf == false)
        {
            shots[shotIndex].transform.position = shotSpawner.transform.position;
            shots[shotIndex].GetComponent<Shot>().Shoot(shotSpawner.transform);
        }
        shotIndex = shotIndex < shots.Length - 1 ? shotIndex + 1 : 0;
        fireButton = false;
    }

    private void Move()
    {
        var main = tailParciles.main;
        if (System.Math.Abs(axisHorizontal) > axisDeadzone || System.Math.Abs(axisVertical) > axisDeadzone)
        {
            float _speed = moveSpeed;
            if (boostButton)
            {
                boost = Mathf.Clamp(boost - (boostBurnRate * Time.deltaTime), 0, boostMax);
                if (boost >= 0.2)
                {
                    _speed *= boostMultiplier;
                } else
                {
                    _speed = moveSpeed;
                }
            }
            float xMove = (axisHorizontal * _speed) * Time.deltaTime;
            float zMove = (axisVertical * _speed) * Time.deltaTime;
            Vector3 newPos = Vector3.zero;
            newPos += transform.forward * zMove;
            newPos += transform.right * xMove;
            characterController.Move(newPos);
        } else
        {
            main.startLifetime = defaultParticleLifetime;
            main.startSpeed = defaultParticleSpeed;
        }
    }

    private void Rotate()
    {
        if (System.Math.Abs(axisHorizontalRight) > axisDeadzone || System.Math.Abs(axisVerticalRight) > axisDeadzone)
        {
            transform.Rotate((Vector3.up * axisHorizontalRight * moveSpeed * rotateSpeed) * Time.deltaTime);
        }
    }

    public int ShotsRemaining()
    {
        int count = 0;
        for (int i = 0; i < shots.Length; i++)
        {
            count += shots[i].gameObject.activeSelf == false ? 1 : 0;
        }
        return count;
    }

    public int BoostRemaining()
    {
        return (int)Mathf.Round(boost);
    }
}
