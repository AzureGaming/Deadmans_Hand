using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public InputField inputField;

    public Dictionary<string, Outcome> currentOutcomes = new Dictionary<string, Outcome>();
    private Navigator navigator;
    private HistoryLog historyLog;

    [SerializeField] private Outcome startingScenario;

    void Start()
    {
        historyLog = GameObject.Find("HistoryLog").GetComponent<HistoryLog>();
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        navigator = GetComponent<Navigator>();

        LoadScenario(startingScenario);

        inputField.onEndEdit.AddListener(delegate
        {
            HandleOnEditEnd(inputField.text);
        });
    }

    public void LoadScenario(Outcome scenario)
    {
        historyLog.ClearText();
        historyLog.AppendText(scenario.outcomeName);
        historyLog.AppendText(scenario.description);
        SetCurrentOutcomes(scenario.actions);
        inputField.Select();
    }

    private void SetCurrentOutcomes(List<Action> actions)
    {
        int counter = 1;

        currentOutcomes.Clear();

        if (actions.Count > 0)
        {
            actions.ForEach(action =>
            {
                string actionName = action.actionName.ToLower();
                Outcome outcome = GetOutcome(action);

                // Todo: Move to appropriate location
                // UpdatePlayerStats(action);

                currentOutcomes.Add(actionName, outcome);
                historyLog.AppendText(counter.ToString() + ". " + action.actionName);
                counter++;
            });
        }
    }

    private void HandleOnEditEnd(string userInput)
    {
        string lowerCaseInput = userInput.ToLower();

        historyLog.AppendText(userInput);
        inputField.text = null;

        if (currentOutcomes.ContainsKey(lowerCaseInput))
        {
            LoadScenario(currentOutcomes[lowerCaseInput]);
        }
        else
        {
            historyLog.AppendText("Invalid input");
            inputField.Select();
        }
    }

    private Outcome GetOutcome(Action action)
    {
        bool isSuccess = CalculateSuccess(action);
        List<Outcome> outcomes = action.outcomes;

        if (outcomes.Count > 0)
        {
            return isSuccess ? outcomes[0] : outcomes[1];
        }

        return null;
    }

    private bool CalculateSuccess(Action action)
    {
        int diceMax = action.dice;
        int roll = Random.Range(0, diceMax);
        // Todo: Implement dice modifier
        Debug.Log("Roll for action:" + action.actionName + " " + roll + " / " + diceMax);
        bool isSuccess = roll >= (diceMax / 2);

        return isSuccess;
    }

    private void UpdatePlayerStats(Action action)
    {
        // Todo: Implement
    }
}