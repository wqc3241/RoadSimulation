using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMoveCar : MonoBehaviour {

    public bool moveback = false;
    public float moveDistance = 2000f;
    public float existTime = 10.0f;
    public float moveSpeed = 50.0f;
    [SerializeField] private Vector3 initPos;
    [SerializeField] private Vector3 tarPos;

    private NavMeshAgent agent;
    // Use this for initialization
    void Start ()
    {
        initPos = transform.position;
        tarPos = initPos + moveDistance * transform.forward;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(tarPos);
        agent.speed = moveSpeed;
        Destroy(gameObject, existTime);
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    public void initSetUp(float Dis, float time, float speed)
    {
        ///to Implement
    }
}
