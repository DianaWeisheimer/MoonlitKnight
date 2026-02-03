using System;
using UnityEngine;
public class UIInputEvents
{
    public event Action<Vector2> onNavigatePressed;
    public void NavigatePressed(Vector2 moveDir)
    {
        if (onNavigatePressed != null)
        {
            onNavigatePressed(moveDir);
        }
    }

    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }
    }

    public event Action onCancelPressed;
    public void CancelPressed()
    {
        if (onCancelPressed != null)
        {
            onCancelPressed();
        }
    }

    public event Action onPageRightPressed;
    public void PageRightPressed()
    {
        if (onPageRightPressed != null)
        {
            onPageRightPressed();
        }
    }

    public event Action onPageLeftPressed;
    public void PageLeftPressed()
    {
        if (onPageLeftPressed != null)
        {
            onPageLeftPressed();
        }
    }
}
