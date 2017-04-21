using System;
using Characters;
public abstract class Command
{
    public event Action OnCompleted;
    public abstract int Cost { get; }

    public abstract void Execute (Character character);
    public abstract void UnExecute(Character character);

    protected void OnFinished ()
    {
         if (OnCompleted!=null)
            OnCompleted ();
    }
}