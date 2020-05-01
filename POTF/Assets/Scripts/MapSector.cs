using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSector : MonoBehaviour
{
    [SerializeField]
    Image targetGraphic;
    Sprite defaultSprite;
    [SerializeField]
    Sprite highlightedSprite;
    [SerializeField]
    Sprite pressedSprite;
    [SerializeField]
    Sprite selectedSprite;
    [SerializeField]
    Sprite disableSprite;

    public ButtonStates State = ButtonStates.Default;


    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = targetGraphic.sprite;
    }

    public void UpdateState(ButtonStates newState)
    {
        switch (newState)
        {
            case ButtonStates.Highlighted:
                targetGraphic.sprite = highlightedSprite;
                break;
            case ButtonStates.Pressed:
                targetGraphic.sprite = pressedSprite;
                break;
            case ButtonStates.Selected:
                targetGraphic.sprite = selectedSprite;
                break;
            default:
                targetGraphic.sprite = defaultSprite;
                break;
        }

        State = newState;
    }
}
