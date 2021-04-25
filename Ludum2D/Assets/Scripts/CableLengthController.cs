using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLengthController : MonoBehaviour
{
    public GameObject lastCable;

    private string baseName;
    private const float cableDistance = 1.094f;
    public int cablesSoFar = 2;

    void Start() {
        baseName = lastCable.name;
    }

    void Update()
    {
        float distanceTraveled = Mathf.Abs(GameObjectAccess.Player.gameObject.transform.position.y);
        if(distanceTraveled - (cablesSoFar * cableDistance) >= cableDistance) {
            GameObject newCable = Instantiate(lastCable);

            Vector3 lastLocal = lastCable.transform.localPosition;

            newCable.transform.SetParent(lastCable.transform.parent);
            newCable.transform.localPosition = new Vector3(lastLocal.x, lastLocal.y + cableDistance, lastLocal.z);

            cablesSoFar++;
            newCable.name = baseName + cablesSoFar;
            lastCable = newCable;

        }

    }
}
