using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller: MonoBehaviour
{
    public event Action OnFinished;

    [SerializeField]
    private int actionPoints = 10;
    private bool isExecuting;
    private Queue<Command> actions;
    public Character character;

    
    public int RemainingActionPoints
    {
        get; private set;
    }
  

    private void Awake()
    {
        actions = new Queue<Command>();
        
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
        if (!isExecuting && RemainingActionPoints >= action.Cost)
        {
            actions.Enqueue(action);
            RemainingActionPoints -= action.Cost;
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
        RemainingActionPoints = actionPoints;
        if (OnFinished != null)
            OnFinished();
    }
    public void Undo()
    {
        actions.Clear();
        Debug.Log("Actions clear");


    }
}