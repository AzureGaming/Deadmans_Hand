using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Action")]
public class Action : ScriptableObject {
	public string actionName;
	public Scenario result;
	public Scenario success;
	public Scenario failure;
	public bool heat;
	public bool weapon;
	public int dice;

}