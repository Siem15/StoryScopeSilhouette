using UnityEngine;

/// <summary>
/// This script is currently used only by the fish object in the scene, 
/// to create the flocking effect of multiple actors moving around an area reacting to one another's postion, direction and distance. 
/// This script's variables should be changed around to make sure that it can be used for other objects as well.
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>

public class Flock : MonoBehaviour
{
    public Vector2 speedRange;
    float Speed;
    readonly float RotationSpeed = 4.0f;
    readonly float NeighbourDistance = 2.0f;
    bool Turning = false;

    private void Start()
    {
        Speed = UnityEngine.Random.Range(speedRange.x, speedRange.y);
    }

    private void Update()
    {
        Vector3 goalPosition = GlobalFlock.GoalPos;
        Turning = Vector3.Distance(transform.position, goalPosition) >= GlobalFlock.tankSize;

        if (Turning)
        {
            Vector3 direction = goalPosition - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 
                RotationSpeed * Time.deltaTime);
            Speed = UnityEngine.Random.Range(speedRange.x, speedRange.y);
        }
        else
        {
            if (UnityEngine.Random.Range(0, 5) < 1)
            {
                ApplyRules();
            }
        }

        transform.Translate(0, 0, Time.deltaTime * Speed);
    }

    private void ApplyRules()
    {
        Vector3 goalPosition = GlobalFlock.GoalPos;

        GameObject[] fishes; // gos  = fishes 
        fishes = GlobalFlock.AllFish;

        Vector3 groupCentre = goalPosition; //group center
        Vector3 groupAvoid = Vector3.zero; // avoid group
        float groupSpeed = 0.1f;

        float distance;
        int groupSize = 0;

        foreach (GameObject fish in fishes) // fish in fishes
        {
            if (fish != gameObject)
            {
                distance = Vector3.Distance(fish.transform.position, transform.position);
                if (distance <= NeighbourDistance)
                {
                    groupCentre += fish.transform.position;
                    groupSize++;
                    if (distance < 1.0f)
                    {
                        groupAvoid += (transform.position - fish.transform.position);
                    }

                    Flock anotherFlock = fish.GetComponent<Flock>();
                    groupSpeed += anotherFlock.Speed;
                }
            }
        }

        if (groupSize > 0)
        {
            groupCentre = groupCentre / groupSize + (goalPosition - transform.position);
            Speed = groupSpeed / groupSize;

            Vector3 direction = (groupCentre + groupAvoid) - transform.position;

            if (direction != goalPosition)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), RotationSpeed * Time.deltaTime);
            }
        }
    }
}