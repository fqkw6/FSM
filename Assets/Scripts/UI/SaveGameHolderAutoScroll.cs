using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveGameHolderAutoScroll : MonoBehaviour {

    private int _verticalPaddingTop;
    private int _verticalPaddingBottom;
    private float _viewportLowerBoundary;
    private float _viewportUpperBoundary;
    private ScrollRect _scrollRect;

    void Awake()
    {
        _verticalPaddingTop = GetComponent<VerticalLayoutGroup>().padding.top;
        _verticalPaddingBottom = GetComponent<VerticalLayoutGroup>().padding.bottom;
        _viewportLowerBoundary = -gameObject.transform.parent.GetComponent<RectTransform>().rect.height;
        _viewportUpperBoundary = 0f;

        _scrollRect = gameObject.transform.parent.parent.GetComponent<ScrollRect>();
    }

    public void FocusOnSelectedSaveGame()
    {
        var selectedGameSaveItem = GetSelectedGameSave();

        if (selectedGameSaveItem == null)
        {
            return;
        }

        var rectTransform = GetComponent<RectTransform>();

        var selectedSaveGameRectTransform = selectedGameSaveItem.GetComponent<RectTransform>();

        var selectedSaveGameYPosition = selectedSaveGameRectTransform.localPosition.y;

        var selectedSaveGameHeight = selectedSaveGameRectTransform.rect.height;

        var selectedSaveGameTopLocation = selectedSaveGameYPosition + (selectedSaveGameHeight / 2);
        var selectedSaveGameBottomLocation = selectedSaveGameYPosition - (selectedSaveGameHeight / 2);

        var contentDisplayOffset = rectTransform.offsetMax.y;

        var bottomBoundary = _viewportLowerBoundary - contentDisplayOffset - _verticalPaddingBottom;
        var topBoundary = _viewportUpperBoundary - contentDisplayOffset + _verticalPaddingTop;

        if (selectedSaveGameBottomLocation < bottomBoundary || selectedSaveGameTopLocation > topBoundary)
        {
            //var distance = bottomBoundary - selectedSaveGameBottomLocation;

            var contentHeight = rectTransform.rect.height;

            var contentNotDisplayedHeight = contentHeight + _viewportLowerBoundary;

            var goalView = -1 * (selectedSaveGameYPosition - _viewportLowerBoundary / 2);

            var newScrollbarPosition = 1 - Mathf.Clamp(goalView / contentNotDisplayedHeight, 0f, 1f);

            _scrollRect.verticalNormalizedPosition = newScrollbarPosition;
        }
    }

    GameListItemScript GetSelectedGameSave()
    {
        var selectedGameSaveItem = EventSystem.current.currentSelectedGameObject;

        if (selectedGameSaveItem != null)
        {
            var gameSaveItemScript = selectedGameSaveItem.GetComponent<GameListItemScript>();

            if (gameSaveItemScript != null)
            {
                return gameSaveItemScript;
            }
        }

        foreach (Transform child in gameObject.transform)
        {
            var childGameSaveItemScript = child.GetComponent<GameListItemScript>();

            if (childGameSaveItemScript.IsSaveGameSelected)
            {
                return childGameSaveItemScript;
            }
        }

        return null;
    }
}
