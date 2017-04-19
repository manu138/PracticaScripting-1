using UnityEngine;

public abstract class UIStateBase
{
    protected CommandViewController controlled;

    public UIStateBase(CommandViewController controlled)
    {
        this.controlled = controlled;
    }

    public virtual void Enable()
    {

    }

    public virtual void Disable()
    {

    }

    public virtual void OnMousePositionChanged(Vector3 mousePosition)
    {

    }

    public abstract void OnClick(Vector3 mousePosition);
}