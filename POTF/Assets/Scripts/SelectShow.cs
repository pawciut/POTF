using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShow : MonoBehaviour
{
    private ISelectable selectable;

    [SerializeField]
    private GameObject objectToShow;

    // Start is called before the first frame update
    void Start()
    {
        selectable = this.GetComponent<ISelectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable != null)
        {
            objectToShow.SetActive(selectable.Selected);
        }
    }
}
