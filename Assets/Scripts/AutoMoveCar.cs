using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMoveCar : MonoBehaviour {

    public bool moveback = false;
    public float moveDistance = 2000f;
    public float existTime = 10.0f;
    public float moveSpeed = 50.0f;

    [Tooltip("The default speed to avoid collision")]
    public float AutoCarCollisionAvoidSpeed = 55.0f;
    public float PlayerCarCollisionAvoidSpeedDiff = 10.0f;

    [SerializeField] private Vector3 initPos;
    [SerializeField] private Vector3 tarPos;

    private NavMeshAgent agent;
    private Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        tarPos =  Vector3.zero;
        agent = GetComponent<NavMeshAgent>();
        initSetUp();
        //Destroy(gameObject, existTime);
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    public void initSetUp()
    {
        Debug.Log(tarPos);
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(tarPos);
        //setTarget(tarPos);
        agent.speed = moveSpeed;
        ///to Implement
    }

    public void setTarget(Vector3 tar)
    {
        tarPos = tar;
        agent.SetDestination(tarPos);

    }

    public void SetSpeed(float MPHSpeed)
    {
        moveSpeed = MPHSpeed * 0.44704f;
        initSetUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnedCar"))
        {
            SetSpeed(AutoCarCollisionAvoidSpeed);
        }
        else if (other.CompareTag("Player"))
        {
            if (other.transform.position.z - transform.position.z > 0 && rb.velocity.z > 0)
            {
                SetSpeed(other.gameObject.GetComponentInChildren<DataRecorder>().getSpeed() - PlayerCarCollisionAvoidSpeedDiff);
            }
            else if (other.transform.position.z - transform.position.z > 0 && rb.velocity.z < 0)
            {
                SetSpeed(other.gameObject.GetComponentInChildren<DataRecorder>().getSpeed() + PlayerCarCollisionAvoidSpeedDiff);
            }
            else if (other.transform.position.z - transform.position.z < 0 && rb.velocity.z < 0)
            {
                SetSpeed(other.gameObject.GetComponentInChildren<DataRecorder>().getSpeed() - PlayerCarCollisionAvoidSpeedDiff);
            }
            else if (other.transform.position.z - transform.position.z < 0 && rb.velocity.z > 0)
            {
                SetSpeed(other.gameObject.GetComponentInChildren<DataRecorder>().getSpeed() + PlayerCarCollisionAvoidSpeedDiff);
            }
        }
    }


}
