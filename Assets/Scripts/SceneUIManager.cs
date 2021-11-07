using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class SceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_LoguinUI;
    [SerializeField] private GameObject m_RegisterUI;
    [SerializeField] private GameObject m_PerfilNiñoCrearUI;
    [SerializeField] private GameObject m_PerfilesGuardadosUI = null;
    [SerializeField] private GameObject m_ActualizarDatosUI = null;
    [SerializeField] private GameObject m_VerDatosCuidadorUI = null;
    [SerializeField] private GameObject m_SeleccionarCategoriaUI = null;
    [SerializeField] private GameObject m_SeleccionarTemaUI = null;
=======
    [SerializeField] private GameObject m_PerfilesGuardadosUI= null;
    [SerializeField] private GameObject m_ActualizarDatosUI= null;
    [SerializeField] private GameObject m_VerDatosCuidadorUI= null;
    [SerializeField] private GameObject m_SeleccionarCategoriaUI= null;
    [SerializeField] private GameObject m_SeleccionarTemaUI= null;
    [SerializeField] private GameObject m_SeleccionarTematicaUI = null;
    [SerializeField] private GameObject m_AnimalesFiltroUI = null;
    [SerializeField] private GameObject m_PersonajesFiltroUI = null;
    [SerializeField] private GameObject m_VariedadesFiltroUI = null;
    [SerializeField] private GameObject m_NivelesCompletosUI = null;
    [SerializeField] private GameObject m_PerfilNinoModificarsUI = null;
    [SerializeField] private GameObject m_ListaPersonalizadaUI = null;
    [SerializeField] private GameObject m_NivelesCompletosUI= null;
    [SerializeField] private GameObject m_PerfilNinoModificarsUI= null;
    [SerializeField] private GameObject m_ListaPersonalizadaUI= null;
    [SerializeField] private GameObject m_PerfilNinoVistaDatosUI;
    [SerializeField] private GameObject m_ModificarListaPersonalizUI;
    [SerializeField] private GameObject m_HistorialUI;
    [SerializeField] private GameObject m_Tema1UI;
    [SerializeField] private GameObject m_Nivel1UI;
    [SerializeField] private GameObject m_FiltroUI;
    private ArrayList AllUIs;

    #region Register Guardian
    [Header("Register Guardian Inputs")]
    [SerializeField] private InputField m_InputCorreo;
    [SerializeField] private InputField m_InputContrasena;
    [SerializeField] private InputField m_InputNombre;
    [SerializeField] private InputField m_InputApellido;
    [SerializeField] private InputField m_InputFechaDia;
    [SerializeField] private InputField m_InputFechaMes;
    [SerializeField] private InputField m_InputFechaAnio;
    [SerializeField] private InputField m_InputUsuario;

    #endregion


    #region Register Child
    [Header("Register Child Inputs")]
    [SerializeField] private InputField m_InputNombreChild;
    [SerializeField] private InputField m_InputApellidoChild;
    [SerializeField] private InputField m_InputFechaDiaChild;
    [SerializeField] private InputField m_InputFechaMesChild;
    [SerializeField] private InputField m_InputFechaAnioChild;
    [SerializeField] private InputField m_GeneroChild;

    #endregion

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

    [Header("Direcciones de correo")]
    public string[] Emails;
    public bool cuentaRegistradaConExito;
    public int MaxLenght;


    private bool sesionIniciada;
    private string id_guardian;
    private string nivelAutismoChild;
    private string generoChild;
    private bool[] sintomas = new bool[] { false, false, false, false, false, false, false };

    public void setGenderM()
    {
        generoChild = "M";
    }

    public void setGenderF()
    {
        generoChild = "F";
    }

    public void setNivelAutismoL()
    {
        nivelAutismoChild = "Leve";
    }
    public void setNivelAutismoM()
    {
        nivelAutismoChild = "Moderado";
    }
    public void setNivelAutismoG()
    {
        nivelAutismoChild = "Grave";
    }

    public void setSintoma(int idx)
    {
        sintomas[idx] = !sintomas[idx];
    }

    void Start()
    {

        sesionIniciada = false;
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
		AllUIs.Add(m_Tema1UI);
		AllUIs.Add(m_Nivel1UI);
        AllUIs.Add(m_FiltroUI);

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
        var state = GameObject.Find("PersistantObject").GetComponent<PersistanceHandler>().GetState();
        if (state == 1)
        {
            ShowUI(m_Nivel1UI);
        }
        else if (state == 2)
        {
            ShowPerfilsGuardados();
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
        CallLogin(m_InputUsuarioLogin.text, m_InputContrasenaLogin.text, delegate (Response response)
        {
            m_ErrorText.text = "Logueando espere un momento";
            m_ErrorText.text = response.message;

            if (response.done)
            {
                if (toggleSesion.isOn)
                {
                    PlayerPrefs.SetString("SavePasswordToggle_Data", m_InputContrasenaLogin.text);
                    PlayerPrefs.SetString("SaveUserToggle_Data", m_InputUsuarioLogin.text);
                    var valueSave = Convert.ToInt32(toggleSesion.isOn);
                    PlayerPrefs.SetInt("toggleIsOn", valueSave);
                }
                sesionIniciada = true;
                id_guardian = response.message;
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
                            m_InputNombre.text, m_InputApellido.text, m_InputFechaAnio.text + "-" + m_InputFechaMes.text + "-" + m_InputFechaDia.text,
                            delegate (Response response)
                        {

                            m_ErrorText.text = response.message;

                            print(response.message);
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

    public void SubmitRegisterChild()
    {     
        CallRegisterChild(id_guardian, m_InputNombreChild.text, m_InputApellidoChild.text, m_InputFechaAnioChild.text + "-" + m_InputFechaMesChild.text + "-" + m_InputFechaDiaChild.text, generoChild, nivelAutismoChild, sintomas,
            delegate (Response response)
            {


                print(response.message);
                if (response.done == true)
                {
                    print("Cuenta creada con exito");
                }
                else
                {
                    ///ACCION AL NO REGISTRAR CUENTA

                }
            });
            
        
            

        
    }
    public void ShowUI_GoBack(GameObject UIToShow) {
        foreach(GameObject m_ui in AllUIs) {
            m_ui.SetActive(false);
		}
        if (UIToShow) 
            UIToShow.SetActive(true);
	}

    public void ShowUI(GameObject UIToShow) {

        GameObject currentlyActiveUI = null;
        foreach(GameObject m_ui in AllUIs) {
            if (m_ui.activeInHierarchy) 
                currentlyActiveUI = m_ui;
            m_ui.SetActive(false);
		}

        UIScreenComponent screenComp = UIToShow.GetComponent<UIScreenComponent>();
        if (screenComp)
		{
            screenComp.ShowUIFromParent(currentlyActiveUI);
		}
		else
		{
            UIToShow.SetActive(true);
		}
	}

	//Se puede mejorar estas funciones creando una que solo reciba la funcion especifica y que solo cambie el que se ponga true 
	public void ShowLoguin(){

        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(true);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_HistorialUI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        
    }
    public void ShowRegister(){
        m_RegisterUI.SetActive(true);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
    public void ShowPerfilNiño(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(true);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
        public void ShowPerfilsGuardados(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(true);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);

    }
        public void ShowActualizarDatos(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(true);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);  
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);

    }
        public void ShowVerDatosCuidador(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(true);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);

    }
        public void ShowCategoria(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(true);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);

    }
    public void ShowTema(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(true);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);

    }
    public void ShowNivelesCompletos(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(true); 
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);       
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }


    public void ShowNinoVistaDatos(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);     
        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(true);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
    public void ShowPerfilNinoModificar(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);   

        m_PerfilNinoModificarsUI.SetActive(true);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);   
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
    public void ShowListaPersonalizada(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);   

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(true);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);     
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
    public void ShowModificarListaPersonalizada(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false); 

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(true);
        m_HistorialUI.SetActive(false);    
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
    public void ShowHistorial(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);    

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(true);    
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }
    
    public void ShowTema1(){
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);    

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);    
        m_Tema1UI.SetActive(true);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }

    public void ShowTematica()
    {
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(true);
        m_NivelesCompletosUI.SetActive(false);

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }

    public void ShowAnimalesFiltro()
    {
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(true);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(false);
    }

    public void ShowPersonajesFiltro()
    {
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(true);
        m_VariedadesFiltroUI.SetActive(false);
    }

    public void ShowVariedadesFiltro()
    {
        m_RegisterUI.SetActive(false);
        m_LoguinUI.SetActive(false);
        m_PerfilNiñoCrearUI.SetActive(false);
        m_PerfilesGuardadosUI.SetActive(false);
        m_ActualizarDatosUI.SetActive(false);
        m_VerDatosCuidadorUI.SetActive(false);
        m_SeleccionarCategoriaUI.SetActive(false);
        m_SeleccionarTemaUI.SetActive(false);
        m_SeleccionarTematicaUI.SetActive(false);
        m_NivelesCompletosUI.SetActive(false);

        m_PerfilNinoModificarsUI.SetActive(false);
        m_ListaPersonalizadaUI.SetActive(false);
        m_PerfilNinoVistaDatosUI.SetActive(false);
        m_ModificarListaPersonalizUI.SetActive(false);
        m_HistorialUI.SetActive(false);
        m_Tema1UI.SetActive(false);
        m_AnimalesFiltroUI.SetActive(false);
        m_PersonajesFiltroUI.SetActive(false);
        m_VariedadesFiltroUI.SetActive(true);
    }
        

    // Codigo de conexion a la bd se debe enviar a otro script
    public void CallRegister(string user, string pass, string email, string names, string lastnames, string birthday, Action<Response> response)
    {
        StartCoroutine(Register(user, pass, email, names, lastnames, birthday, response));
    }

    IEnumerator Register(string user, string pass, string email, string names, string lastnames, string birthday, Action<Response> response)
    {
        Debug.Log("ENTRANDO");
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", pass);
        form.AddField("names", names);
        form.AddField("lastnames", lastnames);
        form.AddField("username", user);
        form.AddField("birthday", birthday);
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
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


    public void CallRegisterChild(string guardian_id, string names, string lastnames, string birthday, string gender, string asdlevel, bool[] symptoms, Action<Response> response)
    {
        StartCoroutine(RegisterChild(guardian_id, names, lastnames, birthday, gender, asdlevel, symptoms, response));
    }

    IEnumerator RegisterChild(string guardian_id, string names, string lastnames, string birthday, string gender, string asdlevel, bool[] symptoms, Action<Response> response)
    {

        string sintomasStr = "";
        for(int i = 0; i < 7; i++)
        {
            if(symptoms[i] == true)
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


}
