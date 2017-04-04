using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecuteButton : MonoBehaviour {


    [SerializeField]
    private ActionType state;
    [SerializeField]
    private CommandViewController viewController;
    private Button button;
    private Controller controller;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Execute);
    }

    private void Execute()
    {
        controller.ExecuteActions();
    }

}
