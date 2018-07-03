using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public InputField inputField;

	public Dictionary<string, Scenario> dictionary = new Dictionary<string, Scenario>();
	private Navigator navigator;
	private HistoryLog historyLog;

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

		if (action.formula == null) {
			return action.failure;
		} else {
			return action.success;
		}
	}

	private void SetDictionary(List<Action> actions) {
		dictionary.Clear();

		if (actions != null) {
			actions.ForEach((Action action) => {
				int counter = 1;
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
}
