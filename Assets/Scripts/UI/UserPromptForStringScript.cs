using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void ButtonEventHandler();

public class UserPromptForStringScript : MonoBehaviour
{
    public Text PromptTitleText;
    public InputField PromptInputField;
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
        EventSystem.current.SetSelectedGameObject(PromptInputField.gameObject, null);
    }

    public void HidePrompt()
    {
        ShowPrompt(false);
    }

    public void ClearPrompt()
    {
        PromptInputField.text = string.Empty;
    }

    public void HideAndClear()
    {
        HidePrompt();
        ClearPrompt();
    }
}
