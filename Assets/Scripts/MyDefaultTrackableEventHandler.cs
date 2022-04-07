using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyDefaultTrackableEventHandler : DefaultTrackableEventHandler
{
    [SerializeField] public GameObject Canvas;
    [SerializeField] public GameObject PointObj;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        var canvasComponents = Canvas.GetComponentsInChildren<Canvas>(true);
        foreach (var component in canvasComponents)
            component.enabled = true;
        for (int i = 0; i < PointObj.transform.childCount; i++)
        {
            PointObj.transform.GetChild(i).GetComponent<DragToUIObj>().SetAnswer(this.gameObject);
            if (this.gameObject== PointObj.transform.GetChild(i).GetComponent<DragToUIObj>().GetAnswer() && PointObj.transform.GetChild(i).GetComponent<DragToUIObj>().GetDone())
            {
                this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }        
        }
        
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        var canvasComponents = Canvas.GetComponentsInChildren<Canvas>(false);
        foreach (var component in canvasComponents)
            component.enabled = false;
    }
}
