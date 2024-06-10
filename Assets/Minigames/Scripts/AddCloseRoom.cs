using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCloseRoom : MonoBehaviour
{

	private RoomTemplates templates;

	void Start()
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.closeRooms.Add(this.gameObject);
	}
}
