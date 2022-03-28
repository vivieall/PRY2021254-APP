using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
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
    [SerializeField] private GameObject m_Tema_PSocialUI;
    [SerializeField] private GameObject m_Chooselvl_PSocialUI;
    [SerializeField] private GameObject m_Tema_ScienceUI;
    [SerializeField] private GameObject m_Chooselvl_ScienceUI;
    [SerializeField] private GameObject m_FiltroUI;
    [SerializeField] private GameObject m_ResetPasswordWindow;
    [SerializeField] private GameObject m_PremiumPaymentWindow;
    [SerializeField] private GameObject m_MessageWindowResponse;
    [SerializeField] private GameObject m_CodigoNinoWindow;
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
    [SerializeField] private InputField m_ResetPasswordEmail;
    [SerializeField] private Text m_ErrorTextResetPassword;
    [SerializeField] private Text m_MessageWindowResponseText;
    #endregion

    [SerializeField] private Toggle toggleSesion;
    
    [Header("Valid Mail")]
    public string[] Emails;
    public bool cuentaRegistradaConExito;
    public int MaxLenght;
    public GameObject[] botonesNinos;
    public Text[] nombresNinos;
    public Text m_BienvenidaNino;
    public Toggle[] checkBoxSintomas;
    public Toggle[] checkBoxSintomasUpdate;

    #region Register Child
    [Header("Register Child Inputs")]
    [SerializeField] private InputField m_InputNombreChild;
    [SerializeField] private InputField m_InputApellidoChild;
    [SerializeField] private InputField m_InputFechaDiaChild;
    [SerializeField] private InputField m_InputFechaMesChild;
    [SerializeField] private InputField m_InputFechaAnioChild;
    #endregion

    #region Perfil Guardian
    [Header("Perfil Guardian Text")]
    [SerializeField] private Text m_NombreGuardian;
    [SerializeField] private Text m_ApellidoGuardian;
    [SerializeField] private Text m_FechaNacimientoGuardian;
    [SerializeField] private Text m_EmailGuardian;
    #endregion


    #region Perfil Child
    [Header("Perfil Child Text")]
    [SerializeField] private Text m_NombreChild;
    [SerializeField] private Text m_IApellidoChild;
    [SerializeField] private Text m_FechaDiaChild;
    [SerializeField] private Text m_FechaMesChild;
    [SerializeField] private Text m_FechaAnioChild;
    [SerializeField] private Text m_GeneroChild;
    [SerializeField] private Text m_GradoChild;
    [SerializeField] private Text m_SintomasChild;
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

    #region Specialist Data
    [Header("Specialist Data")]
    [SerializeField] private InputField m_UsernameSpecialist;
    [SerializeField] private InputField m_PasswordSpecialist;
    #endregion

    //private bool sesionIniciada;
    private string id_guardian;
    private string nivelAutismoChild;
    private string generoChild;
    private string avatarChild;
    private int nivelSeleccionado;
    private bool premiumOn;
    private string nivelAutismoChildUpdate;
    private string generoChildUpdate;
    private string avatarChildUpdate;
    private bool[] sintomas = new bool[] { false, false, false, false, false, false, false };
    private bool[] sintomasUpdate = new bool[] { false, false, false, false, false, false, false };
    private int cantNinos;
    private ChildDataResponse[] ninosGuardian = { };
    private ChildDataResponse loggedChild;

    void Start()
    {
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
        AllUIs.Add(m_Tema_PSocialUI);
        AllUIs.Add(m_Chooselvl_PSocialUI);
        AllUIs.Add(m_FiltroUI);

        foreach (GameObject ui in AllUIs)
        {
            ui.SetActive(false);
        }

        //sesionIniciada = false;
        premiumOn = false;
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
            case 4:
                ShowUI(m_Chooselvl_PSocialUI);
                break;
            case 5:
                ShowUI(m_Chooselvl_ScienceUI);
                break;
        }
    }


    public void setNivel(int nivel){
        nivelSeleccionado = nivel;
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
        sintomasUpdate[idx] = !sintomasUpdate[idx];
    }

    public void setAvatarUpdate(string avatarCode)
    {
        avatarChildUpdate = avatarCode;
    }

    public void submitResetPassword()
    {

        if (m_ResetPasswordEmail.text == "")
        {
            m_ErrorTextResetPassword.text = "Debe ingresar un correo electrónico";
            return;
        }

        CallGetRequestPasswordReset(m_ResetPasswordEmail.text, delegate (DefaultResponse response)
        {
            m_ErrorTextResetPassword.text = response.message;

            if (response.idResponse == 1)
            {
                //Correcto
            }
        });   
    }

    public void submitPayment(bool pago){
        if(pago == true){
            m_MessageWindowResponseText.text = "Bienvenid@ a la versión premium";
            premiumOn = true;
        }
        else if(pago == false){
            m_MessageWindowResponseText.text = "Debe completar el proceso para poder adquirir la versión premium";
        }
        ClosePremiumPayment();
        ShowMessageWindow();
    }

    public void getCodigoNino(){
        if(premiumOn == true){
            CallGetRequestSpecialistFromChild(loggedChild.idChild, delegate (Specialist response)
            {
                if(response.idSpecialist > 0){
                    m_UsernameSpecialist.text = response.username;
                    m_PasswordSpecialist.text = response.password;
                    ShowCodigoNinoWindow();
                }
            });
        }
        else if(premiumOn == false){
            m_MessageWindowResponseText.text = "Debe adquirir la versión premium";
            ShowMessageWindow();
        }
    }

    public string getIdChild(){
        return loggedChild.idChild;
    }

    public int getNivel(){
        return nivelSeleccionado;
    }
    
	#region Logout
    [Header("Confirm Logout")]
    [SerializeField] private GameObject ConfirmPopup;
    public void PromptLogout()
	{
        ConfirmPopupComponent confirmComp = ConfirmPopup.GetComponent<ConfirmPopupComponent>();
        confirmComp.ClearAllEvents();
        confirmComp.OnAccept.AddListener(OnLogoutConfirm);
        confirmComp.OnDecline.AddListener(OnLogoutDeny);
        confirmComp.SetConfirmationText("¿Desea cerrar sesión?");
        ConfirmPopup.SetActive(true);
	}

    public void OnLogoutConfirm() { ConfirmPopup.SetActive(false); logout(); }
    public void OnLogoutDeny() { ConfirmPopup.SetActive(false); }

	public void logout()
    {
        id_guardian = null;
        //sesionIniciada = false;
        premiumOn = false;
        m_ErrorTextLogin.text = "";
        resetChildren();
        ShowLoguin();
    }
    #endregion

    public void submitLogin2()
    {
        if (m_InputContrasenaLogin.text == "" || m_InputUsuarioLogin.text == "")
        {
              m_ErrorText.text = "Verifique que ningun campo este vacío";
        }
        CallLoginApi(m_InputUsuarioLogin.text, m_InputContrasenaLogin.text, delegate (LoginResponse response)
        {
            m_ErrorTextLogin.text = "Validando, espere un momento";
            //PlayerPrefs.DeleteAll();
            Debug.Log("Validando...");

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
                //sesionIniciada = true;

                PlayerPrefs.SetString("token", response.token);
                PlayerPrefs.Save();

                datosUsuarioLogeado.token = response.token;
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
                m_ErrorTextLogin.text = response.message;
                Debug.Log("Llamada a la API no válida...");
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

    public void DeleteChild()
    {
        CallGetRequestDeleteChildApi(loggedChild.idChild, delegate (DefaultResponse response)
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
                    m_ErrorText.text = "Verifica que ningún campo este vacío";
                    return;
                }

                if (m_InputContrasena.text == m_InputContrasenaConf.text)
                {
                    if (m_InputContrasena.text.Length >= MaxLenght)
                    {
                        m_ErrorText.text = "Validando, espere un momento";
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
                        m_ErrorText.text = "Tu contraseña debe contener mínimo 8 caracteres";
                    }
                }
                else
                {
                    m_ErrorText.text = "Verifique los datos ingresados";
                    return;
                }
            }

            if (!m_InputCorreo.text.Contains(emailSet) && !cuentaRegistradaConExito)
            {
                m_ErrorText.text = "Email no válido";
            }
        }
    }

    public void loginChild(int idx)
    {
        loggedChild = ninosGuardian[idx];
        m_BienvenidaNino.text = "Hola, " + loggedChild.names + "!";
        for(int i = 0; i < loggedChild.symptoms.Length; i++){
            checkBoxSintomasUpdate[loggedChild.symptoms[i].idSymptom - 1].isOn = true;
            sintomasUpdate[loggedChild.symptoms[i].idSymptom - 1] = true;
        }

        ShowCategoria();
    }

    public void getChildren()
    {
        CallGetRequestChildrenApi(id_guardian, delegate (ChildDataResponse[] response)
        {
            foreach (ChildDataResponse c in response){
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

        for(int i = 0; i < 7; i++)
        {
            checkBoxSintomas[i].isOn = false;
            checkBoxSintomasUpdate[i].isOn = false;
            sintomasUpdate[i] = false;
            sintomas[i] = false;
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
                    m_ErrorText.text = "Verifica que ningún campo este vacío";
                    return;
                }

                if (m_InputContrasenaNuevaUpdate.text == m_InputContrasenaNuevaConfUpdate.text)
                {
                    if (m_InputContrasena.text.Length >= MaxLenght)
                    {
                        m_ErrorText.text = "Procesando informacion, espere un momento";
                        CallUpdateGuardianApi(id_guardian, m_InputContrasenaActualUpdate.text, m_InputContrasenaNuevaUpdate.text, m_InputCorreoUpdate.text,
                            m_InputNombreUpdate.text, m_InputApellidoUpdate.text, m_InputFechaAnioUpdate.text + "-" + m_InputFechaMesUpdate.text + "-" + m_InputFechaDiaUpdate.text,
                            delegate (GuardianData response)
                            {

                                if (response.idGuardian != null)
                                {
                                    print("Cuenta actualizada con éxito");
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
                        m_ErrorText.text = "Tu contraseña debe contener mínimo 8 caracteres";
                    }
                }
                else
                {
                    m_ErrorText.text = "Verifique los datos ingresados";
                    return;
                }
            }

            if (!m_InputCorreoUpdate.text.Contains(emailSet) && !cuentaRegistradaConExito)
            {
                m_ErrorText.text = "Ingrese un correo válido";
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

	public void ShowLoguin(){
        ShowUI(m_LoguinUI);
    }
    public void ShowResetPassword(){
        m_ResetPasswordWindow.SetActive(true);
    }
    public void CloseResetPassword(){
        m_ResetPasswordWindow.SetActive(false);
    }

    public void ShowPremiumPayment(){
        m_PremiumPaymentWindow.SetActive(true);
    }
    public void ClosePremiumPayment(){
        m_PremiumPaymentWindow.SetActive(false);
    }

    public void ShowMessageWindow(){
        m_MessageWindowResponse.SetActive(true);
    }
    public void CloseMessageWindow(){
        m_MessageWindowResponse.SetActive(false);
    }

    public void ShowCodigoNinoWindow(){
        m_CodigoNinoWindow.SetActive(true);
    }
    public void CloseCodigoNinoWindow(){
        m_CodigoNinoWindow.SetActive(false);
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
        setGuardianProfileData();
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
    public void ShowTemaPSocial()
    {
        ShowUI(m_Tema_PSocialUI);
    }
    public void ShowTemaScience()
    {
        ShowUI(m_Tema_ScienceUI);
    }
    public void ShowNivelesCompletos(){
        ShowUI(m_NivelesCompletosUI);
    }
    public void ShowNinoVistaDatos(){
        setChildProfileData();
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
    public void ShowChooselvlPSocial()
    {
        ShowUI(m_Chooselvl_PSocialUI);
    }
    public void ShowChooselvlScience()
    {
        ShowUI(m_Chooselvl_ScienceUI);
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
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

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
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

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
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

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

    private void CallGetRequestDeleteChildApi(string id, Action<DefaultResponse> response)
    {
        StartCoroutine(GetRequestDeleteChild("https://teapprendo.herokuapp.com/children/delete?idChild=" + loggedChild.idChild, response));
    }

    IEnumerator GetRequestDeleteChild(string url, Action<DefaultResponse> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
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
    }

    private class GuardianData
    {
        public string token;
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
        public string token;
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
            delegate (ChildDataResponse response)
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
            delegate (ChildDataResponse response)
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
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

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
    
    public void setChildProfileData() {
        m_NombreChild.text = loggedChild.names;
        m_IApellidoChild.text = loggedChild.lastNames;
        m_FechaDiaChild.text = loggedChild.birthday.Substring(8, 2);
        m_FechaMesChild.text = loggedChild.birthday.Substring(5, 2);
        m_FechaAnioChild.text = loggedChild.birthday.Substring(0, 4);
        m_GeneroChild.text = loggedChild.gender;
        m_GradoChild.text = loggedChild.asdLevel;
        m_SintomasChild.text = "";
        foreach (Symptom c in loggedChild.symptoms){
            m_SintomasChild.text += c.description + " ";
        }
    }

    public void setGuardianProfileData() {
        m_NombreGuardian.text = datosUsuarioLogeado.names;
        m_ApellidoGuardian.text = datosUsuarioLogeado.lastNames;
        m_FechaNacimientoGuardian.text = datosUsuarioLogeado.birthday.Substring(8, 2) + "/" + datosUsuarioLogeado.birthday.Substring(5, 2) + "/" + datosUsuarioLogeado.birthday.Substring(0, 4);
        m_EmailGuardian.text = datosUsuarioLogeado.email;
    }
    private class LoginData
    {
        public string username;
        public string password;
        public string token;
    }

    private class LoginResponse
    {
        public int idResponse;
        public string token;
        public string message;

        public string idGuardian;
        public string idUserLogin;
        public string username;
        public string password;
        public string email;
        public string names;
        public string lastNames;
        public string birthday;
    }

    private class DefaultResponse
    {
        public int idResponse;
        public string message;
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
    [Serializable]
    private class ChildDataResponse
    {
        public string idChild;
        public string asdLevel;
        public string avatar;
        public string gender;
        public int idGuardian;
        public string lastNames;
        public string names;
        public string birthday;
        public string password;
        public Symptom[] symptoms;
    }

    [Serializable]
    private class Symptom {
        public int idSymptom;
        public string description;
    }
    private class Children
    {
        public ChildData[] children;
    }

    private class Specialist 
    {
        public int idSpecialist;
        public string names;
        public string lastNames;
        public string username;
        public string password;
    }

    private void CallRegisterChildApi(string guardian_id, string names, string lastnames, string birthday, string gender, string asdlevel, int[] symptoms, Action<ChildDataResponse> response)
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

    IEnumerator PostRequestRegisterChild(string url, string json, Action<ChildDataResponse> response)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<ChildDataResponse>(uwr.downloadHandler.text));
        }
    }


    private void CallUpdateChildApi(string id, string names, string lastnames, string birthday, string gender, string asdlevel, int[] symptoms, Action<ChildDataResponse> response)
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

    IEnumerator PutRequestUpdateChild(string url, string json, Action<ChildDataResponse> response)
    {
        var uwr = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            response(JsonUtility.FromJson<ChildDataResponse>(uwr.downloadHandler.text));
        }
    }
    private void CallGetRequestChildrenApi(string id, Action<ChildDataResponse[]> response)
    {
        StartCoroutine(GetRequestChildren("https://teapprendo.herokuapp.com/children/listByIdGuardian?idGuardian=" + id, response));
    }

    IEnumerator GetRequestChildren(string url, Action<ChildDataResponse[]> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            response(JsonHelper.FromJson<ChildDataResponse>(fixJson(uwr.downloadHandler.text)));
        }
    }

    private void CallGetRequestPasswordReset(string email, Action<DefaultResponse> response)
    {
        StartCoroutine(GetRequestPasswordReset("https://teapprendo.herokuapp.com/guardians/restorePassword?email=" + email, response));
    }

    IEnumerator GetRequestPasswordReset(string url, Action<DefaultResponse> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
        }
    }

    private void CallGetRequestSpecialistFromChild(string idChild, Action<Specialist> response)
    {
        StartCoroutine(GetRequestSpecialistFromChild("https://teapprendo.herokuapp.com/children/activateSpecialist?idChild=" + idChild, response));
    }

    IEnumerator GetRequestSpecialistFromChild(string url, Action<Specialist> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);
        Debug.Log("Token: " + token);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            response(JsonUtility.FromJson<Specialist>(uwr.downloadHandler.text));
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

    public static T[] GetArray<T> (string json)
	{
		string newJson = "{\"data\":" + json + "}";
		Wrapper<T> w = JsonUtility.FromJson<Wrapper<T>> (newJson);
		return w.Items;
	}

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}