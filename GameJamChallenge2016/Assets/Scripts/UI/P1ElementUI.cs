using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class P1ElementUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> m_imageListP1;

    [SerializeField]
    private List<Image> m_imageListP2;

    [SerializeField]
    private int m_maxElement = 6;

	[SerializeField]
    	private Element[] m_elementTemplate;
	
    	public Element[] ElementTemplate { get { return m_elementTemplate; } }



    public List<Element> m_elementListP1 = new List<Element>();
    /// <summary>
    /// List of all player 1 elements currently in the bar
    /// </summary>
    public List<Element> ElementListP1 { get { return m_elementListP1; } }

    public List<Element> m_elementListP2 = new List<Element>();
    /// <summary>
    /// List of all player 2 elements currently in the bar
    /// </summary>
    public List<Element> ElementListP2 { get { return m_elementListP2; } }


    void Update ()
    {
//        TestInputs();
    }

    /// <summary>
    /// Used for testing methods below
    /// </summary>
    private void TestInputs ()
    {
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            AddToElementList(m_elementTemplate[0], 1);
//        }
//
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            AddToElementList(m_elementTemplate[1], 1);
//        }
//
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            AddToElementList(m_elementTemplate[2], 1);
//        }
//
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            ClearLastElement(1);
//        }
//
//        if (Input.GetKeyDown(KeyCode.T))
//        {
//            ClearElementList(1);
//        }
//
//
//
//
//
//        if (Input.GetKeyDown(KeyCode.Keypad1))
//        {
//            AddToElementList(m_elementTemplate[0], 2);
//        }
//
//        if (Input.GetKeyDown(KeyCode.Keypad2))
//        {
//            AddToElementList(m_elementTemplate[1], 2);
//        }
//
//        if (Input.GetKeyDown(KeyCode.Keypad3))
//        {
//            AddToElementList(m_elementTemplate[2], 2);
//        }

//        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
//        {
//            ClearLastElement(2);
//        }
//
//        if (Input.GetKeyDown(KeyCode.Keypad0))
//        {
//            ClearElementList(2);
//        }
    }

    /// <summary>
    /// Updates the list of all UI images
    /// </summary>
    private void UpdateImageList (int _player1or2)
    {
        if (_player1or2 == 1)
        {
            for (int i = 0; i < m_elementListP1.Count; i++)
            {
                m_imageListP1[i].enabled = true;
                m_imageListP1[i].sprite = m_elementListP1[i].ElementSprite;
            }

            if (m_elementListP1.Count < m_maxElement)
            {
                for (int i = m_elementListP1.Count; i < m_maxElement; i++)
                {
                    m_imageListP1[i].enabled = false;
                }
            }
        }
        else if (_player1or2 == 2)
        {
            for (int i = 0; i < m_elementListP2.Count; i++)
            {
                m_imageListP2[i].enabled = true;
                m_imageListP2[i].sprite = m_elementListP2[i].ElementSprite;
            }

            if (m_elementListP2.Count < m_maxElement)
            {
                for (int i = m_elementListP2.Count; i < m_maxElement; i++)
                {
                    m_imageListP2[i].enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Add an element to a player element UI list
    /// </summary>
    /// <param name="_element">The new Element</param>
    /// <param name="_player1or2">Which player to add</param>
    public void AddToElementList (Element _element, int _player1or2)
    {
        if (_player1or2 == 1 && m_elementListP1.Count < m_maxElement)
            m_elementListP1.Add(_element);
        else if (_player1or2 == 2 && m_elementListP2.Count < m_maxElement)
            m_elementListP2.Add(_element);

        UpdateImageList(_player1or2);
    }

    /// <summary>
    /// Clears the UI list of images
    /// </summary>
    /// <param name="_player1or2">Which player to clear the list?</param>
    public void ClearElementList (int _player1or2)
    {
        if (_player1or2 == 1)
            m_elementListP1.Clear();
        else if (_player1or2 == 2)
            m_elementListP2.Clear();

        UpdateImageList(_player1or2);
    }

    /// <summary>
    /// Clears the last element of the UI list of images
    /// </summary>
    /// <param name="_player1or2">Which player to clear the last element?</param>
    public void ClearLastElement (int _player1or2)
    {
        int count;

        if (_player1or2 == 1 && m_elementListP1.Count > 0)
        {
            count = m_elementListP1.Count - 1;
            m_elementListP1.RemoveAt(count);
        }
        else if(_player1or2 == 2 && m_elementListP2.Count > 0)
        {
            count = m_elementListP2.Count - 1;
            m_elementListP2.RemoveAt(count);
        }

        UpdateImageList(_player1or2);
    }

    public Element ElementToImage(string name) {
    	
    	if(name.Equals("A"))
			return m_elementTemplate[0];
	else if(name.Equals("B"))
			return m_elementTemplate[1];
	else if(name.Equals("X"))
			return m_elementTemplate[2];
	else if(name.Equals("Y"))
			return m_elementTemplate[3];
	else 
		return null;
    }
}
