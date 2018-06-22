using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMoveCar : MonoBehaviour {

    [SerializeField]
    public GameObject DestinationList;

    int cur = 0;

    public bool moveback = false;
    public float moveDistance = 2000f;
    public float existTime = 10.0f;
    private float moveSpeed = 50.0f;

    [Tooltip("The default speed to avoid collision")]
    public float AutoCarCollisionAvoidSpeed = 55.0f;
    public float PlayerCarCollisionAvoidSpeedDiff = 10.0f;

    [SerializeField] private Vector3 initPos;
    [SerializeField] private Vector3 tarPos;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private Transform body;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        body = transform.GetChild(1);

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        body.GetComponent<Renderer>().material.color = newColor;

        initPos = transform.position;
        tarPos = DestinationList.transform.GetChild(cur).position;
        //Debug.Log(tarPos);
        agent = this.GetComponent<NavMeshAgent>();
        initSetUp();
        //Destroy(gameObject, existTime);
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(DestinationList.transform.GetChild(cur));
        if ( Vector3.Distance(agent.transform.position, DestinationList.transform.GetChild(cur).transform.position)< 5)
        {
            cur++;

            if (cur == DestinationList.transform.childCount)
            {
                cur = 0;
            }

            agent.SetDestination(DestinationList.transform.GetChild(cur).transform.position);
            //Debug.Log(DestinationList.transform.GetChild(cur));
        }

        //Debug.Log(this.tarPos);
    }

    public void initSetUp()
    {
        //Debug.Log(tarPos);
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(tarPos);
        setTarget(tarPos);
        moveSpeed = Random.Range(40.0f, 80.0f);

        agent.speed = moveSpeed;
        ///to Implement
    }

    public void setTarget(Vector3 tar)
    {
        agent.SetDestination(DestinationList.transform.GetChild(cur).transform.position);
    }

    public void SetSpeed(float MPHSpeed)
    {
        moveSpeed = MPHSpeed * 0.44704f;
        initSetUp();
    }

 /*   public void setColor(Color color)
    {

    }
    */
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
