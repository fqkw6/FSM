using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class DialogBoxController : ScriptableObject
{
    private bool _isInitialized;
    private GameObject _dialogBoxPanel;
    private DialogBoxScript _dialogBoxScript;
    private Action<DialogResult> _actionForWhenAnyButtonIsClickedWithNoReturn;
    private Action<DialogResult, string> _actionForWhenAnyButtonIsClickedWithStringReturn;
    private bool _allowEmptyInputs = false;

    private void ShowDialogBoxAsType(DialogBoxType dialogBoxType)
    {
        _dialogBoxScript.ShowDialogBoxAsType(dialogBoxType);
    }

    private void EnsureInitialization()
    {
        if (!_isInitialized)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        _dialogBoxPanel = (GameObject)Instantiate(Resources.Load("DialogBoxPanel"));
        _dialogBoxScript = _dialogBoxPanel.transform.GetComponent<DialogBoxScript>();

        PositionUICorrectly();

        _isInitialized = true;
    }

    private void PositionUICorrectly()
    {
        var canvas = GameObject.Find("Canvas");

        _dialogBoxPanel.transform.SetParent(canvas.transform);
        _dialogBoxPanel.transform.localPosition = Vector3.zero;
        _dialogBoxPanel.transform.localScale = Vector3.one;

        var rectTransform = _dialogBoxPanel.GetComponent<RectTransform>();
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }

    private void CloseDialogBox()
    {
        _dialogBoxScript.HideAndClear();
    }

    internal void DisplayMessage(string messageTitle, string message)
    {
        EnsureInitialization();

        AddEventHandlers();

        ShowDialogBoxAsType(DialogBoxType.MessageWithOk);

        _dialogBoxScript.TitleText.text = messageTitle;
        _dialogBoxScript.MessageText.text = message;
    }

    internal void PromptUserForConfirmation(string promptTitle, string overwriteMessage, Action<DialogResult> whenConfirmationPromptButtonIsClicked)
    {
        EnsureInitialization();

        AddEventHandlers(whenConfirmationPromptButtonIsClicked);

        ShowDialogBoxAsType(DialogBoxType.MessageWithOkCancel);

        _dialogBoxScript.TitleText.text = promptTitle;
        _dialogBoxScript.MessageText.text = overwriteMessage;
    }

    internal void PromptUserForString(string promptTitle, string prepopulatedText, Action<DialogResult, string> whenConfirmationPromptButtonIsClicked, bool allowEmptyInputs = false)
    {
        EnsureInitialization();

        _allowEmptyInputs = allowEmptyInputs;

        AddEventHandlers(whenConfirmationPromptButtonIsClicked);

        ShowDialogBoxAsType(DialogBoxType.PromptWithYesNo);

        _dialogBoxScript.TitleText.text = promptTitle;
        _dialogBoxScript.PromptInputField.text = prepopulatedText;
    }

    private void AddEventHandlers()
    {
        _dialogBoxScript.SaveButtonEvents += OnSaveButtonClicked;
        _dialogBoxScript.OkButtonEvents += OnOkButtonClicked;
        _dialogBoxScript.CancelButtonEvents += OnCancelButtonClicked;
        _dialogBoxScript.MiddleOkButtonEvents += OnMiddleOkButtonClicked;
    }

    private void AddEventHandlers(Action<DialogResult> actionForWhenButtonIsClicked)
    {
        _actionForWhenAnyButtonIsClickedWithNoReturn = actionForWhenButtonIsClicked;

        _dialogBoxScript.SaveButtonEvents += OnSaveButtonClicked;
        _dialogBoxScript.OkButtonEvents += OnOkButtonClicked;
        _dialogBoxScript.CancelButtonEvents += OnCancelButtonClicked;
        _dialogBoxScript.MiddleOkButtonEvents += OnMiddleOkButtonClicked;
    }

    private void AddEventHandlers(Action<DialogResult, string> actionForWhenButtonIsClicked)
    {
        _actionForWhenAnyButtonIsClickedWithStringReturn = actionForWhenButtonIsClicked;

        _dialogBoxScript.SaveButtonEvents += OnSaveButtonClicked;
        _dialogBoxScript.OkButtonEvents += OnOkButtonClicked;
        _dialogBoxScript.CancelButtonEvents += OnCancelButtonClicked;
        _dialogBoxScript.MiddleOkButtonEvents += OnMiddleOkButtonClicked;
    }

    private void ClearEventHandlers()
    {
        _actionForWhenAnyButtonIsClickedWithNoReturn = null;
        _actionForWhenAnyButtonIsClickedWithStringReturn = null;

        _dialogBoxScript.SaveButtonEvents -= OnSaveButtonClicked;
        _dialogBoxScript.OkButtonEvents -= OnOkButtonClicked;
        _dialogBoxScript.CancelButtonEvents -= OnCancelButtonClicked;
        _dialogBoxScript.MiddleOkButtonEvents -= OnMiddleOkButtonClicked;
    }

    private void OnSaveButtonClicked()
    {
        DoButtonLogic(DialogResult.Ok);
    }

    private void OnOkButtonClicked()
    {
        DoButtonLogic(DialogResult.Ok);
    }

    private void OnCancelButtonClicked()
    {
        DoButtonLogic(DialogResult.Cancel);
    }

    private void OnMiddleOkButtonClicked()
    {
        DoButtonLogic(DialogResult.Ok);
    }

    private void DoButtonLogic(DialogResult dialogResult)
    {
        if (_actionForWhenAnyButtonIsClickedWithNoReturn != null)
        {
            _actionForWhenAnyButtonIsClickedWithNoReturn.Invoke(dialogResult);
        }
        
        if (_actionForWhenAnyButtonIsClickedWithStringReturn != null)
        {
            if (!_allowEmptyInputs && string.IsNullOrEmpty(_dialogBoxScript.PromptInputField.text) && dialogResult == DialogResult.Ok)
            {
                var dialogBoxController = CreateInstance("DialogBoxController") as DialogBoxController;
                dialogBoxController.DisplayMessage("No Value Entered", "The input cannot be blank.");
                return;
            }
            _actionForWhenAnyButtonIsClickedWithStringReturn.Invoke(dialogResult, _dialogBoxScript.PromptInputField.text);
        }

        ClearEventHandlers();

        CloseDialogBox();
    }
}
