using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    [SerializeField] int CurrentRoom = 1;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    private bool spawnedDoor;
    private bool spawnedKey;
    public GameObject boss;
    public GameObject door;
    public GameObject key;


    void Update()
    {

        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    checkDoor(i);
                    int keyRoom = Random.Range(0, i - (CurrentRoom+1));
                    Instantiate(key, rooms[keyRoom].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    spawnedDoor = true;
                    spawnedKey = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void checkDoor(int i)
    {
        if (Vector3.Distance(rooms[i].transform.position, rooms[i - CurrentRoom].transform.position) > 1.1f)
        {
            Debug.Log(Vector3.Distance(rooms[i].transform.position, rooms[i - CurrentRoom].transform.position));
            CurrentRoom++;
            checkDoor(i);
        }
        else
        {
            Debug.Log(Vector3.Distance(rooms[i].transform.position, rooms[i - CurrentRoom].transform.position));
            Instantiate(door, rooms[i - CurrentRoom].transform.position, Quaternion.identity);
        }
    }
}
