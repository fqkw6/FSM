using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class UserPromptForConfirmationScript : MonoBehaviour
{
    public Text PromptTitleText;
    public Text PromptMessageText;
    public Button OkButton;
    public Button CancelButton;

    public event ButtonEventHandler OkButtonEvents;
    public event ButtonEventHandler CancelButtonEvents;

    public void OnOkClicked()
    {
        if (OkButtonEvents != null)
        {
            OkButtonEvents();
        }
    }

    public void OnCancelClicked()
    {
        if (CancelButtonEvents != null)
        {
            CancelButtonEvents();
        }
    }

    public void ShowPrompt(bool isVisible = true)
    {
        gameObject.SetActive(isVisible);
    }

    public void HidePrompt()
    {
        ShowPrompt(false);
    }

    public void ClearPrompt()
    {
        PromptTitleText.text = string.Empty;
        PromptMessageText.text = string.Empty;
    }

    public void HideAndClear()
    {
        HidePrompt();
        ClearPrompt();
    }
}
