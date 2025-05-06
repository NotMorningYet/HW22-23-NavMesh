using UnityEngine;

public class TargetPointView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void Enable(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}
