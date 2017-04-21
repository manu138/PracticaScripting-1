using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUIState : UIStateBase
{
    public AttackUIState(CommandViewController controlled) : base(controlled)
    {

    }
    public override void OnMousePositionChanged(Vector3 mousePosition)
    {
        int remainingPoints = controlled.RemainingActionPoints;
        //int moveCost = controlled.Preview.GetCostTo(mousePosition);
    }
    public override void OnClick(Vector3 mousePosition)
    {
        Attack attack = new Attack();
        if (controlled.TryAddAction(attack))
        {
            // controlled.Preview.SetPosition(mousePosition);
            controlled.SetState(ActionType.None);

        }
    }
  
}
