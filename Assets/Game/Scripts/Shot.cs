using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public float forwardSpeed = 1000f;
    
    public void OnTriggerEnter(Collider coll)
    {
        // in the case where 2 shots collide with the same thing at the same time this can cause a warning
        coll.SendMessage("TakeDamage", 100);
        gameObject.SetActive(false);
    }

    public void Shoot(Transform _t)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(true);
        transform.eulerAngles = _t.parent.transform.eulerAngles;
        GetComponent<Rigidbody>().AddForce(_t.forward * forwardSpeed);
        StartCoroutine("ShootTimeout");
    }

    private IEnumerator ShootTimeout()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}