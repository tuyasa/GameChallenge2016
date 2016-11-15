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

	bool isTriggerLeft1 = false;
	bool isTriggerLeft2 = false;
	bool isTriggerRight1 = false;
	bool isTriggerRight2 = false;

	const int playerNumber = 2;

	SpellCast[] castedSpells;

	[System.Serializable]
	public class TypeSpells
	{
		public SpellType type;
		public int id;
		public string input;
	}

	public enum SpellType
	{
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

	public List<TypeSpells> typeSpells = new List<TypeSpells> ();

	public List<string> availableInputs = new List<string> ();

	List<TypeSpells> attackSpells = new List<TypeSpells>();
	List<TypeSpells> defenseSpells = new List<TypeSpells>();
	List<TypeSpells> environmentSpells = new List<TypeSpells>();

	//Just for debug
//	public List<string> viewInputsJ1 = new List<string> ();
//	public List<string> viewInputsJ2 = new List<string> ();
//

	void Awake ()
	{
		//fetch special spells
		foreach (var item in typeSpells) {

			if(item.type==SpellType.Attack)
				attackSpells.Add(item);
			else if(item.type==SpellType.Defense)
				defenseSpells.Add(item);
			else if(item.type==SpellType.Environment)
				environmentSpells.Add(item);
		}
		castedSpells = new SpellCast[playerNumber];
		for (int i = 0; i < playerNumber; i++) {			
			castedSpells [i].pressedInputs = new Stack<string> ();
		}
	}

	void Update ()
	{
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("player " + i + 1 + castedSpells[i].isCasting);
        }
		if (Input.GetAxisRaw ("Left Trigger 1") != 0 && !isTriggerLeft1) {
			isTriggerLeft1 = true;
			CastSpell (1);
		}

		if (Input.GetAxisRaw ("Left Trigger 1") == 0) {
			isTriggerLeft1 = false;
		}

		if (Input.GetAxisRaw ("Left Trigger 2") != 0 && !isTriggerLeft2) {
			isTriggerLeft2 = true;
			CastSpell (2);
		}

		if (Input.GetAxisRaw ("Left Trigger 2") == 0) {
			isTriggerLeft2 = false;
		}

		//		viewInputsJ1 = castedSpells [0].pressedInputs.ToList ();	
		//		viewInputsJ2 = castedSpells [1].pressedInputs.ToList ();	
	}
   

	public void CastSpell (int playerId)
	{
		if (!castedSpells [playerId - 1].isCasting) {
			GenerateStartingElements (playerId, typeSpells [Random.Range (0, 9)]);
			StartCoroutine ("CastSpellCor", playerId);
		}
	}

//	public void CastSpell (int playerId, SpellType type)
//	{
//		if (!castedSpells [playerId - 1].isCasting) {
////			GenerateStartingElements (playerId,);
//			if(type == SpellType.Attack) {
//				StartCoroutine ("CastSpellCor", attackSpells[Random.Range(0, attackSpells.Count-1)] );
//				
//			}else if(type == SpellType.Defense) {
//				StartCoroutine ("CastSpellCor", defenseSpells[Random.Range(0, defenseSpells.Count-1)] );
//				
//			}else if(type == SpellType.Environment) {
//				StartCoroutine ("CastSpellCor", environmentSpells[Random.Range(0, environmentSpells.Count-1)] );
//			}
//		}
//	}

	IEnumerator CastSpellCor (int playerId)
	{
        castedSpells [playerId - 1].isCasting = true;
		float t = 0f;
		float rate = 1 / castTime;

		while (t < 1) {
			t += Time.deltaTime * rate;
            //Debug.Log("time : " + t +" player"+playerId);
			foreach (string input in availableInputs) {
				if (Input.GetButtonDown (input.ToString () + playerId) && castedSpells[playerId - 1].pressedInputs.Count < maxElement) {
                    Debug.Log("player" + playerId);
                    castedSpells[playerId - 1].pressedInputs.Push (input);
					elementsUI.AddToElementList (elementsUI.ElementToImage (input.ToString ()), playerId);
				}
			}

			//Case player one
			if (playerId == 1) {
				if (Input.GetAxisRaw ("Right Trigger " + playerId.ToString ()) != 0 && castedSpells[playerId - 1].pressedInputs.Count > minElement && !isTriggerRight1) {
					isTriggerRight1 = true;
                    castedSpells[playerId - 1].pressedInputs.Pop ();
					elementsUI.ClearLastElement (playerId);
				}
				if (Input.GetAxisRaw ("Right Trigger " + playerId.ToString ()) == 0)
					isTriggerRight1 = false;
			} else if (playerId == 2) { //Case player two
				if (Input.GetAxisRaw ("Right Trigger " + playerId.ToString ()) != 0 && castedSpells[playerId - 1].pressedInputs.Count > minElement && !isTriggerRight2) {
					isTriggerRight2 = true;

                    castedSpells[playerId - 1].pressedInputs.Pop ();
					elementsUI.ClearLastElement (playerId);
				}
				if (Input.GetAxisRaw ("Right Trigger " + playerId.ToString ()) == 0)
					isTriggerRight2 = false;
			}
			//Update view

			yield return null;
		}
        //We've done casting so clear the view and the model
        castedSpells[playerId - 1].isCasting = false;
        castedSpells[playerId - 1].pressedInputs.Clear ();
		elementsUI.ClearElementList (playerId);
	}

	public void GenerateStartingElements (int playerId, TypeSpells spell)
	{
		string randomSpell = spell.input.Substring (0, minElement);
		for (int i = 0; i < minElement; i++) {			
			castedSpells [playerId - 1].pressedInputs.Push (randomSpell [i].ToString ());
			Element element = elementsUI.ElementToImage (randomSpell [i].ToString ());
			elementsUI.AddToElementList (element, playerId);
		}
	}

	public void ClearPlayerInput (int playerId)
	{
		castedSpells [playerId - 1].pressedInputs.Clear ();
	}
}