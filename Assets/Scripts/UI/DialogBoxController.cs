using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class DialogBoxController : ScriptableObject
{
    private bool _isInitialized;
    private GameObject _dialogBoxPanel;
    private DialogBoxScript _dialogBoxScript;
    private Action<DialogResult, string> _lastWhenButtonIsClickedAction;

    public void ShowDialogBoxAsType(DialogBoxType dialogBoxType)
    {
        if (!_isInitialized)
        {
            Initialize();
        }

        _dialogBoxScript.ShowDialogBoxAsType(dialogBoxType);
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

    public void CloseDialogBox()
    {
        _dialogBoxScript.HideAndClear();
    }
}
