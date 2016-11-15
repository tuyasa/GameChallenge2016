using UnityEngine;
using System.Collections;

public class Rooms : MonoBehaviour {

    public static Rooms singleton;

    [SerializeField]
    Transform spawnR1P1;
    public Transform SpawnR1P1 { get { return spawnR1P1; } }
    [SerializeField]
    Transform spawnR1P2;
    public Transform SpawnR1P2 { get { return spawnR1P2; } }

    [SerializeField]
    Transform spawnR2P1;
    public Transform SpawnR2P1 { get { return spawnR2P1; } }
    [SerializeField]
    Transform spawnR2P2;
    public Transform SpawnR2P2 { get { return spawnR2P2; } }

    [SerializeField]
    Transform spawnR3P1;
    public Transform SpawnR3P1 { get { return spawnR3P1; } }
    [SerializeField]
    Transform spawnR3P2;
    public Transform SpawnR3P2 { get { return spawnR3P2; } }

    void Awake ()
    {
        if (singleton != null)
            return;
        else
            singleton = this;
    }
}
