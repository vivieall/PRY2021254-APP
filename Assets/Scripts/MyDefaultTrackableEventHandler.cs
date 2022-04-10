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
        var canvasComponents = Canvas.GetComponentsInChildren<Canvas>(false);
        //Debug.Log(Canvas.transform.childCount);
        if (Canvas.transform.childCount ==1)
        {
            foreach (var component in canvasComponents)
                component.enabled = true;
        }
        else
        {
            for (int i = 0; i < Canvas.transform.childCount; i++)
            {
                Canvas.GetComponent<Canvas>().enabled = true;
                if (Canvas.transform.GetChild(i).GetSiblingIndex()== transform.GetSiblingIndex() - 1)
                {
                    //Debug.Log(Canvas.transform.GetChild(transform.GetSiblingIndex() - 1).GetSiblingIndex());
                    Canvas.transform.GetChild(transform.GetSiblingIndex() - 1).gameObject.SetActive(true);
                }
                else
                {
                    Canvas.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
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
