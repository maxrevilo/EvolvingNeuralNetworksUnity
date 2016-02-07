using UnityEngine;
using System;

public class CritterCtrl : BaseBehaviour
{
    [SerializeField]
    private int maxLife = 100;

    [SerializeField]
    private int life = 100;

    [SerializeField]
    private float lifeConsumtion = 15f;

    public bool isAlive() { return life > 0; }

    public int getMaxLife() { return maxLife; }
    public int getLife() { return life;  }

    public void SetLife(int amout)
    {
        life = amout;
        if(amout < 0)
        {
            Starve();
        } else if(amout > maxLife)
        {
            Overfeeded();
        }
    }

    public void Feed(int amout)
    {
        SetLife(amout);
    }

    public void Starve()
    {
        Die();
    }

    public void Overfeeded()
    {
        Die();
    }

    public float lifeSpan()
    {
        if(deadTime > birthTime)
        {
            return deadTime - birthTime;
        } else
        {
            return Time.time - birthTime;
        }
    }

    public void Die()
    {
        SendMessage("CritterDied");
    }

    private void CritterDied()
    {
        enabled = false;
        life = 0;
        deadTime = Time.time;
        Debug.Log("Critter died after " + lifeSpan() + "s");
    }

    private float lastTimeSienceConsumtion;
    private float birthTime;
    private float deadTime;

    void Start()
    {
        birthTime = Time.time;
        deadTime = 0;
        lastTimeSienceConsumtion = Time.time;
    }

    void FixedUpdate()
    {
        float consumptionDeltaTime = Time.time - lastTimeSienceConsumtion;

        float deltaConsumption = consumptionDeltaTime * lifeConsumtion;

        if(deltaConsumption >= 1f)
        {
            int integerDeltaConsumption = (int) deltaConsumption;

            SetLife(life - integerDeltaConsumption);

            deltaConsumption -= integerDeltaConsumption;

            float deltaExtra = deltaConsumption / lifeConsumtion;
            lastTimeSienceConsumtion = Time.time - deltaExtra;
        }
    }
}
