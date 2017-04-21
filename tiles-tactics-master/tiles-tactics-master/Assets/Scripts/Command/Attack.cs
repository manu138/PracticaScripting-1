using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class Attack : Command
{
    public Vector2 position;
    public override int Cost
    {
        get
        {
            return 1;
        }
    }
  
    public override void Execute(Character character)
    {
        character.Attack(position,OnFinished);
    }

    public override void UnExecute(Character character)
    {

    }

}
