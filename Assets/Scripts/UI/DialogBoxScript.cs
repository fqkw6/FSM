using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void ButtonEventHandler();

public class DialogBoxScript : MonoBehaviour {

    public Text TitleText;
    public Text MessageText;
    public InputField PromptInputField;
    public Button SaveButton;
    public Button OkButton;
    public Button CancelButton;
    public Button MiddleOkButton;

    public event ButtonEventHandler SaveButtonEvents;
    public event ButtonEventHandler OkButtonEvents;
    public event ButtonEventHandler CancelButtonEvents;
    public event ButtonEventHandler MiddleOkButtonEvents;

    public void OnSaveButtonClicked()
    {
        if (SaveButtonEvents != null)
        {
            SaveButtonEvents();
        }
    }

    public void OnOkButtonClicked()
    {
        if (OkButtonEvents != null)
        {
            OkButtonEvents();
        }
    }

    public void OnCancelButtonClicked()
    {
        if (CancelButtonEvents != null)
        {
            CancelButtonEvents();
        }
    }

    public void OnMiddleOkButtonClicked()
    {
        if (MiddleOkButtonEvents != null)
        {
            MiddleOkButtonEvents();
        }
    }

    public void ShowDialogBoxAsType(DialogBoxType dialogBoxType)
    {
        SetDialogBoxType(dialogBoxType);

        ShowDialogBox();
    }

    private void SetDialogBoxType(DialogBoxType dialogBoxType)
    {
        HideAllOptionalControls();

        if (dialogBoxType == DialogBoxType.MessageWithOk)
        {
            MessageText.gameObject.SetActive(true);
            MiddleOkButton.gameObject.SetActive(true);
        }
        else if (dialogBoxType == DialogBoxType.MessageWithOkCancel)
        {
            MessageText.gameObject.SetActive(true);
            OkButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(true);
        }
        else if (dialogBoxType == DialogBoxType.PromptWithYesNo)
        {
            PromptInputField.gameObject.SetActive(true);

            SetOkButtonText("Yes");
            OkButton.gameObject.SetActive(true);

            SetCancelButtonText("No");
            CancelButton.gameObject.SetActive(true);
        }
        else
        {
            throw new Exception("SetDialogBoxType being set to invalid type: " + dialogBoxType);
        }
    }

    private void HideAllOptionalControls()
    {
        MessageText.gameObject.SetActive(false);
        PromptInputField.gameObject.SetActive(false);
        SaveButton.gameObject.SetActive(false);
        OkButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
        MiddleOkButton.gameObject.SetActive(false);

        TitleText.text = "";
        MessageText.text = "";
        PromptInputField.text = "";

        SetOkButtonText("OK");
        SetCancelButtonText("Cancel");
    }

    private void ShowDialogBox(bool isVisible = true)
    {
        gameObject.SetActive(isVisible);

        if (PromptInputField.isActiveAndEnabled)
        {
            EventSystem.current.SetSelectedGameObject(PromptInputField.gameObject, null);
        }
    }

    private void HideDialogBox()
    {
        ShowDialogBox(false);
    }

    private void ClearPrompt()
    {
        PromptInputField.text = string.Empty;
    }

    public void HideAndClear()
    {
        HideDialogBox();
        ClearPrompt();
    }

    private void SetOkButtonText(string newOkButtonText)
    {
        OkButton.GetComponentInChildren<Text>().text = newOkButtonText;
    }

    private void SetCancelButtonText(string newCancelButtonText)
    {
        CancelButton.GetComponentInChildren<Text>().text = newCancelButtonText;
    }
}

public enum DialogBoxType
{
    MessageWithOk,
    MessageWithOkCancel,
    PromptWithYesNo
}