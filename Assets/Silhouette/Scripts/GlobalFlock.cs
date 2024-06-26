﻿using UnityEngine;

/// <summary>
/// This script is currently used only by the fish object in the scene, 
/// That establishes how many actors are in the flock, which sprite they use and instantiates them.
/// The script also uses the fiducial position to create a center for the flock to move around. 
/// This script's variables should be changed around to make sure that it can be used for other objects as well.
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>
public class GlobalFlock : MonoBehaviour
{
    public GameObject FishPrefab;
    public int NumFish = 50;
    public int fiduID;
    public static GameObject[] AllFish;
    public static int tankSize = 5; //TODO screenwithd?
    public GameObject GoalPrefab;
    public static Vector3 GoalPos = Vector3.zero;
    public bool fish = true;
    public float butterflySize = 1.0f;
    Vector2 CamWorldSize;

    private void Start()
    {
        GameObject FishTarget = Instantiate(GoalPrefab);
        GoalPrefab = FishTarget;
        SphereCollider sc = FishTarget.AddComponent<SphereCollider>() as SphereCollider;
        sc.radius = 0.5f;
        FishTarget.GetComponent<FiducialController>().MarkerID = fiduID;
        FishTarget.name = "FishTarget_39";

        AllFish = new GameObject[NumFish];
        CamWorldSize.y = Camera.main.orthographicSize;
        CamWorldSize.x = Camera.main.orthographicSize * Camera.main.aspect;
        // print("y " + CamWorldSize.y); //7
        // print("x " + CamWorldSize.x); //14

        for (int i = 0; i < NumFish; i++)
        {
            Vector3 Pos = new Vector3(Random.Range(-CamWorldSize.x, CamWorldSize.x), Random.Range(0, CamWorldSize.y * 2), Random.Range(-2, 2));
            AllFish[i] = Instantiate(FishPrefab, Pos, Quaternion.identity);
            if (fish)
            {
                AllFish[i].transform.localScale = new Vector3(Random.Range(2, 4), Random.Range(2, 4), Random.Range(2, 4));
                AllFish[i].transform.parent = transform;
            }
            else
            {
                AllFish[i].transform.localScale = new Vector3(butterflySize, butterflySize, butterflySize);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        GoalPos = GoalPrefab.transform.position;
        //if (Random.Range(0, 10000) < 50) 
        //{
        //    GoalPos = new Vector3(Random.Range(-CamWorldSize.x, CamWorldSize.x), Random.Range(0, CamWorldSize.y * 2), Random.Range(-2, 2));
        //    GoalPrefab.transform.position = GoalPos;    
        //}
    }
}