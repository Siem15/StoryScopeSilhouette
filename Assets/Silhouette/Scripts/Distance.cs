using UnityEngine;

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