using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    /**
        Finds the first child of this object with a certain tag, null otherwise
    */
    public static GameObject findChildWithTag(this GameObject parent, string tag) {
        foreach(Transform child in parent.transform) {
            if(child.tag == tag) {
                return child.gameObject;
            }
        }

        return null;
    }

    public static float easeOutQuart(float x) => 1 - Mathf.Pow(1 - Mathf.Clamp01(x), 4);
}
