using UnityEngine;

/// <summary>
/// This script is supposed to be used by the vase, only the vase also currently does not use this script.
/// It used partner to synchronize the transform of the vase with a certain particle system.
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>

public class Distance : MonoBehaviour
{
    Transform partner = null;
    //public Transform hearts;
    public ParticleSystem ps;
    public float DistanceOfLove;
    ParticleSystem.EmissionModule em;
    float dist;

    // Use this for initialization
    void Start()
    {
        em = ps.emission;
        partner = GameObject.Find("PlantVase(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (partner != null)
        {
            dist = Vector3.Distance(partner.position, transform.position);
            //hearts.position = partner.position +(transform.position -partner.position) / 2 + new Vector3(0,3,0); 
            if (dist < DistanceOfLove)
            {
                em.enabled = true;
            }
            else
            {
                em.enabled = false;
            }
        }
    }
}