using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour {

    private NavMeshAgent agent;

    [SerializeField]
    float lookRadius = 5f;

    private Vector3 stockedPosition;
    private Vector3 targetPosition;

    private Controller[] players;

    private Vector3 player1Pos;
    private float player1Distance;

    private Vector3 player2Pos;
    private float player2Distance;



    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        stockedPosition = transform.position;
        targetPosition = stockedPosition;
        players = FindObjectsOfType<Controller>();
    }

    void Update ()
    {
        GetPlayerDistance();
        ChasePlayers();
    }

    void GetPlayerDistance ()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].PlayerID == "1")
            {
                player1Pos = players[i].transform.position;
            }
            else if (players[i].PlayerID == "2")
            {
                player2Pos = players[i].transform.position;
            }
        }

        player1Distance = Vector3.Distance(transform.position, player1Pos);
        player2Distance = Vector3.Distance(transform.position, player2Pos);
    }

    void ChasePlayers ()
    {
        if (Vector3.Distance(transform.position, stockedPosition) > lookRadius)
        {
            targetPosition = stockedPosition;
        }

        if (player1Distance < player2Distance && player1Distance < lookRadius)
        {
            targetPosition = player1Pos;
        }

        if(player2Distance < player1Distance && player2Distance < lookRadius)
        {
            targetPosition = player2Pos;
        }

        agent.SetDestination(targetPosition);
    }

    void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere(targetPosition, 1);
    }
}
