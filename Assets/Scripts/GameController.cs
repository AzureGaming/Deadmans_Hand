using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public InputField inputField;
	public Location location;

	public Dictionary<string, Outcome> dictionary = new Dictionary<string, Outcome>();
	private Navigator navigator;
	private HistoryLog historyLog;

	void Start() {
		historyLog = GameObject.Find("HistoryLog").GetComponent<HistoryLog>();
		inputField = GameObject.Find("InputField").GetComponent<InputField>();
		navigator = GetComponent<Navigator>();

		navigator.GoToLocation(location);
		LoadDictionary(location.actions);
		DisplayActions(location.actions);
		PromptUser();
	}

	public void setLocation(Location destination) {
		location = destination;
		historyLog.AppendText(destination.locationName);
		historyLog.AppendText(destination.description);
	}

	public void PromptUser() {
		inputField.Select();
		inputField.onEndEdit.AddListener(delegate {HandleOnEditEnd(inputField.text);});
	}

	private void HandleOnEditEnd(string input) {
		historyLog.AppendText(input);
		inputField.text = "";

		ActOnChoice(input);

		inputField.onEndEdit.RemoveListener(delegate {HandleOnEditEnd(inputField.text);});
	}

	void ActOnChoice(string input) {
		Outcome outcome;
		bool isValid = dictionary.TryGetValue(input.ToLower(), out outcome);

		if (isValid) {
			// Load outcome
		} else {
			// UpdateHistory("Invalid key in dictionary.");
			PromptUser();
		}
	}

	public void LoadDictionary(List<Action> actions) {
		actions.ForEach(action => {
			Debug.Log("Add name to dictionary:" + action.actionName.ToLower());
			dictionary.Add(action.actionName.ToLower(), action.outcome);
		});
	}

	public void DisplayActions(List<Action> actions) {
		actions.ForEach(action => {
			historyLog.AppendText(action.actionName);
		});
	}
	
	// nextDay()
	// calculate resources ...
	// update history log with location name
	// update histroy log with location description
}
