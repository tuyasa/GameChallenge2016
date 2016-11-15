using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    [SerializeField]
    float m_Speed = 100;
    //[SerializeField]
    //float m_SprintSpeed = 200;
    [SerializeField]
    float m_RotateSpeed = 500;

    Rigidbody m_Rg = null;
    Quaternion m_TargetRotation;

    void Start()
    {
        m_Rg = GetComponent<Rigidbody>();

        m_TargetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 dirToGo)
    {
        m_Rg.MovePosition(transform.position + dirToGo.normalized * m_Speed * Time.deltaTime);
    }
 
    public void Rotate(Vector3 rotate)
    {
        //transform.Rotate(rotate * m_RotateSpeed * Time.deltaTime);
        m_TargetRotation *= Quaternion.AngleAxis( rotate.y * m_RotateSpeed, Vector3.up);
        transform.rotation = m_TargetRotation;
    }


}
