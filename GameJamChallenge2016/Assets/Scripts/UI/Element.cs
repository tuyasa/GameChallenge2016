using UnityEngine;
using System.Collections;

[System.Serializable]
public class Element
{
    [SerializeField]
    private string m_elementName;
    public string ElementName { get { return m_elementName; } }

    [SerializeField]
    private Sprite m_elementSprite;
    public Sprite ElementSprite { get { return m_elementSprite; } }
}