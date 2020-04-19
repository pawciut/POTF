using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTimer : MonoBehaviour
{
    [SerializeField()]
    [Tooltip("Display tooltip after X seconds when mouse over")]
    float DisplayTooltipTime = 1f;

    bool IsMouseStillInside = false;

    public GameObject TooltipControl;

    Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (TooltipControl != null)
            TooltipControl.SetActive(false);
    }

    public void OnPointerEnter()
    {
        Debug.Log($"{name}:TooltipTimer {nameof(OnPointerEnter)}");
        IsMouseStillInside = true;
        coroutine = StartCoroutine(MyCoroutine(DisplayTooltipTime));
    }

    public void OnPointerExit()
    {
        IsMouseStillInside = false;
        TooltipControl.SetActive(false);
        Debug.Log($"{name}:TooltipTimer {nameof(OnPointerExit)}");
    }


    IEnumerator MyCoroutine(float duration)
    {
        Debug.Log($"{name}:Coroutine loop");
        if (!IsMouseStillInside)
        {
            Debug.Log($"{name}:Tooltip Canceled");
            yield break;
        }

        yield return new WaitForSeconds(duration); ;    //Wait one frame

        if (IsMouseStillInside)
            TooltipControl.SetActive(true);
        Debug.Log($"{name}:Tooltip Show");
    }
}
