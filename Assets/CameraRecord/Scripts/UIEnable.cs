using UnityEngine;
using System.Collections;

public class UIEnable : MonoBehaviour 
{
    public UnityEngine.Canvas uiCanvas;

	void Start () 
    {
	    if (uiCanvas == null)
        {
            uiCanvas = GetComponent<UnityEngine.Canvas>();

            if (uiCanvas == null)
            {
                Debug.LogError("Missing reference to <UnityEngine.Canvas>. ");
                enabled = false;
                return;
            }
        }
	}
	

	void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.LeftControl))
        {
            uiCanvas.enabled = !uiCanvas.enabled;
        }
	}


}
