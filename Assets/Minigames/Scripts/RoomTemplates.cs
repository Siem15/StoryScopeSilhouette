using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public bool resetbutton = false;
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    [SerializeField] int CurrentRoom = 1;

    public GameObject closedRoom;
    public GameObject StartRoom;

    public List<GameObject> rooms;
    public List<GameObject> closeRooms;

    public float waitTime;
    public float waitTimeReset;
    private bool spawnedBoss;
    private bool spawnedDoor;
    private bool spawnedKey;
    public GameObject boss;
    public GameObject door;
    public GameObject key;

    private void Start()
    {
        waitTimeReset = waitTime;
    }

    void Update()
    {
        if (resetbutton && Input.GetMouseButton(0))
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0f;
            Resett();
            Instantiate(StartRoom,mouse,Quaternion.identity);
            resetbutton = false;
        }

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

    public void Resett()
    {
        foreach (GameObject item in rooms)
        {
            Destroy(item);
        }
        foreach (GameObject item in closeRooms)
        {
            Destroy(item);
        }

        rooms.Clear();
        closeRooms.Clear();

        CurrentRoom = 1;
        waitTime = waitTimeReset;

        Destroy(GameObject.Find(door.name+"(Clone)"));
        Destroy(GameObject.Find(boss.name + "(Clone)"));
        Destroy(GameObject.Find(key.name + "(Clone)"));

        spawnedBoss = false;
        spawnedDoor = false;
        spawnedKey = false;
    }
}
