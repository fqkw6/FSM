using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GameListItemScript : MonoBehaviour, ISelectHandler
{
    public Color NormalColor;
    public Color SelectedColor;
    public SaveController SaveScreenController;
    public LoadController LoadScreenController;

    public bool IsSaveGameSelected;

    private SaveGameHolderAutoScroll _saveGameHolderAutoScroll;

    private float _doubleClickStart = -1;

    private const float DoubleClickTime = 0.3f;

    void Awake()
    {
        SetSaveGameIsSelected(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CheckDoubleClick();
        }
    }

    void CheckDoubleClick()
    {
        if ((Time.time - _doubleClickStart) < DoubleClickTime)
        {
            if (SaveScreenController != null)
            {
                SaveScreenController.Save();
            }

            if (LoadScreenController != null)
            {
                LoadScreenController.Load();
            }

            _doubleClickStart = -1;
        }
        else
        {
            _doubleClickStart = Time.time;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        DeselectSiblings();

        SetSaveGameIsSelected(true);

        if (_saveGameHolderAutoScroll == null)
        {
            _saveGameHolderAutoScroll = transform.parent.GetComponent<SaveGameHolderAutoScroll>();
        }

        _saveGameHolderAutoScroll.FocusOnSelectedSaveGame();
    }

    void DeselectSiblings()
    {
        var saveGameSiblings = gameObject.transform.parent.GetComponentsInChildren<GameListItemScript>();

        foreach (var saveGameSibling in saveGameSiblings)
        {
            if (saveGameSibling != this)
            {
                saveGameSibling.SetSaveGameIsSelected(false);
            }
        }
    }

    void SetSaveGameIsSelected(bool isSelected)
    {
        IsSaveGameSelected = isSelected;

        var selectable = gameObject.transform.GetComponent<Selectable>();

        var newColorBlock = new ColorBlock
        {
            colorMultiplier = selectable.colors.colorMultiplier,
            disabledColor = selectable.colors.disabledColor,
            fadeDuration = selectable.colors.fadeDuration,
            highlightedColor = selectable.colors.highlightedColor,
            normalColor = selectable.colors.normalColor,
            pressedColor = selectable.colors.pressedColor
        };

        if (isSelected)
        {
            newColorBlock.normalColor = SelectedColor;
        }
        else
        {
            newColorBlock.normalColor = NormalColor;
        }

        selectable.colors = newColorBlock;
    }
}
