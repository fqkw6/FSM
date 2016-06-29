using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public static class UserPromptForStringController {

    private static GameObject _userPromptForStringPanel;
    private static UserPromptForStringScript _userPromptForStringScript;
    private static Action<DialogResult, string> _lastWhenButtonIsClickedAction;

    // Use this for initialization
    public static void Initialize() {
        _userPromptForStringPanel = GameObject.Find("UserPromptForStringPanel");
        _userPromptForStringScript = _userPromptForStringPanel.transform.GetComponent<UserPromptForStringScript>();

        _userPromptForStringScript.HideAndClear();
    }

    internal static void PromptUserForString(string promptTitleText, Action<DialogResult, string> whenButtonIsClicked)
    {
        AddEventHandlers(whenButtonIsClicked);

        _userPromptForStringScript.PromptTitleText.text = promptTitleText;
        _userPromptForStringScript.ShowPrompt();
    }

    private static void AddEventHandlers(Action<DialogResult, string> whenButtonIsClicked)
    {
        _lastWhenButtonIsClickedAction = whenButtonIsClicked;

        _userPromptForStringScript.OkButtonEvents += OnOkButtonClicked;
        _userPromptForStringScript.CancelButtonEvents += OnCancelButtonClicked;
    }

    private static void ClearEventHandlers()
    {
        _lastWhenButtonIsClickedAction = null;

        _userPromptForStringScript.OkButtonEvents -= OnOkButtonClicked;
        _userPromptForStringScript.CancelButtonEvents -= OnCancelButtonClicked;
    }

    private static void OnOkButtonClicked()
    {
        DoButtonLogic(DialogResult.Ok);
    }

    private static void OnCancelButtonClicked()
    {
        DoButtonLogic(DialogResult.Cancel);
    }

    private static void DoButtonLogic(DialogResult dialogResult)
    {
        var userStringEntry = _userPromptForStringScript.PromptInputField.text;
        _lastWhenButtonIsClickedAction.Invoke(dialogResult, userStringEntry);

        _userPromptForStringScript.HideAndClear();

        ClearEventHandlers();
    }
}
