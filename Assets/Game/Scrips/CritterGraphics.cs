using System;
using System;
using UnityEngine;

[RequireComponent(typeof(CritterCtrl))]
class CritterGraphics: BaseBehaviour
{
    [SerializeField]
    private SpriteRenderer torzo;

    [SerializeField]
    private Color healtyColor = Color.green;

    [SerializeField]
    private Color starvingColor = Color.red;

    [SerializeField]
    private Color deadColor = Color.gray;


    private CritterCtrl critter;

    void Awake()
    {
        critter = GetComponent<CritterCtrl>();
        if (torzo == null) throw new Exception("Sprite Renderer <Torzo> not set");
    }

    void Update()
    {
        float lifePercent = Mathf.InverseLerp(0, critter.getMaxLife(), critter.getLife());

        Color currentColor = Color.Lerp(starvingColor, healtyColor, lifePercent);

        torzo.color = currentColor;
    }

    private void CritterRespawned()
    {
        enabled = true;
    }

    private void CritterDied()
    {
        torzo.color = deadColor;
        enabled = false;
    }
}
