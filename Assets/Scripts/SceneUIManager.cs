using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions; 
using UnityEngine.SceneManagement;

public class SceneUIManager : MonoBehaviour
{
    [Header("Starting UI")]
    [SerializeField] private GameObject startingUI;
    
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
    [SerializeField] private Toggle m_TooglePrivacy;
    [SerializeField] private ConfirmPopupComponent ConfirmLeerCondiciones;
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
    [SerializeField] private Text m_ErrorUpdateGuardianText;
    #endregion

    #region Login
    [Header("Login Inputs")]
    [SerializeField] private InputField m_InputContrasenaLogin;
    [SerializeField] private InputField m_InputUsuarioLogin;
    public InputField m_PasswordInputLogin;
    public InputField m_UserInputLogin;
    #endregion

    #region Reset Password
    [Header("Reset Password")]
    [SerializeField] private Text m_ErrorText;
    [SerializeField] private InputField m_ResetPasswordEmail;
    [SerializeField] private Text m_ErrorTextResetPassword;
    [SerializeField] private Text m_MessageWindowResponseText;
    [SerializeField] private Toggle toggleSesion;
    #endregion
    
    [Header("Valid Mail")]
    public string[] Emails;
    public bool cuentaRegistradaConExito;
    public int MinLenght;
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
    [SerializeField] private Text m_ErrorCreateChildText;
    #endregion

    #region Profile Guardian
    [Header("Profile Guardian")]
    [SerializeField] private Text m_NombreGuardian;
    [SerializeField] private Text m_ApellidoGuardian;
    [SerializeField] private Text m_FechaNacimientoGuardian;
    [SerializeField] private Text m_EmailGuardian;
    #endregion


    #region Profile Child
    [Header("Profile Child")]
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
    [Header("Update Child Inputs")]
    [SerializeField] private InputField m_InputNombreChildUpdate;
    [SerializeField] private InputField m_InputApellidoChildUpdate;
    [SerializeField] private InputField m_InputFechaDiaChildUpdate;
    [SerializeField] private InputField m_InputFechaMesChildUpdate;
    [SerializeField] private InputField m_InputFechaAnioChildUpdate;
    [SerializeField] private Button m_SexoMasculinoButton;
    [SerializeField] private Button m_SexoFemeninoButton;
    [SerializeField] private Button m_GradoLeveButton;
    [SerializeField] private Button m_GradoModeradoButton;
    [SerializeField] private Button m_GradoGraveButton;
    [SerializeField] private Button m_AvatarMasculinoButton;
    [SerializeField] private Button m_AvatarFemeninoButton;
    [SerializeField] private Button m_AvatarAdicionalButton;
    [SerializeField] private Text m_ErrorTextLogin;
    #endregion

    #region Specialist Data
    [Header("Specialist Data")]
    [SerializeField] private InputField m_UsernameSpecialist;
    #endregion

    #region Get Premium
    [Header("Get Premium Inputs")]
    [SerializeField] private Text m_PremiumTextLabel;
    [SerializeField] private Button m_PremiumSiBoton;
    [SerializeField] private Button m_PremiumNoBoton;
    [SerializeField] private Button m_PremiumAceptarBoton;
    [SerializeField] private InputField m_InputNumeroTarjeta;
    [SerializeField] private InputField m_InputMesCaducidad;
    [SerializeField] private InputField m_InputAnoCaducidad;
    [SerializeField] private InputField m_InputCCV;
    #endregion

    #region Lists
    [Header("Lists Managers")]
    [SerializeField] private ListManager favoritesListManager;
    [SerializeField] private ListManager customListManager;
    [SerializeField] private Text customListManagerLabel;
    [SerializeField] private CustomListList customListList;
    [SerializeField] private Text m_EmptyCustomListMessageTextLabel;
    #endregion

    #region Specialist Data
    [SerializeField] public Sprite avatar1;
    [SerializeField] public Sprite avatar2;
    [SerializeField] public Sprite avatar3;
    #endregion

    #region Popups
    [Header("Popups")]
    [SerializeField] private GameObject m_RegisterConfirmationPopupUI;
    [SerializeField] private GameObject m_RegisterChildConfirmationPopupUI;
    [SerializeField] private GameObject m_UpdateProfileConfirmationPopupUI;
    [SerializeField] private GameObject m_UpdateChildProfileConfirmationPopupUI;
    [SerializeField] private GameObject m_ChangeChildProfileConfirmationPopupUI;
    [SerializeField] private GameObject m_ChangeAvatarConfirmationPopupUI;
    [SerializeField] private GameObject m_ErrorLoginPopupUI;
    [SerializeField] private GameObject m_UnavailableLevelPopupUI;
    [SerializeField] private GameObject m_ResetPasswordConfirmationPopupUI;
    #endregion

    #region Favorite Levels
    [Header("Favorite Levels")]
    [SerializeField] private Text m_EmptyFavoriteLevelsListMessageTextLabel;
    #endregion

    #region Logout
    [Header("Logout")]
    [SerializeField] private ConfirmPopupComponent ConfirmPopup;
    [SerializeField] private InformationPopupComponent InformationPopup;
    [SerializeField] private InformationPopupComponent WaitPopup;
    #endregion

    GuardianData datosUsuarioLogeado = new GuardianData();
    private ArrayList AllUIs;
    private GameObject currentUI;
    private string id_guardian;
    private string nivelAutismoChild = "";
    private string generoChild = "";
    private string avatarChild = "";
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

    public string getIdChild(){
        return loggedChild.idChild;
    }

    public int getNivel(){
        return nivelSeleccionado;
    }

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

        premiumOn = false;

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
    
    #region Login Guardian
    public void submitLogin2()
    {
        PlayerPrefs.DeleteAll();
        PersistanceHandler persistanceHandler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();

        if (m_InputContrasenaLogin.text == "" || m_InputUsuarioLogin.text == "")
        {
              ShowErrorPopup("Verifique no dejar campos vacíos");
        }
        else
        {
            CallLoginApi(m_InputUsuarioLogin.text, m_InputContrasenaLogin.text, delegate (LoginResponse response)
            {
                persistanceHandler.LoginResponse = response;
                processLoginResponse(response, true);
            });
            PromptLoading();
        }
    }

    public void processLoginResponse(LoginResponse response, bool showSavedProfiles) {
        //m_ErrorTextLogin.text = "Validando, espere un momento";    
        PlayerPrefs.DeleteAll();
        WaitPopup.gameObject.SetActive(false);
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

            PlayerPrefs.SetString("token", response.token);
            PlayerPrefs.Save();

            datosUsuarioLogeado.token = response.token;
            datosUsuarioLogeado.username = response.username;
            datosUsuarioLogeado.password = response.password;
            datosUsuarioLogeado.email = response.email;
            datosUsuarioLogeado.names = response.names;
            datosUsuarioLogeado.lastNames = response.lastNames;
            datosUsuarioLogeado.birthday = response.birthday;
            datosUsuarioLogeado.premium = response.premium;

            SwitchPremium(datosUsuarioLogeado.premium);

            if (showSavedProfiles) {
                ShowPerfilsGuardados();
            } else {
                resetChildren();
                getChildren(() => loginChild(GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>().ChildIdx));
            }
        }
        else
        {
            if(response.message == "Contraseña incorrecta")
                ShowErrorPopup("Usuario o contraseña no válido");
            else
                ShowErrorPopup(response.message);
        }
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

    public void getChildren()
    {
        getChildren(null);
    }

    public void getChildren(Action callback)
    {
        CallGetRequestChildrenApi(id_guardian, delegate (ChildDataResponse[] response)
        {
            ninosGuardian = response;
            cantNinos = response.Length;

            for (int i = 0; i < cantNinos; i++)
            {
                botonesNinos[i].SetActive(true);
                nombresNinos[i].text = response[i].names;
                nombresNinos[i].gameObject.SetActive(true);
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
            }

            if (callback != null) {
                callback();
            }
        });
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

        int childId = Int32.Parse(loggedChild.idChild);

        CallGetCompletedLevelsApi(childId, delegate (LevelRecord[] completedLevelsReponse) {
            IEnumerable<int> completedLevelsIds = completedLevelsReponse.Select(levelRecord => levelRecord.level.idLevel);

            foreach(LevelButtonListItem levelListItemToEdit in levelButtonListItems) {
                levelListItemToEdit.completedCheck.gameObject.SetActive(completedLevelsIds.Contains(levelListItemToEdit.levelId));
            }

            foreach(LevelRecord record in completedLevelsReponse) {
                LevelButtonListItem levelListItemToEdit = levelButtonListItems.Where(e => e.levelId == record.level.idLevel).First();
                levelListItemToEdit.completedCheck.gameObject.SetActive(true);
            }

            CallGetChildrenFavoriteLevelsApi(childId, delegate (Level[] response)
            {
                loggedChildFavoriteLevels = response;

                if(loggedChildFavoriteLevels == null || loggedChildFavoriteLevels.Length == 0)
                    m_EmptyFavoriteLevelsListMessageTextLabel.gameObject.SetActive(true);
                else
                    m_EmptyFavoriteLevelsListMessageTextLabel.gameObject.SetActive(false);

                PopulateFavoriteLevels();
            });

            CallGetChildrenCustomLevelListsApi(childId, delegate (CustomList[] response)
            {
                loggedChildCustomLists = response;
                PopulateCustomLists();
            });
        });

        for(int i = 0; i < loggedChild.symptoms.Length; i++){
            checkBoxSintomasUpdate[loggedChild.symptoms[i].idSymptom - 1].isOn = true;
            sintomasUpdate[loggedChild.symptoms[i].idSymptom - 1] = true;
        }

        if (showCategory) {
            ShowCategoria();
        }
    }
    #endregion

    #region Logout Guardian
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
        premiumOn = false;
        m_ErrorTextLogin.text = "";
        resetChildren();
        blankLogin();
        ShowLoguin();
    }
    #endregion

    #region Reset Password Guardian
    public void submitResetPassword()
    {
        if (m_ResetPasswordEmail.text == "")
        {
            ShowErrorPopup("Debe ingresar un correo electrónico");
            return;
        }
        else if (!validateEmail(m_ResetPasswordEmail.text))
        {
            ShowErrorPopup("Correo no válido");
            return;
        }

        CallGetRequestPasswordReset(m_ResetPasswordEmail.text, delegate (DefaultResponse response)
        {
            if (response.idResponse >= 0)
                ResetPasswordConfirmationPopup();
            else
                ShowErrorPopup(response.message);
        });   
    }
    #endregion

    #region Register Guardian
    public void SubmitRegister2()
    {
        if (m_TooglePrivacy.isOn==false)
        {
            ShowErrorPopup("Debes aceptar el acuerdo de privacidad");
            return;
        }
        string actualTime = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        string fNacimientoCuidador = m_InputFechaDia.text + "-" + m_InputFechaMes.text + "-" + m_InputFechaAnio.text;
        DateTime fNacimientoCuidadorDATETIME;
        //DateTime.TryParse(fNacimientoCuidador, out fNacimientoCuidadorDATETIME);
        //Debug.Log(DateTime.UtcNow.ToLocalTime().Subtract(fNacimientoCuidadorDATETIME).Days);
        if (m_InputUsuario.text != "" && m_InputCorreo.text != "" && m_InputContrasena.text != "" && m_InputContrasenaConf.text != "" &&
            m_InputNombre.text != "" && m_InputApellido.text != "" && m_InputFechaAnio.text != "" && m_InputFechaMes.text != "" && m_InputFechaDia.text != "")
        {
            if (validateEmail(m_InputCorreo.text))
            {
                cuentaRegistradaConExito = true;
                if (m_InputContrasena.text == m_InputContrasenaConf.text)
                {
                    if (m_InputContrasena.text.Length >= MinLenght && m_InputContrasena.text.Length <= MaxLenght)
                    {
                        if (DateTime.TryParse(fNacimientoCuidador, out fNacimientoCuidadorDATETIME) != false)
                        {
                            if (DateTime.UtcNow.ToLocalTime().Subtract(fNacimientoCuidadorDATETIME).Days >= 13 * 365)
                            {
                                //m_ErrorText.text = "Validando, espere un momento";
                                CallRegisterGuardianApi(m_InputUsuario.text, m_InputContrasena.text,
                                    m_InputCorreo.text, m_InputNombre.text, m_InputApellido.text,
                                m_InputFechaAnio.text + "-" + m_InputFechaMes.text + "-" + m_InputFechaDia.text, delegate (GuardianResponse response)
                                {
                                    if (response.idResponse >= 0)
                                    {
                                        cuentaRegistradaConExito = false;
                                        blankRegisterSpace();
                                        ShowRegisterConfirmationPopup();
                                    }
                                    else
                                        ShowErrorPopup(response.message);
                                }
                                );
                            }
                            else { ShowErrorPopup("Debes tener más de 13 años para ser cuidador"); }
                        }
                        else { ShowErrorPopup("Fecha mal ingresada"); }

                    }
                    else
                    {
                        ShowErrorPopup("Tu contraseña debe tener entre 4 y 20 caracteres");
                    }
                }
                else
                {
                    ShowErrorPopup("Las contraseñas no coinciden");
                    return;
                }
            }
            else
            {
                ShowErrorPopup("Email no válido");
            }
        }
        else
        {
            ShowErrorPopup("Verifica que ningún campo este vacío");
            return;
        }
    }
    #endregion
    public void PromptAcceptConditions()
    {
        ConfirmLeerCondiciones.ConfirmOperation("¿Acepta los terminos y condiciones presentados?", () => {
            ConfirmPopup.gameObject.SetActive(false); m_TooglePrivacy.isOn =true;
        }, () => { ConfirmPopup.gameObject.SetActive(false); m_TooglePrivacy.isOn = false; });

    }


    #region Update Guardian
    public void SubmitUpdateGuardian()
    {
        PersistanceHandler persistanceHandler = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>();
        string actualTime = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        string fNacimientoCuidadorUpdate = m_InputFechaDiaUpdate.text + "-" + m_InputFechaMesUpdate.text + "-" + m_InputFechaAnioUpdate.text;
        DateTime fNacimientoCuidadorDATETIMEUpdate;
        if ( m_InputNombreUpdate.text != "" && m_InputApellidoUpdate.text != "" && m_InputFechaAnioUpdate.text != "" && m_InputFechaMesUpdate.text != "" && m_InputFechaDiaUpdate.text != "" && m_InputCorreoUpdate.text != "")
        {
            if (DateTime.TryParse(fNacimientoCuidadorUpdate, out fNacimientoCuidadorDATETIMEUpdate) != false)
            {
                if (DateTime.UtcNow.ToLocalTime().Subtract(fNacimientoCuidadorDATETIMEUpdate).Days >= 13 * 365)
                {

                    if (validateEmail(m_InputCorreoUpdate.text))
                    {
                        cuentaRegistradaConExito = true;
                        m_InputContrasenaNuevaUpdate.text = m_InputContrasenaActualUpdate.text;
                        m_InputContrasenaNuevaConfUpdate.text = m_InputContrasenaActualUpdate.text;
                     
                        m_ErrorUpdateGuardianText.text = "Cambios guardados con éxito";
                        CallUpdateGuardianApi(id_guardian, m_InputContrasenaActualUpdate.text, m_InputContrasenaNuevaUpdate.text,  m_InputCorreoUpdate.text, m_InputNombreUpdate.text, m_InputApellidoUpdate.text, m_InputFechaAnioUpdate.text + "-" + m_InputFechaMesUpdate.text + "-" + m_InputFechaDiaUpdate.text, delegate (GuardianData response)
                            {
                                if (response.idGuardian != null)
                                {
                                    datosUsuarioLogeado.username = response.username;
                                    datosUsuarioLogeado.password = response.password;
                                    datosUsuarioLogeado.email = response.email;
                                    datosUsuarioLogeado.names = response.names;
                                    datosUsuarioLogeado.lastNames = response.lastNames;
                                    datosUsuarioLogeado.birthday = response.birthday;
                                    ShowUpdateProfileConfirmationPopup();
                                }
                            }
                        );
                    }
                    else
                    {    
                        ShowErrorPopup("Ingrese un correo válido");
                    }
                }
                else { ShowErrorPopup("Debes tener más de 13 años para ser cuidador"); }
            }
            else { ShowErrorPopup("Fecha mal ingresada"); }

        }
        else
        {
            ShowErrorPopup("Verifica que ningún campo este vacío");
            return;
        }
    }

    public void ShowPassword(){
        if (m_InputContrasenaActualUpdate.GetComponent<InputField>().contentType == InputField.ContentType.Password)
            m_InputContrasenaActualUpdate.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
        else
            m_InputContrasenaActualUpdate.GetComponent<InputField>().contentType = InputField.ContentType.Password;
    }
    #endregion

    #region Register Child
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

        string actualTime = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        string fNacimientoChild = m_InputFechaDia.text + "-" + m_InputFechaMes.text + "-" + m_InputFechaAnio.text;
        DateTime fNacimientoChildDATETIME;

        if (m_InputNombreChild.text == "" || m_InputApellidoChild.text == "" || m_InputFechaAnioChild.text == "" || m_InputFechaMesChild.text == "" || m_InputFechaDiaChild.text == "" || generoChild == "" || nivelAutismoChild == "" || avatarChild == "")
        {
            ShowErrorPopup("Verifique no dejar campos vacíos");
            return;
        }
        if (DateTime.TryParse(fNacimientoChild, out fNacimientoChildDATETIME) == false)
        {
            ShowErrorPopup("Fecha mal ingresada");
            return;
        }
        if (DateTime.UtcNow.ToLocalTime().Subtract(fNacimientoChildDATETIME).Days < 2 * 365)
        {
            ShowErrorPopup("El niño debe tener más de 2 años");
            return;
        }


        CallRegisterChildApi(id_guardian, m_InputNombreChild.text, m_InputApellidoChild.text, avatarChild, m_InputFechaAnioChild.text + "-" + m_InputFechaMesChild.text + "-" + m_InputFechaDiaChild.text, generoChild, nivelAutismoChild, sintomas2,
            delegate (ChildDataResponse response)
            {
                if (response.idChild != null)
                {
                    ShowRegisterChildConfirmationPopup();
                }
            });
    }
    #endregion

    #region Update Child
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

        string actualTime = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        string fNacimientoChildUpdate = m_InputFechaDiaChildUpdate.text + "-" + m_InputFechaMesChildUpdate.text + "-" + m_InputFechaAnioChildUpdate.text;
        DateTime fNacimientoChildDATETIMEUpdate;

        if (m_InputNombreChildUpdate.text == "" || m_InputApellidoChildUpdate.text == "" || m_InputFechaAnioChildUpdate.text == "" || m_InputFechaMesChildUpdate.text == "" || m_InputFechaDiaChildUpdate.text == "" || generoChildUpdate == "" || nivelAutismoChildUpdate == "" || avatarChildUpdate == "")
        {
            ShowErrorPopup("Verifique no dejar campos vacíos");
            return;
        }
        if (DateTime.TryParse(fNacimientoChildUpdate, out fNacimientoChildDATETIMEUpdate) == false)
        {
            ShowErrorPopup("Fecha mal ingresada");
            return;
        }
        if (DateTime.UtcNow.ToLocalTime().Subtract(fNacimientoChildDATETIMEUpdate).Days < 2 * 365)
        {
            ShowErrorPopup("El niño debe tener más de 2 años");
            return;
        }

        CallUpdateChildApi(loggedChild.idChild, m_InputNombreChildUpdate.text, m_InputApellidoChildUpdate.text, avatarChildUpdate, m_InputFechaAnioChildUpdate.text + "-" + m_InputFechaMesChildUpdate.text + "-" + m_InputFechaDiaChildUpdate.text, generoChildUpdate, nivelAutismoChildUpdate, sintomas2,
            delegate (ChildDataResponse response)
            {
                if (response.idChild != null)
                {
                    ShowUpdateChildProfileConfirmationPopup();
                }
            });
    }
    #endregion

    #region Delete Child
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
    #endregion

    #region Specialist
    public void getCodigoNino(){
        if(premiumOn == true)
        {
            CallGetRequestSpecialistByIdChild(loggedChild.idChild, delegate(Specialist getResponse)
            {
                if(getResponse.idSpecialist > 0) 
                {
                    m_UsernameSpecialist.text = getResponse.username;
                    ShowCodigoNinoWindow();
                }
                else
                {
                    CallPostRequestSpecialistFromChild(loggedChild.idChild, delegate (Specialist postResponse)
                    {
                        if(postResponse.idSpecialist > 0)
                        {
                            m_UsernameSpecialist.text = postResponse.username;
                            ShowCodigoNinoWindow();
                        }
                    });
                }
            });

        }
        else if(premiumOn == false){
            m_MessageWindowResponseText.text = "Debe adquirir la versión premium";
            ShowMessageWindow();
        }
    }
    #endregion

    #region Favorite Level List
    private void PopulateFavoriteLevels() 
    {
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

    public void AddFavoriteLevel()
    {
        ConfirmPopup.ConfirmOperation("¿Desea añadir este nivel a " + favoritesListManager.Name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallAddFavoriteLevelApi(Int32.Parse(loggedChild.idChild), nivelSeleccionado, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    LevelButtonListItem levelListItemToAdd = levelButtonListItems.Where(e => e.levelId == nivelSeleccionado).First();
                    favoritesListManager.Add(levelListItemToAdd);
                    m_EmptyFavoriteLevelsListMessageTextLabel.gameObject.SetActive(false);
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
                    if(favoritesListManager.isEmpty())
                        m_EmptyFavoriteLevelsListMessageTextLabel.gameObject.SetActive(true);
                }
                InformationPopup.PopupMessage(response.message);
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }
    #endregion

    #region Custom Level List
    private void PopulateCustomLists() 
    {
        customListList.RemoveAll();

        foreach(CustomList customList in loggedChildCustomLists) {
            customListList.CreateList(customList.idCustomLevelList, customList.name, customListManager);
            ListManager listManager = customListList.getLists().Last();

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
                    customListList.CreateList(response.idResponse, name, customListManager);
                }
                InformationPopup.PopupMessage(response.message);
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
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

    public void EditCustomList(ListManager listManager)
    {
        string name = listManager.editNameInputField.text;

        if (name.Length == 0) {
            customListManager.SwitchEditMode();
            return;
        }

        ConfirmPopup.ConfirmOperation("¿Desea cambiar el nombre de la lista " + listManager.Name + " por " + name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallEditCustomListApi(listManager.Id, name, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    customListManagerLabel.text = name;
                    customListManager.Name = name;
                    customListManager.SwitchEditMode();
                    customListList.ReorderButtons();
                }
                InformationPopup.PopupMessage(response.message);
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    public void DeleteCustomList(ListManager listManager)
    {
        ConfirmPopup.ConfirmOperation("¿Desea eliminar la lista " + listManager.Name + "?", () => {
            ConfirmPopup.SetLoadingState(true);
            CallDeleteCustomListApi(Int32.Parse(loggedChild.idChild), listManager.Id, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    customListList.RemoveList(listManager);
                    ShowUI(m_SeleccionarCategoriaUI);
                }
                InformationPopup.PopupMessage(response.message);
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

    public void SetCustomListActive(ListManager listManager)
    {
        listManager.Refresh();
        customListManagerLabel.text = listManager.Name;
        customListManager = listManager;

        if(!listManager.isEmpty())
        {
            m_EmptyCustomListMessageTextLabel.text = "";
        }
        else
        {
            m_EmptyCustomListMessageTextLabel.text = "No se ha agregado ningún nivel";
        }

        if (customListManager.deleteButton != null) {
            customListManager.deleteButton.onClick.RemoveAllListeners();
            customListManager.deleteButton.onClick.AddListener(() => {
                DeleteCustomList(listManager);
            });
        }
        if (customListManager.saveButton != null) {
            customListManager.saveButton.onClick.RemoveAllListeners();
            customListManager.saveButton.onClick.AddListener(() => {
                EditCustomList(listManager);
            });
        }
        if (customListManager.editButton != null) {
            customListManager.editButton.onClick.RemoveAllListeners();
            customListManager.editButton.onClick.AddListener(() => {
                listManager.SwitchEditMode();
            });
        }
    }
    #endregion

    #region Premium Service
    public void GetPremiumFormSubmit() {
        ConfirmPopup.ConfirmOperation("¿Confirmar adquirir la versión premium?", () => {
            ConfirmPopup.SetLoadingState(true);

            string cardNumber = m_InputNumeroTarjeta.text;
            string expiryMonth = m_InputMesCaducidad.text;
            string expiryYear = m_InputAnoCaducidad.text;
            string ccv = m_InputCCV.text;

            CallPremiumPaymentApi(cardNumber, expiryMonth, expiryYear, ccv, delegate (DefaultResponse response){
                if (response.idResponse >= 0) {
                    datosUsuarioLogeado.premium = true;
                    SwitchPremium(datosUsuarioLogeado.premium);
                    InformationPopup.PopupMessage(response.message);
                    ShowPerfilsGuardados();
                }
                if (m_InputNumeroTarjeta.text == "" || m_InputMesCaducidad.text == "" || m_InputAnoCaducidad.text == "" ) {
                    ConfirmPopup.SetLoadingState(false);
                    ShowErrorPopup("Verifica que ningún campo este vacío");
                }
                 if (response.idResponse != 1 && m_InputNumeroTarjeta.text != "" && m_InputMesCaducidad.text != "" && m_InputAnoCaducidad.text != ""  ) {
                    ConfirmPopup.SetLoadingState(false);
                    ShowErrorPopup("Los datos no son correctos");
                }
                ConfirmPopup.SetLoadingState(false);
            });
        }, () => {});
    }

    public void SwitchPremium(bool on) {
        premiumOn = on;

        m_PremiumAceptarBoton.gameObject.SetActive(on);
        m_PremiumNoBoton.gameObject.SetActive(!on);
        m_PremiumSiBoton.gameObject.SetActive(!on);

        m_PremiumTextLabel.text = on ? "¡Ya eres premium!" : "¿Está seguro que desea adquirir el servicio premium?" ;
    }
    #endregion

    #region Guardian Endpoints
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

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            ShowErrorPopup("Requiere conectividad de internet");
        }
        else
        {
            response(JsonUtility.FromJson<LoginResponse>(uwr.downloadHandler.text));
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

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
        }
    }

    private void CallGetRequestGuardianApi(string id, Action<GuardianData> response)
    {
        StartCoroutine(GetRequestGuardian("https://teapprendo.herokuapp.com/guardians/listByIdGuardian?idGuardian=" + id, response ));    
    }

    IEnumerator GetRequestGuardian(string url, Action<GuardianData> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<GuardianData>(uwr.downloadHandler.text));
        }
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

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
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

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<GuardianData>(uwr.downloadHandler.text));
        }
    }
    #endregion

    #region Child Endpoints
    private void CallGetRequestChildrenApi(string id, Action<ChildDataResponse[]> response)
    {
        StartCoroutine(GetRequestChildren("https://teapprendo.herokuapp.com/children/listByIdGuardian?idGuardian=" + id, response));
    }

    IEnumerator GetRequestChildren(string url, Action<ChildDataResponse[]> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonHelper.FromJson<ChildDataResponse>(fixJson(uwr.downloadHandler.text)));
        }
    }

    private void CallRegisterChildApi(string guardian_id, string names, string lastnames,string avatarChild, string birthday, string gender, string asdlevel, int[] symptoms, Action<ChildDataResponse> response)
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

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
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

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<ChildDataResponse>(uwr.downloadHandler.text));
        }
    }

    private void CallGetRequestDeleteChildApi(string id, Action<DefaultResponse> response)
    {
        StartCoroutine(GetRequestDeleteChild("https://teapprendo.herokuapp.com/children/delete?idChild=" + loggedChild.idChild, response));
    }

    IEnumerator GetRequestDeleteChild(string url, Action<DefaultResponse> response)
    {
        var uwr = new UnityWebRequest(url, "DELETE");
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
        }
    }
    #endregion

    #region Favorite Levels Endpoints
    private void CallGetChildrenFavoriteLevelsApi(int idChild, Action<Level[]> response)
    {
        StartCoroutine(GetChildrenFavoriteLevels("https://teapprendo.herokuapp.com/children/listFavoriteLevels?idChild=" + idChild, response));
    }

    IEnumerator GetChildrenFavoriteLevels(string url, Action<Level[]> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonHelper.FromJson<Level>(fixJson(uwr.downloadHandler.text)));
        }
    }

    private void CallAddFavoriteLevelApi(int idChild, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelDto addLevelDto = new AddLevelDto();
        addLevelDto.idChild = idChild;
        addLevelDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelDto);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/addFavoriteLevel", json, response));
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

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
        }
    }

    private void CallDeleteFavoriteLevelApi(int idChild, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelDto addLevelDto = new AddLevelDto();
        addLevelDto.idChild = idChild;
        addLevelDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelDto);
        StartCoroutine(DeleteFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/deleteFavoriteLevel", json, response));
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

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
        }
    }
    #endregion

    #region Custom Level List Endpoints
    private void CallGetChildrenCustomLevelListsApi(int idChild, Action<CustomList[]> response)
    {
        StartCoroutine(GetChildrenCustomLevelLists("https://teapprendo.herokuapp.com/children/listCustomLevelLists?idChild=" + idChild, response));
    }

    IEnumerator GetChildrenCustomLevelLists(string url, Action<CustomList[]> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonHelper.FromJson<CustomList>(fixJson(uwr.downloadHandler.text)));
        }
    }

    private void CallAddCustomListApi(int idChild, string name, Action<DefaultResponse> response)
    {
        AddCustomLevelListDto addCustomLevelListDto = new AddCustomLevelListDto();
        addCustomLevelListDto.idChild = idChild;
        addCustomLevelListDto.name = name;
        string json = JsonUtility.ToJson(addCustomLevelListDto);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/addCustomLevelList", json, response));
    }

    private void CallAddCustomLevelApi(int idCustomLevelList, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelToCustomListDto addLevelToCustomListDto = new AddLevelToCustomListDto();
        addLevelToCustomListDto.idCustomLevelList = idCustomLevelList;
        addLevelToCustomListDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelToCustomListDto);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/addLevelToCustomLevelList", json, response));
    }

    private void CallEditCustomListApi(int idCustomLevelList, string name, Action<DefaultResponse> response)
    {
        EditCustomLevelListDto editCustomLevelListDto = new EditCustomLevelListDto();
        editCustomLevelListDto.idCustomLevelList = idCustomLevelList;
        editCustomLevelListDto.name = name;
        string json = JsonUtility.ToJson(editCustomLevelListDto);
        StartCoroutine(PutRequestUpdateCustomList("https://teapprendo.herokuapp.com/children/updateNameCustomLevelList", json, response));
    }

    IEnumerator PutRequestUpdateCustomList(string url, string json, Action<DefaultResponse> response)
    {
        var uwr = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<DefaultResponse>(uwr.downloadHandler.text));
        }
    }

    private void CallDeleteCustomListApi(int idChild, int idCustomLevelList, Action<DefaultResponse> response)
    {
        StartCoroutine(DeleteFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/deleteCustomLevelList?idChild=" + idChild + "&idCustomLevelList=" + idCustomLevelList, "{}", response));
    }

    private void CallDeleteLevelFromCustomListApi(int idCustomLevelList, int idLevel, Action<DefaultResponse> response)
    {
        AddLevelToCustomListDto addLevelToCustomListDto = new AddLevelToCustomListDto();
        addLevelToCustomListDto.idCustomLevelList = idCustomLevelList;
        addLevelToCustomListDto.idLevel = idLevel;
        string json = JsonUtility.ToJson(addLevelToCustomListDto);
        StartCoroutine(DeleteFavoriteLevelRequest("https://teapprendo.herokuapp.com/children/deleteLevelinCustomLevelList", json, response));
    }
    #endregion

    #region Level Record Endpoints
    private void CallGetCompletedLevelsApi(int IdChild, Action<LevelRecord[]> response) {
        StartCoroutine(GetCompletedLevels("https://teapprendo.herokuapp.com/levelRecords/listByIdChild?idChild=" + IdChild, response));
    }

    IEnumerator GetCompletedLevels(string url, Action<LevelRecord[]> response) {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonHelper.FromJson<LevelRecord>(fixJson(uwr.downloadHandler.text)));
        }
    }
    #endregion

    #region Specialist Endpoints
    private void CallPostRequestSpecialistFromChild(string idChild, Action<Specialist> response)
    {
        StartCoroutine(PostRequestSpecialistFromChild("https://teapprendo.herokuapp.com/children/activateSpecialist?idChild=" + idChild, response));
    }

    IEnumerator PostRequestSpecialistFromChild(string url, Action<Specialist> response)
    {
        UnityWebRequest uwr = new UnityWebRequest(url, "POST");
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<Specialist>(uwr.downloadHandler.text));
        }
    }

    private void CallGetRequestSpecialistByIdChild(string idChild, Action<Specialist> response)
    {
        StartCoroutine(GetRequestSpecialistByIdChild("https://teapprendo.herokuapp.com/specialists/listByIdChild?idChild=" + idChild, response));
    }

    IEnumerator GetRequestSpecialistByIdChild(string url, Action<Specialist> response)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        string token = PlayerPrefs.GetString("token");
        uwr.SetRequestHeader("Authorization", token);

        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            response(JsonUtility.FromJson<Specialist>(uwr.downloadHandler.text));
        }
    }
    #endregion

    #region Premium Service Endpoints
    private void CallPremiumPaymentApi(string cardNumber, string expiryMonth, string expiryYear, string ccv, Action<DefaultResponse> response)
    {
        PremiumPaymentDto premiumPaymentDto = new PremiumPaymentDto();
        premiumPaymentDto.cardNumber = cardNumber;

        string dueDate = expiryMonth.Length == 1 ? "0" + expiryMonth : expiryMonth;
        dueDate += "/" + (expiryYear.Length > 2 ? expiryYear.Substring(expiryYear.Length - 2) : expiryYear);

        premiumPaymentDto.dueDate = dueDate;
        premiumPaymentDto.ccv = ccv;
        premiumPaymentDto.idGuardian = Int32.Parse(id_guardian);
        string json = JsonUtility.ToJson(premiumPaymentDto);
        StartCoroutine(AddFavoriteLevelRequest("https://teapprendo.herokuapp.com/payment/payPremium", json, response));
    }
    #endregion

    #region Show UI
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

    public void ShowRegister() {
        ShowUI(m_RegisterUI);
    }

    public void ShowResetPassword(){
        m_ResetPasswordWindow.SetActive(true);
    }

    public void CloseResetPassword(){
        blankResetPassword();
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

    public void ShowPerfilNiño(){
        if(cantNinos>= 6)
            ShowErrorPopup("Solo puede tener 6 cuentas");
        else
        {
            blankRegisterChildSpace();
            ShowUI(m_PerfilNiñoCrearUI);
        }
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

    public void ShowRegisterConfirmationPopup(){
        m_RegisterConfirmationPopupUI.SetActive(true);
    }

    public void ShowRegisterChildConfirmationPopup(){
        m_RegisterChildConfirmationPopupUI.SetActive(true);
    }

    public void ShowUpdateProfileConfirmationPopup(){
        m_UpdateProfileConfirmationPopupUI.SetActive(true);
    }

    public void ShowUpdateChildProfileConfirmationPopup(){
        m_UpdateChildProfileConfirmationPopupUI.SetActive(true);
    }

    public void ShowChangeChildProfileConfirmationPopup(){
        m_ChangeChildProfileConfirmationPopupUI.transform.Find("Text").GetComponent<Text>().text = "¿Está seguro de salir del perfil de " + loggedChild.names + "?";
        m_ChangeChildProfileConfirmationPopupUI.SetActive(true);
    }

    public void ShowChangeAvatarConfirmationPopup(){
        m_ChangeAvatarConfirmationPopupUI.SetActive(true);
    }

    public void ShowErrorPopup(string message){
        m_ErrorLoginPopupUI.transform.Find("Text").GetComponent<Text>().text = message;
        m_ErrorLoginPopupUI.SetActive(true);
    }

    public void ShowUnavailableLevelPopup(){
        m_UnavailableLevelPopupUI.SetActive(true);
    }
    
    public void ResetPasswordConfirmationPopup(){
        CloseResetPassword();
        m_ResetPasswordConfirmationPopupUI.SetActive(true);
    }
    #endregion
    #region Popup Change Level - Theme - Category
        #region ChangeLvl
    public void PromptChooselvlMath()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de nivel?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowChooselvlMath();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }

    public void PromptChooselvlComm()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de nivel?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowChooselvlComm();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }

    public void PromptChooselvlPSocial()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de nivel?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowChooselvlPSocial();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }

    public void PromptChooselvlScience()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de nivel?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowChooselvlScience();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }
        #endregion

        #region ChangeTheme
    public void PromptChooseThemeMath()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de tema?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowTemaMath();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }

    public void PromptChooseThemeComm()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de tema?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowTemaComm();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }

    public void PromptChooseThemePSocial()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de tema?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowTemaPSocial();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }

    public void PromptChooseThemeScience()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de tema?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowTemaScience();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }
        #endregion

        #region ChangeCategory
    public void PromptChooseCategory()
    {
        ConfirmPopup.ConfirmOperation("¿Desea cambiar de categoría?", () => {
            ConfirmPopup.gameObject.SetActive(false); ShowCategoria();
        }, () => { ConfirmPopup.gameObject.SetActive(false); });
    }
    #endregion
    #endregion

    public void PromptLoading()
    {
        WaitPopup.PopupMessage("Cargando, por favor espere");
        //ConfirmPopup.ConfirmOperation("¿Desea cambiar de nivel?", () => {
        //    ConfirmPopup.gameObject.SetActive(false); ShowChooselvlMath();
        //}, () => { ConfirmPopup.gameObject.SetActive(false); });
    }
    

    #region Set Guardian Profile Data
    public void setGuardianProfileData() 
    {
        m_NombreGuardian.text = datosUsuarioLogeado.names;
        m_ApellidoGuardian.text = datosUsuarioLogeado.lastNames;
        m_FechaNacimientoGuardian.text = datosUsuarioLogeado.birthday.Substring(8, 2) + "/" + datosUsuarioLogeado.birthday.Substring(5, 2) + "/" + datosUsuarioLogeado.birthday.Substring(0, 4);
        m_EmailGuardian.text = datosUsuarioLogeado.email;
    }
    #endregion

    #region Set Guardian Profile Data - Update
    public void setProfileData()
    {
        m_InputCorreoUpdate.text = datosUsuarioLogeado.email;
        m_InputNombreUpdate.text = datosUsuarioLogeado.names;
        m_InputApellidoUpdate.text = datosUsuarioLogeado.lastNames;
        m_InputFechaDiaUpdate.text = datosUsuarioLogeado.birthday.Substring(8, 2);
        m_InputFechaMesUpdate.text = datosUsuarioLogeado.birthday.Substring(5, 2);
        m_InputFechaAnioUpdate.text = datosUsuarioLogeado.birthday.Substring(0, 4);
        m_InputContrasenaActualUpdate.text = m_InputContrasenaLogin.text;
        m_ErrorUpdateGuardianText.text = "";
    }
    #endregion

    #region Set Child Profile Data
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
    #endregion

    #region Set Child Profile Data - Update
    public void setChildPerfil()
    {
        m_InputNombreChildUpdate.text = loggedChild.names;
        m_InputApellidoChildUpdate.text = loggedChild.lastNames;
        m_InputFechaDiaChildUpdate.text = loggedChild.birthday.Substring(8, 2);
        m_InputFechaMesChildUpdate.text = loggedChild.birthday.Substring(5, 2);
        m_InputFechaAnioChildUpdate.text = loggedChild.birthday.Substring(0, 4);

        if(loggedChild.gender == "M")
            m_SexoMasculinoButton.onClick.Invoke();
        else if(loggedChild.gender == "F")
            m_SexoFemeninoButton.onClick.Invoke();
        
        if(loggedChild.asdLevel == "Leve")
            m_GradoLeveButton.onClick.Invoke();
        else if(loggedChild.asdLevel == "Moderado")
            m_GradoModeradoButton.onClick.Invoke();
        else if(loggedChild.asdLevel == "Grave")
            m_GradoGraveButton.onClick.Invoke();

        if(loggedChild.avatar == "avatar1")
            m_AvatarMasculinoButton.onClick.Invoke();
        else if(loggedChild.avatar == "avatar2")
            m_AvatarFemeninoButton.onClick.Invoke();
        else if(loggedChild.avatar == "avatar3")
            m_AvatarAdicionalButton.onClick.Invoke();
    }

    public void ResetAvatar(){
        m_AvatarMasculinoButton.onClick.Invoke();
    }
    #endregion

    #region Validate Email
    public bool validateEmail(string Email)
    {
        String Format;
        Format = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(Email, Format))
        {
            if (Regex.Replace(Email, Format, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Convert To Lowercase - Registry Username
    public void ConvertRegistryUsernameToLowerCase()
    {
        m_InputUsuario.text = m_InputUsuario.text.ToLower();
    }
    #endregion

    #region Convert To Lowercase - Login Username
    public void ConvertLoginUsernameToLowerCase()
    {
        m_InputUsuarioLogin.text = m_InputUsuarioLogin.text.ToLower();
    }
    #endregion

    #region FixJSON
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    #endregion

    #region Empty Login
    void blankLogin()
    {
        m_InputContrasenaLogin.text = "";
        m_InputUsuarioLogin.text = "";
    }
    #endregion

    #region Empty Input Field - Guardian
    void blankRegisterSpace()
    {
        m_InputNombre.text = "";
        m_InputApellido.text = "";
        m_InputUsuario.text = "";
        m_InputFechaAnio.text = "";
        m_InputFechaMes.text = "";
        m_InputFechaDia.text = "";
        m_InputCorreo.text = "";
        m_InputContrasena.text = "";
        m_InputContrasenaConf.text = "";
    }
    #endregion

    #region Empty Input Field - Child
    void blankRegisterChildSpace()
    {
        m_InputNombreChild.text = "";
        m_InputApellidoChild.text = "";
        m_InputFechaDiaChild.text = "";
        m_InputFechaMesChild.text = "";
        m_InputFechaAnioChild.text = "";
        nivelAutismoChild = "";
        generoChild = "";
        avatarChild = "";
        m_ErrorCreateChildText.text = "";
    }
    #endregion

    #region Empy Input Field - Reset Password
    void blankResetPassword()
    {
        m_ResetPasswordEmail.text = "";
        m_ErrorTextResetPassword.text = "";
    }
    #endregion

    #region Data Response Classes
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
        public bool premium;
    }

    private class GuardianResponse
    {
        public int idResponse;
        public string message;
        public string token;
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
        public bool premium;
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
    public class AddLevelDto 
    {
        public int idChild;
        public int idLevel; 
    }

    [Serializable]
    public class AddCustomLevelListDto 
    {
        public int idChild;
        public string name; 
    }

    [Serializable]
    public class AddLevelToCustomListDto 
    {
        public int idCustomLevelList;
        public int idLevel;
    }

    [Serializable]
    public class EditCustomLevelListDto 
    {
        public int idCustomLevelList;
        public string name;
    }

    [Serializable]
    public class PremiumPaymentDto 
    {
        public string cardNumber;
        public string dueDate;
        public string ccv;
        public int idGuardian;
    }

    [Serializable]
    public class Level 
    {
        public int idLevel;
        public string description;
        public Topic topic;
        public string video;
    }

    [Serializable]
	public class LevelRecord 
    {
        public int idLevelRecord;
		public Level level;
	}

    [Serializable]
    public class Topic 
    {
        public int idTopic;
        public string description;
        public Category category;
    }

    [Serializable]
    public class Category 
    {
        public int idCategory;
        public string description;
    }

    [Serializable]
    public class Symptom 
    {
        public int idSymptom;
        public string description;
    }

    [Serializable]
    public class CustomList 
    {
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
    }
    #endregion   
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