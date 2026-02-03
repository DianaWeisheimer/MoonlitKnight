using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsPuzzles : MonoBehaviour
{
    public event Action CheckCompletion;
    public event Action CompletePuzzle;
    public void CheckCompleted()
    {
        CheckCompletion?.Invoke();
    }

    public void Complete()
    {
        CompletePuzzle?.Invoke();
    }
}
