using UnityEngine;

public class FoodCtrl : BaseBehaviour
{
    [SerializeField]
    int feedAmount = 40;

    void Start()
    {
        setupGraphics();
        setupLogic();
    }

    void setupLogic()
    {
        Collider2DEventManager collider2DEventManager = GetComponentInChildren<Collider2DEventManager>();
        if (collider2DEventManager == null) throw new System.Exception("collider2DEventManager not found");

        collider2DEventManager.OnTriggerEnterEvent2D += (Collider2D other) => OnEaten(other.gameObject);
    }

    void setupGraphics()
    {
        transform.Rotate(Vector3.forward, Random.Range(0, 360));
    }

    void OnEaten(GameObject eater)
    {
        CritterCtrl critter = eater.GetComponentInParent<CritterCtrl>();
        if(critter != null)
        {
            critter.Feed(feedAmount);
        }

        gameObject.SetActive(false);
    }
}
