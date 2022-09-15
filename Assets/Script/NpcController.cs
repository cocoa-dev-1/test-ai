using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{

    NavMeshAgent nav;
    GameObject target;
    [SerializeField] float angle = 0f;
    [SerializeField] float distance = 0f;
    [SerializeField] LayerMask layerMask = 0;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, distance, layerMask);
        if (cols.Length > 0)
        {
            Transform player = cols[0].transform;
            Vector3 direction = (player.position - transform.position).normalized;

            float t_angle = Vector3.Angle(direction, transform.forward);
            if (t_angle < angle * 0.5f)
            {
                Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
                if (Physics.Raycast(transform.forward, direction, out RaycastHit t_hit, distance))
                {
                    Debug.Log("test");
                    if (t_hit.transform.name == "player")
                    {
                        if (nav.destination != t_hit.transform.position)
                        {
                            nav.SetDestination(t_hit.transform.position);
                        }
                        else
                        {
                            nav.SetDestination(transform.position);
                        }
                    }
                }
            }
        }
    }
}
