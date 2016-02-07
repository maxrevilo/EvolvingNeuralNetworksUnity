using UnityEngine;

[RequireComponent(typeof(Collider2D))]
class Collider2DEventManager : BaseBehaviour
{
    public delegate void ColliderEvent2D(Collider2D other);

    // Trigger 2D
    public event ColliderEvent2D OnTriggerEnterEvent2D;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (OnTriggerEnterEvent2D != null) OnTriggerEnterEvent2D(other);
    }
}
