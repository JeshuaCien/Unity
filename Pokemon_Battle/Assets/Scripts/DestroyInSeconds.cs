using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField]

    private float _destroyInSecons = 2f;

    private void Start()
    {
        Destroy(gameObject, _destroyInSecons);
    }
}
