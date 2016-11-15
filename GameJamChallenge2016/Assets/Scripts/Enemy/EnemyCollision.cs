using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

    [SerializeField]
    string enemyRoom;

    void OnCollisionEnter (Collision _col)
    {
        if(_col.gameObject.GetComponent<Controller>())
        {
            Controller _player = _col.gameObject.GetComponent<Controller>();
            _player.Respawn(_player.PlayerID, enemyRoom);
        }
    }

    void Update ()
    {

    }
}
