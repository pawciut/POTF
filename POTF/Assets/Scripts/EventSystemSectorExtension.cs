using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSectorExtension : MonoBehaviour
{
    [SerializeField]
    MapSector[] sectors;

    bool IsLeftMouseButtonDown = false;
    bool IsLeftMouseButtonDownStarted = false;
    bool IsLeftMouseButtonUp = false;
    bool IsStillDragging = false;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        IsLeftMouseButtonDown = Input.GetMouseButton(0);
        IsLeftMouseButtonDownStarted = Input.GetMouseButtonDown(0);
        IsLeftMouseButtonUp = Input.GetMouseButtonUp(0);

        if (IsLeftMouseButtonUp)
        {
            IsStillDragging = false;
        }


        IsPointerOverMapSector();
    }

    public bool IsPointerOverMapSector()
    {
        return IsPointerOverMapSector(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverMapSector(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.GetComponent<MapSector>() != null)
            {
                //Debug.Log($"Pointer over map sector {curRaysastResult.gameObject.name}");


                if (sectors != null)
                    foreach (var sector in sectors)
                    {
                        if (sector.gameObject.GetInstanceID() != curRaysastResult.gameObject.GetInstanceID())
                        {
                            if (sector.State == ButtonStates.Pressed || sector.State == ButtonStates.Selected)
                            {
                                //nie zmieniaj
                            }
                            else if (sector.State != ButtonStates.Selected)
                                sector.UpdateState(ButtonStates.Default);
                        }
                        else
                        {
                            TrySelectSector(sector);
                        }
                    }
                return true;
            }
            else
            {
                NoSectorHighlighted();
            }
        }
        return false;
    }

    void TrySelectSector(MapSector sectorButton)
    {
        //wylaczyc poprzednio zaznaczone
        foreach (var sector in sectors)
        {
            if (sector.gameObject.GetInstanceID() != sectorButton.gameObject.GetInstanceID())
            {
                if (IsLeftMouseButtonDown)
                {
                    if (sector.State == ButtonStates.Selected || sector.State == ButtonStates.Pressed)
                    {
                        sector.UpdateState(ButtonStates.Default);
                    }
                }
            }
        }

        //zaznaczyc obecny
        if (IsLeftMouseButtonDown
                                || sectorButton.State == ButtonStates.Selected)
        {
            sectorButton.UpdateState(ButtonStates.Selected);
        }
        else
            sectorButton.UpdateState(ButtonStates.Highlighted);
    }

    void NoSectorHighlighted()
    {
        if (sectors != null)
            foreach (var sector in sectors)
            {
                if (sector != null)
                {
                    //gdy nie jest zaznaczony, lub przestano przeciagac(rozpoczeto na sektorze)
                    if (sector.State != ButtonStates.Selected &&
                            !(sector.State == ButtonStates.Pressed && IsStillDragging))
                        sector.UpdateState(ButtonStates.Default);
                }
            }
    }

    ///Gets all event systen raycast results of current mouse or touch position.
    List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
