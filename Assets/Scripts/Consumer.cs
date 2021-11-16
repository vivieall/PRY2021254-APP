/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using System;
using System.Threading.Tasks;

//Sale problemas en Path de Java, tener en cuenta ello al momento de iniciar todo

//public class Consumer : using UnityEngine;

public class Consumer : MonoBehaviour {
    private FirebaseApp _app;
    void Start() {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       _app = Firebase.FirebaseApp.DefaultInstance;

    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
    }

    
}*/