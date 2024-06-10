using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockKeyBoss : MonoBehaviour
{
	private RoomTemplates templates;

	void Start()
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            if(this.gameObject.name == "Key(Clone)")
            {
                Debug.Log("Player" + this.gameObject);
                templates.spawnedKey = false;
                this.gameObject.SetActive(false);
            }

            if (this.gameObject.name == "Door(Clone)" && templates.spawnedKey == false)
            {
                Debug.Log("Player" + this.gameObject);
                this.gameObject.SetActive(false);
            }

            if (this.gameObject.name == "Boss(Clone)")
            {
                Debug.Log("Player" + this.gameObject);
                templates.resetbutton = true;
            }
        }
    }
}
