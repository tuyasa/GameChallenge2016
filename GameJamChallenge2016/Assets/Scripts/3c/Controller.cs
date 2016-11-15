using UnityEngine;
using System.Collections;


public class Controller : MonoBehaviour
{

    [SerializeField]
    Character m_Chara = null;
    [SerializeField]
    string m_PlayerID;
    public string PlayerID { get { return m_PlayerID; } }

    Vector3 m_DirToGo1 = new Vector3();
    Vector3 m_Rotate1 = new Vector3();

    // Update is called once per frame
    void Update()
    {
        m_DirToGo1 = Vector3.zero;
        m_Rotate1 = Vector3.zero;

        if (Input.GetAxisRaw("Right X Axis "+ m_PlayerID) != 0)
        {
            Debug.Log("Right X Axis " + m_PlayerID);
            m_Rotate1 = new Vector3(0, Input.GetAxisRaw("Right X Axis " + m_PlayerID), 0);
        }

        if (Input.GetAxisRaw("Left Y Axis " + m_PlayerID) != 0)
        {
            m_DirToGo1 += new Vector3(m_DirToGo1.x, 0, -Input.GetAxisRaw("Left Y Axis " + m_PlayerID));
        }
        if (Input.GetAxisRaw("Left X Axis " + m_PlayerID) != 0)
        {
            m_DirToGo1 += new Vector3(Input.GetAxisRaw("Left X Axis " + m_PlayerID), 0, m_DirToGo1.z);
        }


        m_Chara.Move(m_DirToGo1);
        m_Chara.Rotate(m_Rotate1);
    }
    
    /// <summary>
    /// Respawns the player at a new location
    /// </summary>
    public void Respawn (string _player, string _room)
    {
        switch (_room)
        {
            case "1":
                if (_player == "1")
                    transform.position = Rooms.singleton.SpawnR1P1.position;
                else
                    transform.position = Rooms.singleton.SpawnR1P2.position;
                break;

            case "2":
                if (_player == "1")
                    transform.position = Rooms.singleton.SpawnR2P1.position;
                else
                    transform.position = Rooms.singleton.SpawnR2P2.position;
                break;

            case "3":
                if (_player == "1")
                    transform.position = Rooms.singleton.SpawnR3P1.position;
                else
                    transform.position = Rooms.singleton.SpawnR3P2.position;
                break;
        }
    }
}
