using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller: MonoBehaviour
{
    [SerializeField]
    private int actionPoints = 10;
    private bool isExecuting;
    private Queue<Command> actions;

    [SerializeField]
    private Character character;
    public Action Onfinished;
 
    public void start()
    {
       
    }

    private void Awake()
    {
        actions = new Queue<Command>();
        character = GetComponent<Character>();
    }
    private void Update ()

    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // TryAddAction(character.MoveTo(character.MousePos(),onFinished));



            ExecuteActions();
        }
           
           
    }
    public int CurrentActionPoints
    {
        get; private set;
    }




    private void Start()
    {
        Reset();
    }

    public void ExecuteActions()
    {
        isExecuting = true;
        ExecuteNextAction();
    }

    public bool TryAddAction(Command action)
    {
        if (!isExecuting && CurrentActionPoints >= action.Cost)
        {
            actions.Enqueue(action);
            CurrentActionPoints -= action.Cost;
            return true;
        }
        else
            return false;
    }

    private void ExecuteNextAction()
    {
        if (actions.Count == 0)
        {
            Reset();
            return;
        }
        Command next = actions.Dequeue();
        next.OnCompleted += ExecuteNextAction;
        next.Execute(character);
    }

    private void Reset()
    {
        isExecuting = false;
        CurrentActionPoints = actionPoints;
    }
}