using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public static class UserPromptForConfirmationController
{
    private static GameObject _userPromptForConfirmationPanel;
    private static UserPromptForConfirmationScript _userPromptForConfirmationScript;
    private static Action<DialogResult> _lastWhenButtonIsClickedAction;

    // Use this for initialization
    public static void Initialize()
    {
        _userPromptForConfirmationPanel = GameObject.Find("UserPromptForConfirmationPanel");
        _userPromptForConfirmationScript = _userPromptForConfirmationPanel.transform.GetComponent<UserPromptForConfirmationScript>();

        _userPromptForConfirmationScript.HideAndClear();
    }

    internal static void PromptUserForConfirmation(string promptTitleText, string promptMessageText, Action<DialogResult> whenButtonIsClicked)
    {
        AddEventHandlers(whenButtonIsClicked);

        _userPromptForConfirmationScript.PromptTitleText.text = promptTitleText;
        _userPromptForConfirmationScript.PromptMessageText.text = promptMessageText;
        _userPromptForConfirmationScript.ShowPrompt();
    }

    private static void AddEventHandlers(Action<DialogResult> whenButtonIsClicked)
    {
        _lastWhenButtonIsClickedAction = whenButtonIsClicked;

        _userPromptForConfirmationScript.OkButtonEvents += OnOkButtonClicked;
        _userPromptForConfirmationScript.CancelButtonEvents += OnCancelButtonClicked;
    }

    private static void ClearEventHandlers()
    {
        _lastWhenButtonIsClickedAction = null;

        _userPromptForConfirmationScript.OkButtonEvents -= OnOkButtonClicked;
        _userPromptForConfirmationScript.CancelButtonEvents -= OnCancelButtonClicked;
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
        _lastWhenButtonIsClickedAction.Invoke(dialogResult);

        _userPromptForConfirmationScript.HideAndClear();

        ClearEventHandlers();
    }
}
