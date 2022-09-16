using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{

    NavMeshAgent nav;
    GameObject target;

    [SerializeField] bool DebugMode = false;
    [Range(0f, 360f)][SerializeField] float m_angle = 0f;
    [SerializeField] float m_distance = 0f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;
    List<Collider> hitTargetList = new List<Collider>();
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Tower");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        float lookingAngle = transform.eulerAngles.y;  //캐릭터가 바라보는 방향의 각도
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + m_angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - m_angle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);


        // hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, m_distance, TargetMask);

        if (!(Targets.Length == 0))
        {
            Vector3 targetPos = Targets[0].transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= m_angle * 0.5f && Physics.Raycast(myPos, targetDir, out RaycastHit t_hit, m_distance))
            {
                // hitTargetList.Add(EnemyColli);
                if (t_hit.transform.name == "player")
                {
                    target = t_hit.transform.gameObject;
                    if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
                }
            }
        }
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

    void OnDrawGizmos()
    {
        if (!DebugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        float lookingAngle = transform.eulerAngles.y;  //캐릭터가 바라보는 방향의 각도
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + m_angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - m_angle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * m_distance, Color.blue);
        Debug.DrawRay(myPos, leftDir * m_distance, Color.blue);
        Debug.DrawRay(myPos, lookDir * m_distance, Color.cyan);
        Gizmos.DrawWireSphere(myPos, m_distance);
        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, m_distance, TargetMask);

        if (Targets.Length == 0) return;
        foreach (Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= m_angle * 0.5f && !Physics.Raycast(myPos, targetDir, m_distance, ObstacleMask))
            {
                hitTargetList.Add(EnemyColli);
                if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
            }
        }
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
}
