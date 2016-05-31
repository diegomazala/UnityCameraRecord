using UnityEngine;
using System.Collections;

public class UIWindowControl : MonoBehaviour 
{

    public GameObject[] uiObjects;
    public MonoBehaviour[] uiComponents;
    protected UnityEngine.UI.Image imagePanel = null;

    void Start()
    {
        imagePanel = GetComponent<UnityEngine.UI.Image>();
    }

    public void OnEnableUI(bool toogle)
    {
        foreach (GameObject go in uiObjects)
            go.SetActive(toogle);

        foreach (MonoBehaviour mb in uiComponents)
            mb.enabled = toogle;

        if (imagePanel)
            imagePanel.enabled = toogle;
    }


}
