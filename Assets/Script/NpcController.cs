using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{

    NavMeshAgent nav;
    GameObject target;

    [SerializeField] float m_angle = 0f;
    [SerializeField] float m_distance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Tower");
    }

    // Update is called once per frame
    void Update()
    {
        Tower();
    }

    void Tower()
    {
        if (nav.destination != target.transform.position)
        {
            nav.SetDestination(target.transform.position);
        }
        else
        {
            nav.SetDestination(transform.position);
        }
    }
}
