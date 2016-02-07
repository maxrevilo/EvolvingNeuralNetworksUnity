using UnityEngine;
using System;

[RequireComponent(typeof(CritterCtrl))]
public class CritterSensors : BaseBehaviour
{
    private CritterCtrl critterCtrl;

    [SerializeField]
    private Transform antenaL;
    [SerializeField]
    private Transform antenaR;

    public SceneManager scene;

    private GameObject[] foods;

    private float antenaLSignal = float.MaxValue;
    private float antenaRSignal = float.MaxValue;

    void Awake()
    {
        critterCtrl = GetComponent<CritterCtrl>();

        foods = (GameObject[]) scene.foods.Clone();

        if (antenaL == null) throw new Exception("antenaL not set");
        if (antenaR == null) throw new Exception("antenaL not set");
        if (scene == null) throw new Exception("scene not set");
    }

    public int SampleLife()
    {
        return critterCtrl.getLife();
    }

    public float SampleAntenaR()
    {
        return antenaRSignal;
    }

    public float SampleAntenaL()
    {
        return antenaLSignal;
    }

    private void FixedUpdate()
    {
        InsertionSort(foods);
        if(foods.Length > 0)
        {
            GameObject closestFood = foods[0];

            antenaLSignal = Vector3.Distance(antenaL.position, transform.position);
            antenaRSignal = Vector3.Distance(antenaR.position, transform.position);
        }
    }

    private void InsertionSort(GameObject[] inputArray)
    {
        long j = 0;
        GameObject temp;
        for (int index = 1; index < inputArray.Length; index++)
        {
            j = index;
            temp = inputArray[index];
            float tempDist = distance(temp);
            while ((j > 0) && (distance(inputArray[j - 1]) > tempDist))
            {
                inputArray[j] = inputArray[j - 1];
                j = j - 1;
            }
            inputArray[j] = temp;
        }
    }

    private float distance(GameObject gameObject)
    {
        Vector3 iPos = transform.position;
        Vector3 fPos = gameObject.transform.position;
        float dX = fPos.x - iPos.x;
        float dZ = fPos.z - iPos.z;
        return dX * dX + dZ * dZ;
    }

    private void CritterDied()
    {
        enabled = false;
    }

    private void CritterRespawned()
    {
        enabled = true;
    }
}
