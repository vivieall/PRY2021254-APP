using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using System;
using System.Linq; 

public class SceneUIManager : MonoBehaviour
{
    [Header("Starting UI")]
    [SerializeField] private GameObject startingUI;
    private GameObject currentUI;
    [Header("Scene Manager")]
    [SerializeField] private GameObject m_LoguinUI;
    [SerializeField] private GameObject m_RegisterUI;
    [SerializeField] private GameObject m_PerfilNiñoCrearUI;
    [SerializeField] private GameObject m_PerfilesGuardadosUI = null;
    [SerializeField] private GameObject m_ActualizarDatosUI = null;
    [SerializeField] private GameObject m_VerDatosCuidadorUI = null;
    [SerializeField] private GameObject m_SeleccionarCategoriaUI = null;
    [SerializeField] private GameObject m_SeleccionarTemaUI = null;
    [SerializeField] private GameObject m_SeleccionarTematicaUI = null;
    [SerializeField] private GameObject m_AnimalesFiltroUI = null;
    [SerializeField] private GameObject m_PersonajesFiltroUI = null;
    [SerializeField] private GameObject m_VariedadesFiltroUI = null;
    [SerializeField] private GameObject m_NivelesCompletosUI = null;
    [SerializeField] private GameObject m_PerfilNinoModificarsUI = null;
    [SerializeField] private GameObject m_ListaPersonalizadaUI = null;
    [SerializeField] private GameObject m_ListaLikesUI = null;
    [SerializeField] private GameObject m_PerfilNinoVistaDatosUI;
    [SerializeField] private GameObject m_ModificarListaPersonalizUI;
    [SerializeField] private GameObject m_HistorialUI;
    [SerializeField] private GameObject m_Tema_MathUI;
    [SerializeField] private GameObject m_Chooselvl_MathUI;
    [SerializeField] private GameObject m_Tema_CommUI;
    [SerializeField] private GameObject m_Chooselvl_CommUI;
    [SerializeField] private GameObject m_FiltroUI;
    private ArrayList AllUIs;
    GuardianData datosUsuarioLogeado = new GuardianData();

    #region Register Guardian
    [Header("Register Guardian Inputs")]
    [SerializeField] private InputField m_InputCorreo;
    [SerializeField] private InputField m_InputContrasena;
    [SerializeField] private InputField m_InputContrasenaConf;
    [SerializeField] private InputField m_InputNombre;
    [SerializeField] private InputField m_InputApellido;
    [SerializeField] private InputField m_InputFechaDia;
    [SerializeField] private InputField m_InputFechaMes;
    [SerializeField] private InputField m_InputFechaAnio;
    [SerializeField] private InputField m_InputUsuario;

    #endregion

    #region Update Guardian
    [Header("Update Guardian Inputs")]
    [SerializeField] private InputField m_InputCorreoUpdate;
    [SerializeField] private InputField m_InputContrasenaActualUpdate;
    [SerializeField] private InputField m_InputContrasenaNuevaUpdate;
    [SerializeField] private InputField m_InputContrasenaNuevaConfUpdate;
    [SerializeField] private InputField m_InputNombreUpdate;
    [SerializeField] private InputField m_InputApellidoUpdate;
    [SerializeField] private InputField m_InputFechaDiaUpdate;
    [SerializeField] private InputField m_InputFechaMesUpdate;
    [SerializeField] private InputField m_InputFechaAnioUpdate;

    #endregion



    #region Login
    [Header("Login Inputs")]
    [SerializeField] private InputField m_InputContrasenaLogin;
    [SerializeField] private InputField m_InputUsuarioLogin;
    public InputField m_PasswordInputLogin;
    public InputField m_UserInputLogin;
    #endregion

    #region Register
    [Header("Register Inputs")]
    [SerializeField] private InputField m_Username;
    [SerializeField] private InputField m_Email;
    [SerializeField] private InputField m_Password;
    [SerializeField] private InputField m_ConfirmPassword;
    [SerializeField] private Text m_ErrorText;
    



    #endregion
    [SerializeField] private Toggle toggleSesion;
    private NetworkManager m_NetworkManager;
    
    [Header("Direcciones de corre")]
    public string[] Emails;
    public bool cuentaRegistradaConExito;
    public int MaxLenght;
    public GameObject[] sintomasCheckbox;

    public GameObject[] botonesNinos;
    public Text[] nombresNinos;

    #region Register Child
    [Header("Register Child Inputs")]
    [SerializeField] private InputField m_InputNombreChild;
    [SerializeField] private InputField m_InputApellidoChild;
    [SerializeField] private InputField m_InputFechaDiaChild;
    [SerializeField] private InputField m_InputFechaMesChild;
    [SerializeField] private InputField m_InputFechaAnioChild;
    [SerializeField] private InputField m_GeneroChild;

    #endregion


    #region Update Child
    [Header("Register Child Inputs")]
    [SerializeField] private InputField m_InputNombreChildUpdate;
    [SerializeField] private InputField m_InputApellidoChildUpdate;
    [SerializeField] private InputField m_InputFechaDiaChildUpdate;
    [SerializeField] private InputField m_InputFechaMesChildUpdate;
    [SerializeField] private InputField m_InputFechaAnioChildUpdate;
    [SerializeField] private InputField m_GeneroChildUpdate;
    [SerializeField] private Text m_ErrorTextLogin;
    #endregion

    private bool sesionIniciada;
    private string id_guardian;
    private string nivelAutismoChild;
    private string generoChild;
    private string avatarChild;
    private string nivelAutismoChildUpdate;
    private string generoChildUpdate;
    private string avatarChildUpdate;
    private bool[] sintomas = new bool[] { false, false, false, false, false, false, false };
    private bool[] sintomasUpdate = new bool[] { false, false, false, false, false, false, false };
    private int cantNinos;
    private ChildData[] ninosGuardian = { };
    private ChildData loggedChild;

    


    void Start()
    {
        // No es muy bonito, pero es más bonito que  las 350 lineas de copypaste para cada show

        // TODO: Create UILayerComponent or something similar that automatically "registers" itself into this AllUIs' array list
        // instead of manually adding every reference.
        AllUIs = new ArrayList();
        AllUIs.Add(m_LoguinUI);
        AllUIs.Add(m_RegisterUI);
        AllUIs.Add(m_PerfilNiñoCrearUI);
        AllUIs.Add(m_PerfilesGuardadosUI);
        AllUIs.Add(m_ActualizarDatosUI);
        AllUIs.Add(m_VerDatosCuidadorUI);
        AllUIs.Add(m_SeleccionarCategoriaUI);
        AllUIs.Add(m_SeleccionarTemaUI);
        AllUIs.Add(m_SeleccionarTematicaUI);
        AllUIs.Add(m_AnimalesFiltroUI);
        AllUIs.Add(m_PersonajesFiltroUI);
        AllUIs.Add(m_VariedadesFiltroUI);
        AllUIs.Add(m_NivelesCompletosUI);
        AllUIs.Add(m_PerfilNinoModificarsUI);
        AllUIs.Add(m_ListaPersonalizadaUI);
        AllUIs.Add(m_ListaLikesUI);
        AllUIs.Add(m_PerfilNinoVistaDatosUI);
        AllUIs.Add(m_ModificarListaPersonalizUI);
        AllUIs.Add(m_HistorialUI);
        AllUIs.Add(m_Tema_MathUI);
        AllUIs.Add(m_Chooselvl_MathUI);
        AllUIs.Add(m_Tema_CommUI);
        AllUIs.Add(m_Chooselvl_CommUI);
        AllUIs.Add(m_FiltroUI);

        foreach (GameObject ui in AllUIs)
        {
            ui.SetActive(false);
        }

        sesionIniciada = false;
        m_NetworkManager = FindObjectOfType<NetworkManager>();
        Debug.Log("test");

        /*
        Debug.Log(toggleSesion.isOn);
        if (PlayerPrefs.HasKey("toggleIsOn") == true)
        {
            toggleSesion.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("toggleIsOn"));
            if (toggleSesion.isOn)
            {
                m_PasswordInputLogin.text = PlayerPrefs.GetString("SavePasswordToggle_Data", "");
                m_UserInputLogin.text = PlayerPrefs.GetString("SaveUserToggle_Data", "");

            }
            else
            {
                m_PasswordInputLogin.text =  "";
                m_UserInputLogin.text = "";
            }
        }
        */
        //Display Reqired Screen on Scene Load depending on State
        var state = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>().GetState();
        currentUI = startingUI;
        switch (state)
        {
            case 0:
                ShowUI(startingUI);
                break;
            case 1:
                ShowUI(m_SeleccionarCategoriaUI);
                break;
            case 2:
                ShowUI(m_Chooselvl_MathUI);
                break;
            case 3:
                ShowUI(m_Chooselvl_CommUI);
                break;
        }
    }


    public void setGenero(string genero)
    {
        generoChild = genero;
    }


    public void setNivelAutismo(string nivel)
    {
        nivelAutismoChild = nivel;
    }


    public void setSintoma(int idx)
    {
        sintomas[idx] = !sintomas[idx];
    }

    public void setAvatar(string avatarCode)
    {
        avatarChild = avatarCode;
    }

    public void setGeneroUpdate(string genero)
    {
        generoChildUpdate = genero;
    }


    public void setNivelAutismoUpdate(string nivel)
    {
        nivelAutismoChildUpdate = nivel;
    }


    public void setSintomaUpdate(int idx)
    {
        sintomasUpdate[idx] = !sintomas[idx];
    }

    public void setAvatarUpdate(string avatarCode)
    {
        avatarChildUpdate = avatarCode;
    }


    //<summary>
    //Orden para enviar datos
    //user
    //email
    //pass
    //<summary>

    public void submitLogin()
    {

        if (m_PasswordInputLogin.text == "" || m_UserInputLogin.text == "")
        {
            m_ErrorText.text = "Error 444: Verifica que ningun campo este vacio";
            return;
        }

        m_NetworkManager.LoginUser(m_UserInputLogin.text, m_PasswordInputLogin.text, delegate (Response response)
        {
            m_ErrorText.text = "Logueando espere un momento";
            m_ErrorText.text = response.message;

            if (response.done)
            {
                if (toggleSesion.isOn)
                {
                    PlayerPrefs.SetString("SavePasswordToggle_Data", m_PasswordInputLogin.text);
                    PlayerPrefs.SetString("SaveUserToggle_Data", m_UserInputLogin.text);
                    var valueSave = Convert.ToInt32(toggleSesion.isOn);
                    PlayerPrefs.SetInt("toggleIsOn", valueSave);
                }

                        // m_LoguinUI.SetActive(false);
                        //m_PerfilNiñoUI.SetActive(true);
                    }
        });
        //m_LoguinUI.SetActive(false);
        //m_PerfilNiñoUI.SetActive(true);
    }

    public void logout()
    {
        id_guardian = null;
        sesionIniciada = false;
        resetChildren();
        ShowLoguin();
    }


    public void submitLogin2()
    {
        if (m_InputContrasenaLogin.text == "" || m_InputUsuarioLogin.text == "")
        {
            return;
        }
        CallLoginApi(m_InputUsuarioLogin.text, m_InputContrasenaLogin.text, delegate (LoginResponse response)
        {
            m_ErrorTextLogin.text = "Logueando espere un momento";
            
            if (response.idGuardian != null)
            {
                if (toggleSesion.isOn)
                {
                    PlayerPrefs.SetString("SavePasswordToggle_Data", m_InputContrasenaLogin.text);
                    PlayerPrefs.SetString("SaveUserToggle_Data", m_InputUsuarioLogin.text);
                    var valueSave = Convert.ToInt32(toggleSesion.isOn);
                    PlayerPrefs.SetInt("toggleIsOn", valueSave);
                }
                id_guardian = response.idGuardian;
                sesionIniciada = true;
                datosUsuarioLogeado.username = response.username;
                datosUsuarioLogeado.password = response.password;
                datosUsuarioLogeado.email = response.email;
                datosUsuarioLogeado.names = response.names;
                datosUsuarioLogeado.lastNames = response.lastNames;
                datosUsuarioLogeado.birthday = response.birthday;


                ShowPerfilsGuardados();

                //UnityEngine.SceneManagement.SceneUIManager.LoadScene("m_PerfilNiñoUI");
                // m_LoguinUI.SetActive(false);
                //m_PerfilNiñoUI.SetActive(true);
            }
            else
            {
                m_ErrorTextLogin.text = response.message;
            }
        });
    }

    void blankRegisterSpace()
    {
        m_Username.text = "";
        m_Email.text = "";
        m_Password.text = "";
        m_ConfirmPassword.text = "";
    }

    void blankRegisterChildSpace()
    {
        m_InputNombreChild.text = "";
        m_InputApellidoChild.text = "";
        m_InputFechaDiaChild.text = "";
        m_InputFechaMesChild.text = "";
        m_InputFechaAnioChild.text = "";
    }

    public void SubmitRegister()
    {
        foreach (string emailSet in Emails)
        {
            if (m_Email.text.Contains(emailSet)) {
                cuentaRegistradaConExito = true;
                if (m_Username.text == "" || m_Email.text == "" || m_Password.text == "" || m_ConfirmPassword.text == "")
                {
                    m_ErrorText.text = "Error 444: Verifica que ningun campo este vacio";
                    return;
                }

                if (m_Password.text == m_ConfirmPassword.text)
                {
                    if (m_Password.text.Length >= MaxLenght) {
                        m_ErrorText.text = "Procesando informacion por favor espera un momento";
                        m_NetworkManager.SubmitRegister(m_Username.text, m_Email.text, m_Password.text, delegate (Response response)
                        {

                            m_ErrorText.text = response.message;
                            if (response.done == true)
                            {
                                ////ESCRIBIR CODIGO DE ACEPTACION AL REGISTRARSE
                                cuentaRegistradaConExito = false;
                                blankRegisterSpace();
                                print("Cuenta creada con exito");
                            }
                            else
                            {
                                ///ACCION AL NO REGISTRAR CUENTA

                            }
                        });
                    }
                    else
                    {
                        m_ErrorText.text = "Tu contraseña debe contener minimo 8 caracteres";
                    }
                }
                else
                {
                    m_ErrorText.text = "Error 565: Hay datos que no son similares intentalo de nuevo";
                    return;
                }
            }

            if (!m_Email.text.Contains(emailSet) && !cuentaRegistradaConExito)
            {
                m_ErrorText.text = "Error 877: Lo sentimos pero no se puede leer un email valido";
            }
        }
    }

    public void DeleteChild()
    {
        CallGetRequestDeleteChildApi(loggedChild.idChild, delegate (DeleteResponse response)
        { 
            if(response.idResponse == 1)
            {

                ShowPerfilsGuardados();
            }
        
        });

    }


    public void SubmitRegister2()
    {
        foreach (string emailSet in Emails)
        {
            if (m_InputCorreo.text.Contains(emailSet))
            {
                cuentaRegistradaConExito = true;
                if (m_InputUsuario.text == "" || m_InputCorreo.text == "" || m_InputContrasena.text == "" || m_InputContrasenaConf.text == "")
                {
                    m_ErrorText.text = "Error 444: Verifica que ningun campo este vacio";
                    return;
                }

                if (m_InputContrasena.text == m_InputContrasenaConf.text)
                {
                    if (m_InputContrasena.text.Length >= MaxLenght)
                    {
                        m_ErrorText.text = "Procesando informacion por favor espera un momento";
                        CallRegisterGuardianApi(m_InputUsuario.text, m_InputContrasena.text, m_InputCorreo.text,
                            m_InputNombre.text, m_InputApellido.text, m_InputFechaAnio.text + "-" + m_InputFechaMes.text + "-" + m_InputFechaDia.text,
                            delegate (GuardianResponse response)
                        {

                            m_ErrorText.text = response.message;
                            if (response.idResponse >= 0)
                            {
                                ////ESCRIBIR CODIGO DE ACEPTACION AL REGISTRARSE
                                cuentaRegistradaConExito = false;
                                blankRegisterSpace();
                                print("Cuenta creada con exito");
                                ShowLoguin();
                            }
                            else
                            {
                                ///ACCION AL NO REGISTRAR CUENTA

                            }
                        });
                    }
                    else
                    {
                        m_ErrorText.text = "Tu contraseña debe contener minimo 8 caracteres";
                    }
                }
                else
                {
                    m_ErrorText.text = "Error 565: Hay datos que no son similares intentalo de nuevo";
                    return;
                }
            }

            if (!m_InputCorreo.text.Contains(emailSet) && !cuentaRegistradaConExito)
            {
                m_ErrorText.text = "Error 877: Lo sentimos pero no se puede leer un email valido";
            }
        }
    }

    public void loginChild(int idx)
    {
        loggedChild = ninosGuardian[idx];
        ShowCategoria();
    }

    public void getChildren()
    {
        CallGetRequestChildrenApi(id_guardian, delegate (ChildData[] response)
        {

            foreach (ChildData c in response){
                Debug.Log(c.idChild);
            }
            ninosGuardian = response;
            cantNinos = response.Length;

            for (int i = 0; i < cantNinos; i++)
            {
                botonesNinos[i].SetActive(true);
                nombresNinos[i].text = response[i].names;
                nombresNinos[i].gameObject.SetActive(true);
            }
        });
    }

    public void resetChildren()
    {
        for (int i = 0; i < 6; i++)
        {
            botonesNinos[i].SetActive(false);
            nombresNinos[i].gameObject.SetActive(false);
        }
    }

    public void SubmitUpdateGuardian()
    {
        foreach (string emailSet in Emails)
        {
            if (m_InputCorreoUpdate.text.Contains(emailSet))
            {
                cuentaRegistradaConExito = true;
                if ( m_InputCorreoUpdate.text == "" || m_InputContrasenaActualUpdate.text == "" || m_InputContrasenaNuevaUpdate.text == "" || m_InputContrasenaNuevaConfUpdate.text == "")
                {
                    m_ErrorText.text = "Error 444: Verifica que ningun campo este vacio";
                    return;
                }

                if (m_InputContrasenaNuevaUpdate.text == m_InputContrasenaNuevaConfUpdate.text)
                {
                    if (m_InputContrasena.text.Length >= MaxLenght)
                    {
                        m_ErrorText.text = "Procesando informacion por favor espera un momento";
                        CallUpdateGuardianApi(id_guardian, m_InputContrasenaActualUpdate.text, m_InputContrasenaNuevaUpdate.text, m_InputCorreoUpdate.text,
                            m_InputNombreUpdate.text, m_InputApellidoUpdate.text, m_InputFechaAnioUpdate.text + "-" + m_InputFechaMesUpdate.text + "-" + m_InputFechaDiaUpdate.text,
                            delegate (GuardianData response)
                            {

                                if (response.idGuardian != null)
                                {
                                    print("Cuenta actualizada con exito");
                                    datosUsuarioLogeado.username = response.username;
                                    datosUsuarioLogeado.password = response.password;
                                    datosUsuarioLogeado.email = response.email;
                                    datosUsuarioLogeado.names = response.names;
                                    datosUsuarioLogeado.lastNames = response.lastNames;
                                    datosUsuarioLogeado.birthday = response.birthday;
                                    ShowPerfilsGuardados();
                                }
                                else
                                {
                                    ///ACCION AL NO REGISTRAR CUENTA

                                }
                            });
                    }
                    else
                    {
                        m_ErrorText.text = "Tu contraseña debe contener minimo 8 caracteres";
                    }
                }
                else
                {
                    m_ErrorText.text = "Error 565: Hay datos que no son similares intentalo de nuevo";
                    return;
                }
            }

            if (!m_InputCorreoUpdate.text.Contains(emailSet) && !cuentaRegistradaConExito)
            {
                m_ErrorText.text = "Error 877: Lo sentimos pero no se puede leer un email valido";
            }
        }
    }

    public void ShowUI_GoBack(GameObject UIToShow) {
        foreach(GameObject m_ui in AllUIs) {
            m_ui.SetActive(false);
		}
        if (UIToShow) 
            UIToShow.SetActive(true);
	}

    public void ShowUI(GameObject UIToShow) {
        currentUI.SetActive(false);
        UIToShow.SetActive(true);
        currentUI = UIToShow;
        print(currentUI);
    }

	//Se puede mejorar estas funciones creando una que solo reciba la funcion especifica y que solo cambie el que se ponga true 
    //D.L.: Si se pudo mejorar
	public void ShowLoguin(){
        ShowUI(m_LoguinUI);
    }
    public void ShowRegister(){
        ShowUI(m_RegisterUI);
    }
    public void ShowPerfilNiño(){
        blankRegisterChildSpace();
        ShowUI(m_PerfilNiñoCrearUI);
    }
    public void ShowPerfilsGuardados(){
        resetChildren();
        getChildren();
        ShowUI(m_PerfilesGuardadosUI);
    }
    public void ShowActualizarDatos(){
        setProfileData();
        ShowUI(m_ActualizarDatosUI);
    }
    public void ShowVerDatosCuidador(){
        ShowUI(m_VerDatosCuidadorUI);
    }
    public void ShowCategoria(){
        ShowUI(m_SeleccionarCategoriaUI);
    }
    public void ShowTemaMath(){
        ShowUI(m_Tema_MathUI);
    }
    public void ShowTemaComm(){
        ShowUI(m_Tema_CommUI);
    }
    public void ShowNivelesCompletos(){
        ShowUI(m_NivelesCompletosUI);
    }
    public void ShowNinoVistaDatos(){
        ShowUI(m_PerfilNinoVistaDatosUI);
    }
    public void ShowPerfilNinoModificar(){
        setChildPerfil();
        ShowUI(m_PerfilNinoModificarsUI);
    }
    public void ShowListaPersonalizada(){
        ShowUI(m_ListaPersonalizadaUI);
    }
    public void ShowModificarListaPersonalizada(){
        ShowUI(m_ModificarListaPersonalizUI);
    }
    public void ShowHistorial(){
        ShowUI(m_HistorialUI);
    }
    public void ShowChooselvlMath(){
        ShowUI(m_Chooselvl_MathUI);
    }
    public void ShowChooselvlComm(){
        ShowUI(m_Chooselvl_CommUI);
    }
    public void ShowTematica(){
        ShowUI(m_SeleccionarTematicaUI);
    }
    public void ShowAnimalesFiltro(){
        ShowUI(m_AnimalesFiltroUI);
    }
    public void ShowPersonajesFiltro(){
        ShowUI(m_PersonajesFiltroUI);
    }
    public void ShowVariedadesFiltro(){
        ShowUI(m_VariedadesFiltroUI);
    }
        
    // Codigo de conexion a la bd se debe enviar a otro script
    public void CallRegister(string user, string pass, string email, string names, string lastnames, string birthday
       , Action<Response> response)
    {
        StartCoroutine(Register(user, pass, email, names, lastnames, birthday, response));
    }

    IEnumerator Register(string user, string pass, string email, string names, string lastnames, string birthday, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", pass);
        form.AddField("names", names);
        form.AddField("lastnames", lastnames);
        form.AddField("username", user);
        form.AddField("birthday", birthday);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;

        Debug.Log(www.text);
        response(JsonUtility.FromJson<Response>(www.text));
    }



    private void CallRegisterGuardianApi(string user, string pass, string email, string names, string lastnames, string birthday, Action<GuardianResponse> response)
    {
        GuardianData gd = new GuardianData();
        gd.email = email;
        gd.password = pass;
        gd.username = user;
        gd.names = names;
        gd.lastNames = lastnames;
        gd.birthday = birthday;
        string json = JsonUtility.ToJson(gd);
        Debug.Log(json);
        StartCoroutine(PostRequestRegisterGuardian("https://teapprendo.herokuapp.com/guardians/create", json, response));
    }

    IEnumerator PostRequestRegisterGuardian(string url, string json, Action<GuardianResponse> response)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<GuardianResponse>(uwr.downloadHandler.text));
        }
    }


    


    private void CallUpdateGuardianApi(string id_guardian, string pass, string newPass, string email, string names, string lastnames, string birthday, Action<GuardianData> response)
    {
        GuardianData gd = new GuardianData();
        gd.email = email;
        gd.password = pass;
        gd.names = names;
        gd.lastNames = lastnames;
        gd.birthday = birthday;
        gd.newPassword = newPass;
        gd.idGuardian = id_guardian;
        string json = JsonUtility.ToJson(gd);
        Debug.Log(json);
        StartCoroutine(PutRequestUpdateGuardian("https://teapprendo.herokuapp.com/guardians/update", json, response));
    }

    IEnumerator PutRequestUpdateGuardian(string url, string json, Action<GuardianData> response)
    {
        var uwr = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<GuardianData>(uwr.downloadHandler.text));
        }
    }

    private void CallGetRequestGuardianApi(string id, Action<GuardianData> response)
    {
        StartCoroutine(GetRequestGuardian("https://teapprendo.herokuapp.com/guardians/listByIdGuardian?idGuardian=" + id, response ));    }

    IEnumerator GetRequestGuardian(string url, Action<GuardianData> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<GuardianData>(uwr.downloadHandler.text));
        }
    }

    private void CallGetRequestDeleteChildApi(string id, Action<DeleteResponse> response)
    {
        StartCoroutine(GetRequestDeleteChild("https://teapprendo.herokuapp.com/children/delete?idChild=" + loggedChild.idChild, response));
    }

    IEnumerator GetRequestDeleteChild(string url, Action<DeleteResponse> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<DeleteResponse>(uwr.downloadHandler.text));
        }
    }

    public void setProfileData()
    {

        m_InputCorreoUpdate.text = datosUsuarioLogeado.email;
        m_InputNombreUpdate.text = datosUsuarioLogeado.names;
        m_InputApellidoUpdate.text = datosUsuarioLogeado.lastNames;
        m_InputFechaDiaUpdate.text = datosUsuarioLogeado.birthday.Substring(8, 2);
        m_InputFechaMesUpdate.text = datosUsuarioLogeado.birthday.Substring(5, 2);
        m_InputFechaAnioUpdate.text = datosUsuarioLogeado.birthday.Substring(0, 4);

    }


    public void setChildPerfil()
    {
        m_InputNombreChildUpdate.text = loggedChild.names;
        m_InputApellidoChildUpdate.text = loggedChild.lastNames;
        m_InputFechaDiaChildUpdate.text = loggedChild.birthday.Substring(8, 2);
        m_InputFechaMesChildUpdate.text = loggedChild.birthday.Substring(5, 2);
        m_InputFechaAnioChildUpdate.text = loggedChild.birthday.Substring(0, 4);
        nivelAutismoChildUpdate = loggedChild.asdLevel;
        generoChildUpdate = loggedChild.gender;
        avatarChildUpdate = loggedChild.avatar;

        /*
        for (int i = 0; i < loggedChild.symptoms.Length; i++)
        {
            sintomasUpdate[loggedChild.symptoms[i]] = true;
        }
        */

    }

    private class GuardianData
    {
        public string username;
        public string password;
        public string newPassword;
        public string email;
        public string names;
        public string lastNames;
        public string birthday;
        public string idGuardian;
    }
    private class GuardianResponse
    {
        public int idResponse;
        public string message;
    }

    public void SubmitRegisterChild()
    {
        int[] sintomas2 = new int[] { };

        
        for (int i = 0; i < 7; i++)
        {
            if (sintomas[i] == true)
            {
                sintomas2 = sintomas2.Concat(new int[] { i+1 }).ToArray();
            }
        }
        CallRegisterChildApi(id_guardian, m_InputNombreChild.text, m_InputApellidoChild.text, m_InputFechaAnioChild.text + "-" + m_InputFechaMesChild.text + "-" + m_InputFechaDiaChild.text, generoChild, nivelAutismoChild, sintomas2,
            delegate (ChildData response)
            {

                Debug.Log(response);
                if (response.idChild != null)
                {
                    print("Niño creado con exito");
                    ShowPerfilsGuardados();
                }
                else
                {
                    ///ACCION AL NO REGISTRAR CUENTA
                }
            });
    }

    public void SubmitUpdateChild()
    {
        int[] sintomas2 = new int[] { };


        for (int i = 0; i < 7; i++)
        {
            if (sintomasUpdate[i] == true)
            {
                sintomas2 = sintomas2.Concat(new int[] { i + 1 }).ToArray();
            }
        }
        CallUpdateChildApi(loggedChild.idChild, m_InputNombreChildUpdate.text, m_InputApellidoChildUpdate.text, m_InputFechaAnioChildUpdate.text + "-" + m_InputFechaMesChildUpdate.text + "-" + m_InputFechaDiaChildUpdate.text, generoChildUpdate, nivelAutismoChildUpdate, sintomas2,
            delegate (ChildData response)
            {

                Debug.Log(response);
                if (response.idChild != null)
                {
                    print("Niño actualizado con exito");
                    ShowPerfilsGuardados();
                }
                else
                {
                    ///ACCION AL NO REGISTRAR CUENTA
                }
            });
    }


    public void CallLogin(string user, string pass, Action<Response> response)
    {
        StartCoroutine(Login(user, pass,response));
    }

    IEnumerator Login(string user, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("password", pass);
        form.AddField("username", user);
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;

        Debug.Log(www.text);
        response(JsonUtility.FromJson<Response>(www.text));
    }


    private void CallLoginApi(string user, string pass, Action<LoginResponse> response)
    {
        LoginData ld = new LoginData();
        ld.username = user;
        ld.password = pass;
        string json = JsonUtility.ToJson(ld);
        StartCoroutine(PostRequestLogin("https://teapprendo.herokuapp.com/guardians/login", json, response));
    }

    IEnumerator PostRequestLogin(string url, string json, Action<LoginResponse> response)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<LoginResponse>(uwr.downloadHandler.text));
        }
    }

    private class LoginData
    {
        public string username;
        public string password;
    }

    private class LoginResponse
    {
        public int idResponse;
        public string message;

        public string idGuardian;
        public string username;
        public string password;
        public string email;
        public string names;
        public string lastNames;
        public string birthday;
    }

    private class DeleteResponse
    {
        public int idResponse;
        public string message;
    }






        private void CallRegisterChild(string guardian_id, string names, string lastnames, string birthday, string gender, string asdlevel, bool[] symptoms, Action<Response> response)
    {
        StartCoroutine(RegisterChild(guardian_id, names, lastnames, birthday, gender, asdlevel, symptoms, response));
    }

    IEnumerator RegisterChild(string guardian_id, string names, string lastnames, string birthday, string gender, string asdlevel, bool[] symptoms, Action<Response> response)
    {

        string sintomasStr = "";
        for (int i = 0; i < 7; i++)
        {
            if (symptoms[i] == true)
            {
                sintomasStr += "1";
            }
            else
            {
                sintomasStr += "0";
            }
        }

        Debug.Log(sintomasStr);
        WWWForm form = new WWWForm();
        form.AddField("guardian_id", guardian_id);
        form.AddField("gender", gender);
        form.AddField("names", names);
        form.AddField("lastnames", lastnames);
        form.AddField("asdlevel", asdlevel);
        form.AddField("symptoms", sintomasStr);
        form.AddField("birthday", birthday);
        WWW www = new WWW("http://localhost/sqlconnect/registerChild.php", form);
        yield return www;
        response(JsonUtility.FromJson<Response>(www.text));
    }
    [Serializable]
    private class ChildData
    {
        public string idChild;
        public string asdLevel;
        public string avatar;
        public string gender;
        public int idGuardian;
        public string lastNames;
        public string names;
        public string birthday;
        public int[] symptoms;
        public string password;
    }

    private class Children
    {
        public ChildData[] children;
    }

    private void CallRegisterChildApi(string guardian_id, string names, string lastnames, string birthday, string gender, string asdlevel, int[] symptoms, Action<ChildData> response)
    {
        ChildData cd = new ChildData();
        cd.idGuardian = Int32.Parse(guardian_id);
        cd.avatar = avatarChild;
        cd.names = names;
        cd.lastNames = lastnames;
        cd.birthday = birthday;
        cd.asdLevel = asdlevel;
        cd.symptoms = symptoms;
        cd.gender = gender;
        string json = JsonUtility.ToJson(cd);
        Debug.Log(json);
        StartCoroutine(PostRequestRegisterChild("https://teapprendo.herokuapp.com/children/create", json, response));
    }

    IEnumerator PostRequestRegisterChild(string url, string json, Action<ChildData> response)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<ChildData>(uwr.downloadHandler.text));
        }
    }


    private void CallUpdateChildApi(string id, string names, string lastnames, string birthday, string gender, string asdlevel, int[] symptoms, Action<ChildData> response)
    {
        ChildData cd = new ChildData();
        cd.idChild = id;
        cd.avatar = avatarChild;
        cd.names = names;
        cd.lastNames = lastnames;
        cd.birthday = birthday;
        cd.asdLevel = asdlevel;
        cd.symptoms = symptoms;
        cd.gender = gender;
        string json = JsonUtility.ToJson(cd);
        Debug.Log(json);
        StartCoroutine(PutRequestUpdateChild("https://teapprendo.herokuapp.com/children/update", json, response));
    }

    IEnumerator PutRequestUpdateChild(string url, string json, Action<ChildData> response)
    {
        var uwr = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<ChildData>(uwr.downloadHandler.text));
        }
    }
    private void CallGetRequestChildrenApi(string id, Action<ChildData[]> response)
    {
        StartCoroutine(GetRequestChildren("https://teapprendo.herokuapp.com/children/listByIdGuardian?idGuardian=" + id, response));
    }

    IEnumerator GetRequestChildren(string url, Action<ChildData[]> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            response(JsonHelper.FromJson<ChildData>(fixJson(uwr.downloadHandler.text)));
        }
    }
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}