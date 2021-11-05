using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class SceneManager : MonoBehaviour
{
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
    [SerializeField] private GameObject m_Tema1UI;
    [SerializeField] private GameObject m_Nivel1UI;
    [SerializeField] private GameObject m_FiltroUI;
    private ArrayList AllUIs;


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
    private NetworkManager m_NetworkManager;
    public Toggle toggleSesion;

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
		AllUIs.Add(m_Tema1UI);
		AllUIs.Add(m_Nivel1UI);
        AllUIs.Add(m_FiltroUI);

        m_NetworkManager = FindObjectOfType <NetworkManager>();

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

                        //UnityEngine.SceneManagement.SceneManager.LoadScene("m_PerfilNiñoUI");
                        // m_LoguinUI.SetActive(false);
                        //m_PerfilNiñoUI.SetActive(true);
                    }
                });
                //m_LoguinUI.SetActive(false);
               //m_PerfilNiñoUI.SetActive(true);
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

    // Really want to get rid of all these functions
    // as soon as possible and replace every reference to these functions with new ShowUI
	#region PleaseDeleteThisSoon

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
	#endregion
}