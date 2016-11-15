using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{

    [SerializeField]
    Transform m_Target1 = null;
    [SerializeField]
    Transform m_Target2 = null;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = m_Target1.position;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = m_Target2.position;
        }

	}
}
