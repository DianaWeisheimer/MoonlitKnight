using UnityEngine;

public class PlayerMenuPage : MonoBehaviour
{
    public virtual void OnPageOpen()
    {

    }

    public virtual bool CloseSubMenu()
    {
        return false;
    }
}
