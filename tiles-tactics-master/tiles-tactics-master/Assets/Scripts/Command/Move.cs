using UnityEngine;
using Characters;

public class Move : Command
{
    private Vector3 position;


    public Move(Vector3 position)
    {
        this.position = position;
    }

    public override int Cost
    {
        get
        {
            return 1;
        }
    }

    public override void Execute(Character character)
    {
        character.Move(new Vector2(0,0), OnFinished);
    }
}
