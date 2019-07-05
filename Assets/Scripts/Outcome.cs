using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Outcome")]
public class Outcome : ScriptableObject {
	public string outcomeName;
	public string description;
	public List<Action> actions;
}