using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FocusCamera : MonoBehaviour
{
	private bool mVuforiaStarted = false;

	void Start()
    {
        VuforiaARController vuforia = VuforiaARController.Instance;

        if (vuforia != null)
            vuforia.RegisterVuforiaStartedCallback(StartAfterVuforia);
    }

    private void StartAfterVuforia()
    {
        mVuforiaStarted = true;
        SetAutofocus();
    }

    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            //App resumend
            if(mVuforiaStarted)
            {
                //App resumed and vuforia already started
                //but lets start it again...
                SetAutofocus(); //this is done because some android devices lose the auto focus after resume
                //this was a bug in vuforia 4 and 5. I havent' checked 6, but the code is hamless anyway
            }
        }
    }

    private void SetAutofocus()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("SetAutofocus set");
        }
        else
        { 
            // never actually seen a device that doesnt' support this, but just in case
            Debug.Log("this device doesn't support autofocus");
        
        }

    }
}