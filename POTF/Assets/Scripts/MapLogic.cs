using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLogic : MonoBehaviour
{
    public Button StartGameButton;
    public Button NextTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
    }
}
