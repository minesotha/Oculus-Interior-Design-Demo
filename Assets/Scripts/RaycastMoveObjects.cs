﻿using UnityEngine;
using System.Collections;

public class RaycastMoveObjects : MonoBehaviour {
    public float seeDistance = 10.0f;
    Light lastLigh = null;
    GameObject currTarget = null;
    GameObject lastDrag = null;
    //czy jest cos przeciagane?
    bool doLookUp = true;



    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && lastLigh != null)
        {
            lastLigh.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            MoveRaycast.MoveObject(lastLigh.gameObject);
            lastDrag = lastLigh.gameObject;
            doLookUp = false;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            lastDrag.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            doLookUp = true;
        }

      



        if (doLookUp == true)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, fwd, out hit, seeDistance))
            {
                currTarget = transform.gameObject;
                if (lastLigh != null && lastLigh != currTarget.GetComponent<Light>())
                {
                    lastLigh.enabled = false;

                }
                Debug.Log("Widzę: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<Light>() != null)
                {

                    lastLigh = hit.collider.gameObject.GetComponent<Light>();
                    lastLigh.enabled = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (lastLigh != null)
                    {
                        lastLigh.gameObject.GetComponent<ChooseFurnitureGUI>().setGui();
                    }
                }

            }
        }
    }
}