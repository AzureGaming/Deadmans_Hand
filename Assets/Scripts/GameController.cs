using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public InputField inputField;

	public Dictionary<string, Scenario> dictionary = new Dictionary<string, Scenario>();
	private Navigator navigator;
	private HistoryLog historyLog;
	private int heat = 0;
	private int weapon = 0;

	[SerializeField] private Scenario startingScenario;

	void Start() {
		historyLog = GameObject.Find("HistoryLog").GetComponent<HistoryLog>();
		inputField = GameObject.Find("InputField").GetComponent<InputField>();
		navigator = GetComponent<Navigator>();

		LoadScenario(startingScenario);

		inputField.onEndEdit.AddListener(delegate {
			HandleOnEditEnd(inputField.text);
		});
	}

	public void LoadScenario(Scenario scenario) {
		historyLog.AppendText(scenario.scenarioName);
		historyLog.AppendText(scenario.description);
		SetDictionary(scenario.actions);
		inputField.Select();
	}

	private Scenario ParseAction(Action action) {
		if (action.result) {
			return action.result;
		}

		if (CalculateOutcome(action)) {
			return action.success;
		} else {
			return action.failure;
		}
	}

	private void SetDictionary(List<Action> actions) {
		int counter = 1;
		
		dictionary.Clear();

		if (actions != null) {
			actions.ForEach((Action action) => {
				string actionToLower = action.actionName.ToLower();
				Scenario parsedAction = ParseAction(action);

				dictionary.Add(actionToLower, parsedAction);
				historyLog.AppendText(counter.ToString() + ". "  + action.actionName);
				counter++;
			});
		}
	}

	private void HandleOnEditEnd(string userInput) {
		string lowerCaseInput = userInput.ToLower();

		historyLog.AppendText(userInput);
		inputField.text = null;

		if (dictionary.ContainsKey(lowerCaseInput)) {
			LoadScenario(dictionary[lowerCaseInput]);
		} else {
			historyLog.AppendText("Invalid input");
			inputField.Select();
		}
	}

	private bool CalculateOutcome(Action action) {
		int stats = CalculateStats(action);
		int dice = action.dice;

		Debug.Log("Calculated player stats for outcome roll: " + stats);

		if (dice != 0) {
			int diceRoll = Random.Range(0, dice);
			int outcome = diceRoll - stats;

			Debug.Log("Outcome: " + outcome + action.actionName);

			if (outcome >= (dice / 2)) {
				return true;
			} else {
				return false;
			}
		} else {
			return true;
		}
	}

	private int CalculateStats(Action action) {
		int stats = 0;

		if (action.heat) {
			stats += heat;
		}
		if (action.weapon) {
			stats += weapon;
		}

		return stats;
	}
}
