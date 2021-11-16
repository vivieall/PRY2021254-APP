/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using System;
using System.Threading.Tasks;
//using Firebase.Database;
/*
public class FirebaseController
{
    FirebaseApp firebaseApp = null;
    internal Firebase.Auth.FirebaseAuth auth = null;
    internal Firebase.Auth.FirebaseUser user = null;
    internal DatabaseReference database = null;

    public FirebaseController()
    {
		// Esto es lo que va a detenerminar si se puede usar o no firebase, en el dispositivo que sea.
        DependencyStatus dependencyStatus = FirebaseApp.CheckDependencies();
        if(dependencyStatus == DependencyStatus.Available)
        {
			// Esto es para crear el objeto FirebaseApp, necesario para manejar los distintos metodos de firebase.
            firebaseApp = FirebaseApp.Create(new AppOptions() {
                ApiKey = "", //Clave de API web
                DatabaseUrl = new Uri(""), // URL de base de datos
                ProjectId = "", // ID del proyecto
                StorageBucket = "",
                AppId = "", // ID de la app
                MessageSenderId = ""
            });
			// para obtener el objeto Firebase.Auth.FirebaseAuth
            auth = Firebase.Auth.FirebaseAuth.GetAuth(firebaseApp);   
			// con esto recogemos si tiene algun usuario registrado
            user = auth.CurrentUser;
			// con esto recogemos la referencia a la base de datos, para poder hacer operaciones de escritura o lectura.
            database = FirebaseDatabase.GetInstance(firebaseApp).RootReference;
        }
    }

	// Registrar nuevo usuario
    public async RegisterNewUser(string email, string password)
    {
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                throw new Exception("Ha ocurrido un problema. Intentelo mas tarde.");
            }
            if (task.IsFaulted)
            {
                throw getLastInnterException(task.Exception);
            }
            user = task.Result;
        });
    }

	

	// Iniciar sesion
    public Task SignIn(string email, string password)
    {
        return auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                throw new Exception("Ha ocurrido un problema. Intentelo mas tarde.");
            }
            if (task.IsFaulted)
            {
                throw getLastInnterException(task.Exception);
            }
            user = task.Result;
        });
    }    
	
	// Cerrar sesion
    public void SignOut()
    {
        auth.SignOut();

        user = null;
    }



	//Obtener datos de una rama
    public Task<DataSnapshot> getDataOfEquipmentFromDataBase()
    {
        return database.Child($"users/{user.UserId}/asdasd").GetValueAsync();//Ver como llamar a los datos, ver nombre, en vez de asdasd

	//Obtener datos de una rama
    public Task<DataSnapshot> getDataOfShoppingCarFromDataBase()
    {
        return database.Child($"users/{user.UserId}/asdasd").GetValueAsync(); //Ver como llamar a los niños registrados, ver nombre, en vez de asdasd
    }
}*/