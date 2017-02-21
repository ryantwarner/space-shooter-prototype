using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupCameraController : MonoBehaviour {

    public GameObject target;

    public float smoothing = 5f;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            GameObject.Find("Player");
        }
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void LateUpdate()
    {
        Vector3 targetCameraPosition = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }
}
