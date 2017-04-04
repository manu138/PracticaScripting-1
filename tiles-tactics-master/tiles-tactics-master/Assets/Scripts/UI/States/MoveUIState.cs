using UnityEngine;

public class MoveUIState : UIStateBase
{
    public MoveUIState (CommandViewController controlled): base (controlled)
    {

    }

    public override void OnClick(Vector3 mousePosition)
    {
        Move move = new Move (mousePosition);
        controlled.TryAddAction (move);
    }
}