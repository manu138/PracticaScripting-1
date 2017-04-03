using UnityEngine;
using Characters;
public class Move : Command 
{
	private Vector2 position;

    public Move(Vector2 position)
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
        character.Move (position, OnFinished);
    }
}
