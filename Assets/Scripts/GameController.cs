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

		SetLocation(location);

		inputField.onEndEdit.AddListener(delegate {
			HandleOnEditEnd(inputField.text);
		});
	}

	public void SetLocation(Location destination) {
		location = destination;
		historyLog.AppendText(destination.locationName);
		historyLog.AppendText(destination.description);
		LoadDictionary(destination.actions);
	}

	public void LoadDictionary(List<Action> actions) {
		dictionary.Clear();
		
		actions.ForEach(action => {
			int option = 1;
			dictionary.Add(action.actionName.ToLower(), action.outcome);
			historyLog.AppendText(option.ToString() + ". " + action.actionName);
			option++;
		});
	}

	private void HandleOnEditEnd(string input) {
		historyLog.AppendText(input);
		inputField.text = "";

		if (CheckIsValid(input)) {
			if (dictionary[input.ToLower()].resultDestination) {
				SetLocation(dictionary[input.ToLower()].resultDestination);
			} else {
				historyLog.AppendText(dictionary[input.ToLower()].success);
				historyLog.AppendText(dictionary[input.ToLower()].failure);
			}
		}

		inputField.Select();
	}

	bool CheckIsValid(string input) {
		bool isValid = dictionary.ContainsKey(input.ToLower());

		return isValid ? true : false;
	}
	
	// nextDay()
	// calculate resources ...
	// update history log with location name
	// update histroy log with location description
}
