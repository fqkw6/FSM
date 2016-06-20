using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using UnityEngine.UI;

[ExecuteInEditMode]
public class InsideLocationController : MonoBehaviour {

    [Header("Images")]
    public Sprite BackgroundImage;
    public Sprite CharacterImage;

    [Header("Details")]
    public Character Character;

    private Transform _backgroundPanel;
    private Transform _characterPanel;
    private Transform _textBarPanel;
    private Transform _contentText;

    private Transform _detailsPanel;
    private Transform _nameText;
    private Transform _sexAndClassText;
    //private Transform _attribute1LabelText;
    private Transform _attribute1ValueText;
    //private Transform _attribute2LabelText;
    private Transform _attribute2ValueText;
    //private Transform _attribute3LabelText;
    private Transform _attribute3ValueText;

    private Transform _negativeOptionButton;
    private Transform _negativeOptionText;
    private Transform _positiveOptionButton;
    private Transform _positiveOptionText;

    void Start()
    {
        CaptureUIElements();

        SetupBackground();
        SetupCharacter();
        SetupDetails();
        HideOptionButtons();

        ClearTextBar();
        AddTextBarText("Hello there!");
        AddTextBarText("Can you do me a quick favour? You see, I was once a powerful magician...");
        AddTextBarText("Looking over Albez, you notice that he has a dark glimmer in his eyes. There is something that he isn't telling you, and it will likely end up poorly for you.");
        AddTextBarText("Can you pass me the eraser?");
        AddTextBarText("I've a new line for you.");
    }

    void Update () {
        if (!EditorApplication.isPlaying)
        {
            CaptureUIElements();

            SetupBackground();
            SetupCharacter();
            SetupDetails();
            ShowOptionButtons();

            ClearTextBar();
            AddTextBarText("This is the TextBar.");
        }
    }

    private void CaptureUIElements()
    {
        _backgroundPanel = transform.Find("BackgroundPanel");
        _characterPanel = transform.Find("CharacterPanel");
        _textBarPanel = transform.Find("TextBarPanel");
        _contentText = _textBarPanel.Find("TextBarScrollView").Find("Viewport").Find("ContentText");

        _detailsPanel = transform.Find("DetailsPanel");
        _nameText = _detailsPanel.Find("NameText");
        _sexAndClassText = _detailsPanel.Find("SexAndClassText");
        //_attribute1LabelText = _detailsPanel.Find("Attribute1LabelText");
        _attribute1ValueText = _detailsPanel.Find("Attribute1ValueText");
        //_attribute2LabelText = _detailsPanel.Find("Attribute2LabelText");
        _attribute2ValueText = _detailsPanel.Find("Attribute2ValueText");
        //_attribute3LabelText = _detailsPanel.Find("Attribute3LabelText");
        _attribute3ValueText = _detailsPanel.Find("Attribute3ValueText");

        _negativeOptionButton = transform.Find("NegativeOptionButton");
        _negativeOptionText = _negativeOptionButton.Find("NegativeOptionText");
        _positiveOptionButton = transform.Find("PositiveOptionButton");
        _positiveOptionText = _positiveOptionButton.Find("PositiveOptionText");
    }

    private void SetupBackground()
    {
        var backgroundPanelImage = _backgroundPanel.GetComponent<Image>();

        backgroundPanelImage.sprite = BackgroundImage;
    }

    private void SetupCharacter()
    {
        var characterPanelImage = _characterPanel.GetComponent<Image>();

        characterPanelImage.sprite = CharacterImage;
    }

    private void SetupDetails()
    {
        var nameText = _nameText.GetComponent<Text>();
        var sexAndClassText = _sexAndClassText.GetComponent<Text>();
        //var attribute1LabelText = _attribute1LabelText.GetComponent<Text>();
        var attribute1ValueText = _attribute1ValueText.GetComponent<Text>();
        //var attribute2LabelText = _attribute2LabelText.GetComponent<Text>();
        var attribute2ValueText = _attribute2ValueText.GetComponent<Text>();
        //var attribute3LabelText = _attribute3LabelText.GetComponent<Text>();
        var attribute3ValueText = _attribute3ValueText.GetComponent<Text>();

        nameText.text = Character.Name;
        sexAndClassText.text = string.Format("{0} {1}", Character.Sex.ToFriendlyString(), Character.Class.ToFriendlyString());
        //attribute1LabelText.text = "Deception";
        attribute1ValueText.text = Character.Stats.Deception.ToString();
        //attribute2LabelText.text = "Enjoyment";
        attribute2ValueText.text = Character.Stats.Enjoyment.ToString();
        //attribute3LabelText.text = "Morale";
        attribute3ValueText.text = Character.Stats.Morale.ToString();
    }

    private void SetNegativeButtonText(string text)
    {
        var negativeOptionText = _negativeOptionText.GetComponent<Text>();

        negativeOptionText.text = text;
    }

    private void SetPositiveButtonText(string text)
    {
        var positiveOptionText = _positiveOptionText.GetComponent<Text>();

        positiveOptionText.text = text;
    }

    private void ClearTextBar()
    {
        var contextText = _contentText.GetComponent<Text>();

        contextText.text = string.Empty;
    }

    private void AddTextBarText(string text)
    {
        var contextText = _contentText.GetComponent<Text>();

        if (!string.IsNullOrEmpty(contextText.text))
        {
            contextText.text += Environment.NewLine;
        }

        contextText.text += text;
    }

    #region Hide and Show
    private void ShowBackground(bool isVisible = true)
    {
        _backgroundPanel.gameObject.SetActive(isVisible);
    }

    private void HideBackground()
    {
        ShowBackground(false);
    }

    private void ShowCharacter(bool isVisible = true)
    {
        _characterPanel.gameObject.SetActive(isVisible);
    }

    private void HideCharacter()
    {
        ShowBackground(false);
    }

    private void ShowDetails(bool isVisible = true)
    {
        _detailsPanel.gameObject.SetActive(isVisible);
    }

    private void HideDetails()
    {
        ShowDetails(false);
    }

    private void ShowOptionButtons(bool isVisible = true)
    {
        ShowNegativeOptionButton(isVisible);
        ShowPositiveOptionButton(isVisible);
    }

    private void HideOptionButtons()
    {
        ShowOptionButtons(false);
    }

    private void ShowNegativeOptionButton(bool isVisible = true)
    {
        _negativeOptionButton.gameObject.SetActive(isVisible);
    }

    private void HideNegativeOptionButton()
    {
        ShowNegativeOptionButton(false);
    }

    private void ShowPositiveOptionButton(bool isVisible = true)
    {
        _positiveOptionButton.gameObject.SetActive(isVisible);
    }

    private void HidePositiveOptionButton()
    {
        ShowPositiveOptionButton(false);
    }

    private void ShowTextBar(bool isVisible = true)
    {
        _textBarPanel.gameObject.SetActive(isVisible);
    }

    private void HideTextBar()
    {
        ShowTextBar(false);
    }
    #endregion Hide and Show
}