using UnityEngine;
using Characters;

public class MoveUIState : UIStateBase
{
  
    public MoveUIState(CommandViewController controlled) : base(controlled)
    {

    }
   

    public override void OnMousePositionChanged(Vector3 mousePosition)
    {
        int remainingPoints = controlled.RemainingActionPoints;
        //int moveCost = controlled.Preview.GetCostTo(mousePosition);
    }

    public override void OnClick(Vector3 mousePosition)
    {
        Move move = new Move(mousePosition);
        if (controlled.TryAddAction(move))
        {
           // controlled.Preview.SetPosition(mousePosition);
            controlled.SetState(ActionType.None);
           
        }
    }
}