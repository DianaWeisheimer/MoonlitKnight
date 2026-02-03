using UnityEngine;

public class LockOnReticle : MonoBehaviour
{
    public Transform target;
    public GameObject reticle;

    private void Start()
    {
        if(target == null)
            reticle.SetActive(false);
    }

    void FixedUpdate()
    {
        if (target)
        {
            reticle.transform.position = Camera.main.WorldToScreenPoint(target.position);
        }
    }

    public void SetReticle(bool lockOn, Transform transform)
    {
        switch (lockOn)
        {
            case true:
                reticle.SetActive(true);
                target = transform; break;
            case false:
                reticle.SetActive(false);
                target = null; break;
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.uIEvents.onSetLockonReticle += SetReticle;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uIEvents.onSetLockonReticle -= SetReticle;
    }
}
