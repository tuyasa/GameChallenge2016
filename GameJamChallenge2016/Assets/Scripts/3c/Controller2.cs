using UnityEngine;
using System.Collections;

public class Controller2 : MonoBehaviour {


    [SerializeField]
    Character m_Chara = null;


    Vector3 m_DirToGo1 = new Vector3();
    Vector3 m_Rotate1 = new Vector3();

    // Update is called once per frame
    void Update()
    {
        m_DirToGo1 = Vector3.zero;
        m_Rotate1 = Vector3.zero;

        if (Input.GetAxisRaw("Right X Axis 2") != 0)
        {
            Debug.Log("Right X Axis 2");
            m_Rotate1 = new Vector3(0, Input.GetAxisRaw("Right X Axis 2"), 0);
        }

        if (Input.GetAxisRaw("Left Y Axis 2") != 0)
        {
            m_DirToGo1 += -transform.forward * Input.GetAxisRaw("Left Y Axis 2");
        }
        if (Input.GetAxisRaw("Left X Axis 2") != 0)
        {
            m_DirToGo1 += transform.right * Input.GetAxisRaw("Left X Axis 2");
        }


        m_Chara.Move(m_DirToGo1);
        m_Chara.Rotate(m_Rotate1);
    }
}
