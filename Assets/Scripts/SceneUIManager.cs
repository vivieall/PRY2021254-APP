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
    [SerializeField] public GameObject m_ListaPersonalizadaUI = null;
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
    //[SerializeField] private Text m_AvatarChild;
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

    #region Lists
    [Header("Lists Managers")]
    [SerializeField] private ListManager favoritesListManager;
    [SerializeField] private ListManager customListManager;
    [SerializeField] private Text customListManagerLabel;
    #endregion
    
    [SerializeField] private CustomListList customListList;
    #region Specialist Data
    [SerializeField] public Sprite avatar1;
    [SerializeField] public Sprite avatar2;
    [SerializeField] public Sprite avatar3;
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
    private Level[] loggedChildFavoriteLevels;
    private CustomList[] loggedChildCustomLists;
    private LevelButtonListItem[] levelButtonListItems;

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

        PersistanceHandler persistanceHandler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();

        currentUI = startingUI;
        switch (persistanceHandler.GetState())
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

        levelButtonListItems = Resources.FindObjectsOfTypeAll<LevelButtonListItem>();

        if (persistanceHandler.GetState() != 0) {
            processLoginResponse(persistanceHandler.LoginResponse, false);
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
    [SerializeField] private ConfirmPopupComponent ConfirmPopup;
    [SerializeField] private InformationPopupComponent InformationPopup;
    public void PromptLogout()
	{
        ConfirmPopup.ClearAllEvents();
        ConfirmPopup.OnAccept.AddListener(OnLogoutConfirm);
        ConfirmPopup.OnDecline.AddListener(OnLogoutDeny);
        ConfirmPopup.SetConfirmationText("¿Desea cerrar sesión?");
        ConfirmPopup.gameObject.SetActive(true);
	}

    public void OnLogoutConfirm() { ConfirmPopup.gameObject.SetActive(false); logout(); }
    public void OnLogoutDeny() { ConfirmPopup.gameObject.SetActive(false); }

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
        PersistanceHandler persistanceHandler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();

        if (m_InputContrasenaLogin.text == "" || m_InputUsuarioLogin.text == "")
        {
              m_ErrorText.text = "Verifique que ningun campo este vacío";
        }
        CallLoginApi(m_InputUsuarioLogin.text, m_InputContrasenaLogin.text, delegate (LoginResponse response)
        {
            persistanceHandler.LoginResponse = response;
            processLoginResponse(response, true);
        });
    }

    public void processLoginResponse(LoginResponse response, bool showSavedProfiles) {
        m_ErrorTextLogin.text = "Validando, espere un momento";
        PlayerPrefs.DeleteAll();
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

            if (showSavedProfiles) {
                ShowPerfilsGuardados();
            } else {
                resetChildren();
                getChildren(() => loginChild(GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>().ChildIdx));
            }
        }
        else
        {
            m_ErrorTextLogin.text = response.message;
            Debug.Log("Llamada a la API no válida...");
        }
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
        loginChild(idx, true);
    }

    public void loginChild(int idx, bool showCategory)
    {
        GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>().ChildIdx = idx;
        loggedChild = ninosGuardian[idx];
        m_BienvenidaNino.text = "Hola, " + loggedChild.names + "!";

        CallGetChildrenFavoriteLevelsApi(Int32.Parse(loggedChild.idChild), delegate (Level[] response)
        {
            Debug.Log(response);
            loggedChildFavoriteLevels = response;
            PopulateFavoriteLevels();
        });

        CallGetChildrenCustomLevelListsApi(Int32.Parse(loggedChild.idChild), delegate (CustomList[] response)
        {
            Debug.Log(response);
            loggedChildCustomLists = response;
            PopulateCustomLists();
        });

        for(int i = 0; i < loggedChild.symptoms.Length; i++){
            checkBoxSintomasUpdate[loggedChild.symptoms[i].idSymptom - 1].isOn = true;
            sintomasUpdate[loggedChild.symptoms[i].idSymptom - 1] = true;
        }

        if (showCategory) {
            ShowCategoria();
        }
    }


    public void getChildren()
    {
        getChildren(null);
    }

    public void getChildren(Action callback)
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
                Debug.Log(response[i].avatar);
                if (response[i].avatar=="avatar1")
                {
                    botonesNinos[i].GetComponent<Image>().sprite = avatar1;
                }
                else if(response[i].avatar == "avatar2")
                {
                    botonesNinos[i].GetComponent<Image>().sprite = avatar2;
                }
                else if (response[i].avatar == "avatar3")
                {
                    botonesNinos[i].GetComponent<Image>().sprite = avatar3;
                }
                Debug.Log(response[i].avatar);
            }

            if (callback != null) {
                callback();
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
        CallRegisterChildApi(id_guardian, m_InputNombreChild.text, m_InputApellidoChild.text, avatarChild, m_InputFechaAnioChild.text + "-" + m_InputFechaMesChild.text + "-" + m_InputFechaDiaChild.text, generoChild, nivelAutismoChild, sintomas2,
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
        CallUpdateChildApi(loggedChild.idChild, m_InputNombreChildUpdate.text, m_InputApellidoChildUpdate.text, avatarChildUpdate, m_InputFechaAnioChildUpdate.text + "-" + m_InputFechaMesChildUpdate.text + "-" + m_InputFechaDiaChildUpdate.text, generoChildUpdate, nivelAutismoChildUpdate, sintomas2,
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
        //m_AvatarChild.text = loggedChild.avatar;
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
    public class LoginData
    {
        public string username;
        public string password;
        public string token;
    }

    public class LoginResponse
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

    public class DefaultResponse
    {
        public int idResponse;
        public string message;
    }

    [Serializable]
    public class ChildData
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
    public class ChildDataResponse
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
    public class AddLevelDto {
        public int idChild;
        public int idLevel; 
    }

    [Serializable]
    public class AddCustomLevelListDto {
        public int idChild;
        public string name; 
    }

    [Serializable]
    public class AddLevelToCustomListDto {
        public int idCustomLevelList;
        public int idLevel;
    }

    [Serializable]
    public class Level {
        public int idLevel;
        public string description;
        public Topic topic;
        public string video;
    }

    [Serializable]
    public class Topic {
        public int idTopic;
        public string description;
        public Category category;
    }

    [Serializable]
    public class Category {
        public int idCategory;
        public string description;
    }

    [Serializable]
    public class Symptom {
        public int idSymptom;
        public string description;
    }

    [Serializable]
    public class CustomList {
        public int idCustomLevelList;
        public string name;
        public Level[] levels;
    }
    public class Children
    {
        public ChildData[] children;
    }

    public class Specialist 
    {
        public int idSpecialist;
        public string names;
        public string lastNames;
        public string username;
        public string password;
    }

    private void PopulateFavoriteLevels() {
        favoritesListManager.RemoveAll();

        IEnumerable<int> childFavoriteLevelsIds = loggedChildFavoriteLevels.Select(nivel => nivel.idLevel);

        foreach(LevelButtonListItem levelButtonListItem in levelButtonListItems) {
            if (childFavoriteLevelsIds.Contains(levelButtonListItem.levelId)) {
                favoritesListManager.Add(levelButtonListItem);
            } else if (levelButtonListItem.levelUIComponent != null) {
                levelButtonListItem.levelUIComponent.LikeButton.interactable = true;
			    levelButtonListItem.levelUIComponent.LikeLabel.text = "¿Te gusta el nivel? Agregálo a favoritos";
            }
        }
    }

    private void PopulateCustomLists() {
        customListList.RemoveAll();

        foreach(CustomList customList in loggedChildCustomLists) {
            customListList.CreateList(customList.name, customListManager);
            ListManager listManager = customListList.getLists().Last();
            listManager.Id = customList.idCustomLevelList;

            IEnumerable<int> childFavoriteLevelsIds = customList.levels.Select(nivel => nivel.idLevel);

            foreach(LevelButtonListItem levelButtonListItem in levelButtonListItems) {
                if (childFavoriteLevelsIds.Contains(levelButtonListItem.levelId)) {
                    listManager.Add(levelButtonListItem);
                }
            }

        }
    }

    public void AddCustomList(string name)
    {
        ConfirmPopup.ConfirmOperation("¿Desea crear la lista " + name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallAddCustomListApi(Int32.Parse(loggedChild.idChild), name, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    customListList.CreateList(name, customListManager);
                }
                InformationPopup.PopupMessage(response.message);
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    public void SetCustomListActive(ListManager listManager)
    {
        listManager.Refresh();
        customListManagerLabel.text = listManager.Name;
        customListManager = listManager;
    }

    private void CallAddCustomListApi(int idChild, string name, Action<DefaultResponse> response)
    {
        AddCustomLevelListDto addCustomLevelListDto = new AddCustomLevelListDto();
        addCustomLevelListDto.idChild = idChild;
        addCustomLevelListDto.name = name;
        string json = JsonUtility.ToJson(addCustomLevelListDto);
        Debug.Log(json);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/addCustomLevelList", json, response));
    }

    public void AddLevelToCustomList(ListManager listManager, LevelButtonListItem levelButtonListItem)
    {
        ConfirmPopup.ConfirmOperation("¿Desea añadir este nivel a " + listManager.Name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallAddCustomLevelApi(listManager.Id, levelButtonListItem.levelId, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    listManager.Add(levelButtonListItem);
                    SetCustomListActive(listManager);
                    ShowUI(m_ListaPersonalizadaUI);
                } else {
                    InformationPopup.PopupMessage(response.message);
                }
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    public void DeleteLevelFromCustomList(ListManager listManager, LevelButtonListItem levelButtonListItem)
    {
        ConfirmPopup.ConfirmOperation("¿Desea eliminar este nivel de " + listManager.Name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallDeleteLevelFromCustomListApi(listManager.Id, levelButtonListItem.levelId, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    listManager.Remove(levelButtonListItem);
                }
                InformationPopup.PopupMessage(response.message);
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    public void AddFavoriteLevel()
    {
        ConfirmPopup.ConfirmOperation("¿Desea añadir este nivel a " + favoritesListManager.Name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallAddFavoriteLevelApi(Int32.Parse(loggedChild.idChild), nivelSeleccionado, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    LevelButtonListItem levelListItemToAdd = levelButtonListItems.Where(e => e.levelId == nivelSeleccionado).First();
                    favoritesListManager.Add(levelListItemToAdd);
                    ShowUI(m_ListaLikesUI);
                } else {
                    InformationPopup.PopupMessage(response.message);
                }
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    public void DeleteFavoriteLevel(LevelButtonListItem levelButtonListItem)
    {
        ConfirmPopup.ConfirmOperation("¿Desea eliminar este nivel de " + favoritesListManager.Name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallDeleteFavoriteLevelApi(Int32.Parse(loggedChild.idChild), levelButtonListItem.levelId, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    favoritesListManager.Remove(levelButtonListItem);
                }
                InformationPopup.PopupMessage(response.message);
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    private void CallAddCustomLevelApi(int idCustomLevelList, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelToCustomListDto addLevelToCustomListDto = new AddLevelToCustomListDto();
        addLevelToCustomListDto.idCustomLevelList = idCustomLevelList;
        addLevelToCustomListDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelToCustomListDto);
        Debug.Log(json);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/addLevelToCustomLevelList", json, response));
    }

    private void CallDeleteLevelFromCustomListApi(int idCustomLevelList, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelToCustomListDto addLevelToCustomListDto = new AddLevelToCustomListDto();
        addLevelToCustomListDto.idCustomLevelList = idCustomLevelList;
        addLevelToCustomListDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelToCustomListDto);
        Debug.Log(json);
        StartCoroutine(DeleteFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/deleteLevelinCustomLevelList", json, response));
    }

    private void CallAddFavoriteLevelApi(int idChild, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelDto addLevelDto = new AddLevelDto();
        addLevelDto.idChild = idChild;
        addLevelDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelDto);
        Debug.Log(json);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/addFavoriteLevel", json, response));
    }

    private void CallDeleteFavoriteLevelApi(int idChild, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelDto addLevelDto = new AddLevelDto();
        addLevelDto.idChild = idChild;
        addLevelDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelDto);
        Debug.Log(json);
        StartCoroutine(DeleteFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/deleteFavoriteLevel", json, response));
    }

    private void CallGetChildrenFavoriteLevelsApi(int idChild, Action<Level[]> response)
    {
        StartCoroutine(GetChildrenFavoriteLevels("https://teapprendo.herokuapp.com/children/listFavoriteLevels?idChild=" + idChild, response));
    }

    private void CallGetChildrenCustomLevelListsApi(int idChild, Action<CustomList[]> response)
    {
        StartCoroutine(GetChildrenCustomLevelLists("https://teapprendo.herokuapp.com/children/listCustomLevelLists?idChild=" + idChild, response));
    }
    IEnumerator AddFavoriteLevelRequest(string url, string json, Action<DefaultResponse> response)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

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

    IEnumerator DeleteFavoriteLevelRequest(string url, string json, Action<DefaultResponse> response)
    {
        var uwr = new UnityWebRequest(url, "DELETE");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

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

    IEnumerator GetChildrenFavoriteLevels(string url, Action<Level[]> response)
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
            response(JsonHelper.FromJson<Level>(fixJson(uwr.downloadHandler.text)));
        }
    }

    IEnumerator GetChildrenCustomLevelLists(string url, Action<CustomList[]> response)
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
            response(JsonHelper.FromJson<CustomList>(fixJson(uwr.downloadHandler.text)));
        }
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


    private void CallUpdateChildApi(string id, string names, string lastnames,string avatarChild, string birthday, string gender, string asdlevel, int[] symptoms, Action<ChildDataResponse> response)
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

    private void testDC() {
        Console.WriteLine("THIS IS A TEST");
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