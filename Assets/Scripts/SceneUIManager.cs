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
    [SerializeField] private GameObject m_PerfilesGuardadosUI= null;
    [SerializeField] private GameObject m_ActualizarDatosUI= null;
    [SerializeField] private GameObject m_VerDatosCuidadorUI= null;
    [SerializeField] private GameObject m_SeleccionarCategoriaUI= null;
    [SerializeField] private GameObject m_SeleccionarTemaUI= null;
    [SerializeField] private GameObject m_SeleccionarTematicaUI = null;
    [SerializeField] private GameObject m_AnimalesFiltroUI= null;
    [SerializeField] private GameObject m_PersonajesFiltroUI = null;
    [SerializeField] private GameObject m_VariedadesFiltroUI = null;
    [SerializeField] private GameObject m_NivelesCompletosUI= null;
    [SerializeField] private GameObject m_PerfilNinoModificarsUI= null;
    [SerializeField] private GameObject m_ListaPersonalizadaUI= null;
    [SerializeField] private GameObject m_PerfilNinoVistaDatosUI;
    [SerializeField] private GameObject m_ModificarListaPersonalizUI;
    [SerializeField] private GameObject m_HistorialUI;
    [SerializeField] private GameObject m_Tema_MathUI;
    [SerializeField] private GameObject m_Chooselvl_MathUI;
    [SerializeField] private GameObject m_Tema_CommUI;
    [SerializeField] private GameObject m_Chooselvl_CommUI;
    [SerializeField] private GameObject m_FiltroUI;
    private ArrayList AllUIs;


    [SerializeField]  private InputField m_InputCorreo;
    [SerializeField]  private InputField m_InputContrasena;
    [SerializeField]  private InputField m_InputNombre;
    [SerializeField]  private InputField m_InputApellido;
    [SerializeField]  private InputField m_InputFechaDia;
    [SerializeField]  private InputField m_InputFechaMes;
    [SerializeField]  private InputField m_InputFechaAnio;
    [SerializeField]  private InputField m_InputUsuario;


    [SerializeField] private InputField m_InputContrasenaLogin;
    [SerializeField] private InputField m_InputUsuarioLogin;

    #region Login
    [Header("Login Inputs")]
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
                            var valueSave = Convert.ToInt32 (toggleSesion.isOn);
                            PlayerPrefs.SetInt ("toggleIsOn", valueSave);
                        }

                        // m_LoguinUI.SetActive(false);
                        //m_PerfilNiñoUI.SetActive(true);
                    }
                });
                //m_LoguinUI.SetActive(false);
               //m_PerfilNiñoUI.SetActive(true);
    }

    public void submitLogin2()
    {
        if (m_InputContrasenaLogin.text == "" || m_InputUsuarioLogin.text == "")
        {
            return;
        }
        CallLoginApi(m_InputUsuarioLogin.text, m_InputContrasenaLogin.text, delegate (LoginResponse response)
        {
            m_ErrorText.text = "Logueando espere un momento";
            m_ErrorText.text = response.message;

            if (response.idResponse != -3)
            {
                if (toggleSesion.isOn)
                {
                    PlayerPrefs.SetString("SavePasswordToggle_Data", m_InputContrasenaLogin.text);
                    PlayerPrefs.SetString("SaveUserToggle_Data", m_InputUsuarioLogin.text);
                    var valueSave = Convert.ToInt32(toggleSesion.isOn);
                    PlayerPrefs.SetInt("toggleIsOn", valueSave);
                }
                ShowPerfilsGuardados();

                //UnityEngine.SceneManagement.SceneUIManager.LoadScene("m_PerfilNiñoUI");
                // m_LoguinUI.SetActive(false);
                //m_PerfilNiñoUI.SetActive(true);
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

            if (!m_Email.text.Contains (emailSet) && !cuentaRegistradaConExito)
            {
                    m_ErrorText.text = "Error 877: Lo sentimos pero no se puede leer un email valido";
            }
        }
    }
    public void SubmitRegister2()
    {
        foreach (string emailSet in Emails)
        {
            if (m_InputCorreo.text.Contains(emailSet))
            {
                cuentaRegistradaConExito = true;
                if (m_InputUsuario.text == "" || m_InputCorreo.text == "" || m_InputContrasena.text == "" || m_InputContrasena.text == "")
                {
                    m_ErrorText.text = "Error 444: Verifica que ningun campo este vacio";
                    return;
                }

                if (m_InputContrasena.text == m_InputContrasena.text)
                {
                    if (m_InputContrasena.text.Length >= MaxLenght)
                    {
                        m_ErrorText.text = "Procesando informacion por favor espera un momento";
                        CallRegister(m_InputUsuario.text, m_InputContrasena.text, m_InputCorreo.text,
                            m_InputNombre.text, m_InputApellido.text, "2018-05-05", "8",
                            delegate (Response response)
                        {

                            m_ErrorText.text = response.message;
                            if (response.done == true)
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
        ShowUI(m_PerfilNiñoCrearUI);
    }
    public void ShowPerfilsGuardados(){
        ShowUI(m_PerfilesGuardadosUI);
    }
    public void ShowActualizarDatos(){
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
        ShowUI(m_PerfilNinoVistaDatosUI);
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
    public void ShowlvlMath(){
        ShowUI(m_Chooselvl_MathUI);
    }
    public void ShowlvlComm(){
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
    public void CallRegister(string user, string pass, string email, string names, string lastnames, string birthday,
        string id, Action<Response> response)
    {
        StartCoroutine(Register(user, pass, email, names, lastnames, birthday, id, response));
    }

    IEnumerator Register(string user, string pass, string email, string names, string lastnames, string birthday,
        string id, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", pass);
        form.AddField("names", names);
        form.AddField("lastnames", lastnames);
        form.AddField("username", user);
        form.AddField("birthday", birthday);
        form.AddField("id", id);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;

        Debug.Log(www.text);
        response(JsonUtility.FromJson<Response>(www.text));
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

    void CallLoginApi(string user, string pass, Action<LoginResponse> response)
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
    }
}
