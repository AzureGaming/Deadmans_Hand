using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Action")]
public class Action : ScriptableObject {
	public string actionName;
	public Outcome outcome;
	public string formula;
}
