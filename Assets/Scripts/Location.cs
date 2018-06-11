using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Location")]
public class Location : ScriptableObject {
	public string locationName;
	public string description;
	public List<Action> actions;
}
