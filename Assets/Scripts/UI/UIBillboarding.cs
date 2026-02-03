using UnityEngine;

public class UIBillboarding : MonoBehaviour
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
