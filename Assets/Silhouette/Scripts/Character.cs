using UnityEngine;
/// <summary>
/// 
/// </summary>
public class Character : MonoBehaviour
{
    public float WalkSpeed, RunSpeed;
    [Min(0.1f)]
    public float stopRunningUntilThisDist;
    public bool singleSprite, controlRotation, controlPosition, facingRight;
    public GameObject endMarkerPrefab;
    string check;
    float speed, distance;
    float distanceToMove = 0.5f;
    GameObject endMarker;
    bool idle;
    Animator animator;
    FiducialController endMarkerFC, fidu;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (singleSprite) spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        endMarker = SetEndMarker();
        fidu = GetComponent<FiducialController>();
    }

    private void Update()
    {
        if (/*fidu.m_IsVisible &&*/ animator == null) GetAnimator();
        CheckGrounded();
        Flip();
    }

    private void CheckGrounded()
    {
        if (Vector3.Distance(transform.position, endMarker.transform.position) > distanceToMove) idle = false;
        if (transform.position == endMarker.transform.position) idle = true;
        if (!idle) transform.position = Vector3.MoveTowards(transform.position, endMarker.transform.position, speed * Time.deltaTime); //move to target

        SetAnimation(idle);
    }

    private void SetAnimation(bool idle)
    {
        distance = Vector3.Distance(transform.position, endMarker.transform.position);

        if (idle)
        {
            SetAnimationAndSpeed("idle", 0f);
            return;
        }

        if (distance > 0.1f && distance < stopRunningUntilThisDist) SetAnimationAndSpeed("walk", WalkSpeed);
        else if (distance > stopRunningUntilThisDist) SetAnimationAndSpeed("run", RunSpeed);
    }

    private void GetAnimator()
    {
        if (animator == null)
        {
            if (GetComponentInChildren<Animator>()) animator = GetComponentInChildren<Animator>();
            else animator = GetComponent<Animator>();
        }
    }

    private GameObject SetEndMarker()
    {
        GameObject tempEndMarker = Instantiate(endMarkerPrefab);
        tempEndMarker.name = gameObject.name + "_MARKER";
        endMarkerFC = tempEndMarker.GetComponent<FiducialController>();
        endMarkerFC.MarkerID = GetComponentInParent<FiducialController>().MarkerID;
        endMarkerFC.IsRotationMapped = controlRotation;
        endMarkerFC.IsPositionMapped = controlPosition;
        return tempEndMarker;
    }

    public void SetAnimationAndSpeed(string animation, float setSpeed)
    {
        if (animation != check)
        {
            speed = setSpeed;
            if (!animator) return;
            /*if (fidu.m_IsVisible)*/ animator.Play(animation);
            check = animation;
        }
    }

    private void Flip()
    {
        if (endMarker.transform.position.x - 0.05f >= transform.position.x && facingRight) Rotate();
        else if (endMarker.transform.position.x + 0.05f <= transform.position.x && !facingRight) Rotate();
    }

    void Rotate()
    {
        if (singleSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            return;
        }

        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, +180, 0));
    }
}