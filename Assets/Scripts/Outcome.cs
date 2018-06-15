using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DMH/Outcome")]
public class Outcome : ScriptableObject {
	public string description;
	public Location resultDestination;

	public string success;
	public string failure;

	public string effect;
}
