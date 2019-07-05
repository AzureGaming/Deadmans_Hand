using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Action")]
public class Action : ScriptableObject {
	public string actionName;
	public List<Outcome> outcomes = new List<Outcome>();
	public bool heat;
	public bool weapon;
	public int dice;

}