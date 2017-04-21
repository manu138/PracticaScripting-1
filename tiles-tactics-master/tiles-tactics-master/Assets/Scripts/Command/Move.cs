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
        Debug.Log("");
        

        character.Move(position, OnFinished);
    }
}
