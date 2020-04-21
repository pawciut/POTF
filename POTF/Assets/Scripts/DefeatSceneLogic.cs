using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatSceneLogic : MonoBehaviour
{
    public AnalyticsPanelScript Analytics;

    // Start is called before the first frame update
    void Start()
    {
        Analytics.UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
