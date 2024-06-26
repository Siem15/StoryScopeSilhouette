using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	public float timeAlive;

    private void Start()
    {
        timeAlive += Time.deltaTime;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //check who is longer alive and destroy the newest target
        if (other.CompareTag("Destroyer") && other.GetComponent<Destroyer>().timeAlive < timeAlive)
        {
            Debug.Log(other);
            Destroy(other.transform.parent.gameObject);
        }
	}
}
