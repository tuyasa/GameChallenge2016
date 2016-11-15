using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class SpellCastManager : PersistentSingleton<SpellCastManager>
{
	public P1ElementUI elementsUI;
	public int maxElement = 6;
	public int minElement = 3;
	public float castTime = 10f;

    private bool isTriggerLeft1 = false;
    private bool isTriggerLeft2 = false;
    private bool isTriggerRight1 = false;
    private bool isTriggerRight2 = false;

    private const int playerNumber = 2;

	SpellCast[] castedSpells;

	[System.Serializable]
	public class TypeSpells {
		public int id;
		public string input;
	}

	public enum SpellType {
		Attack,
		Defense,
		Environment,
	};

	struct SpellCast
	{
		public int playerId;
		public bool isCasting;
		public Stack<string> pressedInputs;
	};

	public List<TypeSpells> typeSpells = new List<TypeSpells>();

	public List<string> availableInputs = new List<string> ();

	//Just for debug
	public List<string> viewInputsJ1 = new List<string> ();
	public List<string> viewInputsJ2 = new List<string> ();


	void Awake ()
	{
		castedSpells = new SpellCast[playerNumber];
		for (int i = 0; i < playerNumber; i++) {			
			castedSpells [i].pressedInputs = new Stack<string> ();
		}
	}

	void Update ()
	{
		if (Input.GetAxisRaw ("Left Trigger 1") != 0 && !isTriggerLeft1)
        {
            isTriggerLeft1 = true;
			GenerateStartingElements(1, typeSpells[Random.Range(0,9)]);
			CastSpell(1);
		}

        if(Input.GetAxisRaw ("Left Trigger 1") == 0)
        {
            isTriggerLeft1 = false;
        }

		if (Input.GetAxisRaw ("Left Trigger 2") != 0 && !isTriggerLeft2)
        {
            isTriggerRight2 = true;
			GenerateStartingElements(2, typeSpells[Random.Range(0,9)]);
			CastSpell(2);
		}

        if (Input.GetAxisRaw("Left Trigger 2") == 0)
        {
            isTriggerRight2 = false;
        }
        //		viewInputsJ1 = castedSpells [0].pressedInputs.ToList ();	
        //		viewInputsJ2 = castedSpells [1].pressedInputs.ToList ();	
    }

	public void CastSpell(int playerId) {
		if(!castedSpells[playerId-1].isCasting) {
			StartCoroutine("CastSpellCor", playerId);
		}
	}

	IEnumerator CastSpellCor (int playerId)
	{
		SpellCast spell = castedSpells [playerId - 1];
		spell.isCasting = true;
		float t = 0f;
		float rate = 1 / castTime;

		while (t < 1) {
			t += Time.deltaTime * rate;

			foreach (string input in availableInputs) {
				if (Input.GetButtonDown (input.ToString () + playerId) && spell.pressedInputs.Count < maxElement) {
					spell.pressedInputs.Push (input);
					elementsUI.AddToElementList(elementsUI.ElementToImage(input.ToString()), playerId);
				}
			}
			
			if (Input.GetAxisRaw ("Left Trigger "+playerId.ToString()) != 0 && spell.pressedInputs.Count > minElement )
            {
                if (playerId == 1)
                    isTriggerRight1 = true;
				spell.pressedInputs.Pop ();
				elementsUI.ClearLastElement(playerId);
			}

			yield return null;
		}
		//We've done casting so clear the view and the model
		spell.isCasting = false;
		spell.pressedInputs.Clear ();
		elementsUI.ClearElementList(playerId);
	}

	public void GenerateStartingElements(int playerId, TypeSpells spell) {
		string randomSpell = spell.input.Substring(0, minElement);
		Debug.Log(randomSpell);
		for (int i = 0; i < minElement; i++) {			
			castedSpells[playerId-1].pressedInputs.Push(randomSpell[i].ToString());
			Element element = elementsUI.ElementToImage(randomSpell[i].ToString());
			elementsUI.AddToElementList(element, playerId);
		}
	}

	public void ClearPlayerInput (int playerId)
	{
		castedSpells [playerId - 1].pressedInputs.Clear ();
	}
}