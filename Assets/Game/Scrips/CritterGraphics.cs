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
	private Color midColor = Color.yellow;

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

		Color currentColor;

		if (lifePercent > 0.5f) {
			currentColor = Color.Lerp(midColor, healtyColor, 2f * (lifePercent - .5f));
		} else {
			currentColor = Color.Lerp(starvingColor, midColor, 2f * lifePercent);
		}

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
