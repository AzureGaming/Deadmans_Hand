using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour {
	private GameController gameController;

	void Start() {
		gameController = GetComponent<GameController>();
	}

	public void GoToLocation(Location destination) {
		gameController.SetLocation(destination);
	}
}
