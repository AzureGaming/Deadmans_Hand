using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Action")]
public class Action : ScriptableObject {
	public string actionName;

	public string formula;
	public Scenario result;

	public Scenario success;
	public Scenario failure;
}