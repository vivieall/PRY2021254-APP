using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonFecha : MonoBehaviour
{
    public Button btnIzqMes, btnDerMes, btnIzqAno, btnDerAno;
    public List<Button> botonesDias;
    public Button botonFecha;
    public Text textMes, textAno, textFecha;
    public GameObject insBotonDia, contenedorDias, calendario;
    public bool calendarioAbierto;
    public Action onValueChanged;
    public int dia, mes, anio;
    int diap, mesp, aniop;
    int cantDias;

    // Start is called before the first frame update
    void Start()
    {
        DefinirTamanoDias();
        calendarioAbierto = false;
        calendario.SetActive(false);
        InstanciarBotones();
        if (anio == 0)
        {
            ObtenerFechaActual();
        }
        botonFecha.onClick.AddListener(() =>
       {
           MostrarOcultarCalendario();
       });
        //Mes anterior
        btnIzqMes.onClick.AddListener(() =>
       {
           mes -= 1;
           if (mes < 1)
           {
               mes = 12;
               anio -= 1;
           }
           DefinirCalendario( mes, anio );
       });
        //Mes siguiente
        btnDerMes.onClick.AddListener(() =>
        {
            mes += 1;
            if (mes > 12)
            {
                mes = 1;
                anio += 1;
            }
            DefinirCalendario(mes, anio);
        });
        // Año anterior
        btnIzqAno.onClick.AddListener( () =>
        {
            anio -= 1;
            DefinirCalendario(mes, anio);
        });
        btnDerAno.onClick.AddListener(() =>
        {
            anio += 1;
            DefinirCalendario(mes, anio);
        });
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void InstanciarBotones()
    {
        for (int x = 0; x < 42; x++)
        {
            GameObject dia = Instantiate(insBotonDia, contenedorDias.transform);
            dia.GetComponent<Button>().onClick.AddListener( () =>
            {
                this.dia = int.Parse( dia.GetComponentInChildren<Text>().text );
                EstablecerFecha(this.dia, mes, anio);
                MostrarOcultarCalendario();
            });
            botonesDias.Add(dia.GetComponent<Button>());
        }
    }

    private void DefinirTamanoDias()
    {
        Vector2 tamano = contenedorDias.GetComponent<RectTransform>().sizeDelta;
        int altura = (int)tamano.y / 6;
        int longitud = (int)tamano.x / 7;
        contenedorDias.GetComponent<GridLayoutGroup>().cellSize = new Vector2(longitud, altura);
    }

    public void MostrarOcultarCalendario()
    {
        if (calendarioAbierto)
        {
            if (onValueChanged != null)
            {
                if (diap != dia || mesp != mes || aniop != anio)
                {
                    onValueChanged.Invoke();
                    diap = dia;
                    mesp = mes;
                    aniop = anio;
                }
            }
        }
        EstablecerFecha( dia, mes, anio);
        calendarioAbierto = !calendarioAbierto;
        calendario.SetActive(calendarioAbierto);
        DefinirTamanoDias();
    }

    public void EstablecerFecha( int dia, int mes, int anio)
    {
        textFecha.text = dia + " / " + MesString(mes) + " (" + mes + ") / " + anio;
    }

    private void ObtenerFechaActual()
    {
        dia = DateTime.Today.Day;
        mes = DateTime.Today.Month;
        anio = DateTime.Today.Year;
        DefinirCalendario(mes, anio);
    }

    private void DefinirCalendario(int mes, int anio)
    {
        cantDias = DateTime.DaysInMonth(anio, mes);
        int primerDia = PrimerDia(mes, anio);
        textMes.text = MesString(mes);
        textAno.text = anio.ToString();
        HabilitarDias();
        //Inhabilitar botones al principio
        for (int x = 0; x < primerDia; x++)
        {
            contenedorDias.transform.GetChild(x).GetComponent<Button>().enabled = false;
        }
        //Inhabilitar botones al final
        for (int x = primerDia + cantDias; x < 42; x++)
        {
            contenedorDias.transform.GetChild(x).GetComponent<Button>().enabled = false;
        }
        //Definir numeración
        int diaCuenta = 1;
        for (int x = 0; x < 42; x++)
        {
            if (contenedorDias.transform.GetChild(x).GetComponent<Button>().enabled)
            {
                contenedorDias.transform.GetChild(x).GetComponentInChildren<Text>().text = diaCuenta.ToString();
                diaCuenta++;
            }
            else
            {
                contenedorDias.transform.GetChild(x).GetComponentInChildren<Text>().text = "";
            }
        }
    }

    private int PrimerDia(int mes, int anio)
    {
        int diaSemana;
        DateTime fechaInicio = new DateTime(anio, mes, 1);
        switch (fechaInicio.DayOfWeek.ToString())
        {
            case "Sunday":
                diaSemana = 0;
                break;
            case "Monday":
                diaSemana = 1;
                break;
            case "Tuesday":
                diaSemana = 2;
                break;
            case "Wednesday":
                diaSemana = 3;
                break;
            case "Thursday":
                diaSemana = 4;
                break;
            case "Friday":
                diaSemana = 5;
                break;
            case "Saturday":
                diaSemana = 6;
                break;
            default:
                diaSemana = 0;
                break;
        }
        return diaSemana;
    }

    private void HabilitarDias()
    {
        for (int x = 0; x < contenedorDias.transform.childCount; x++)
        {
            contenedorDias.transform.GetChild(x).GetComponent<Button>().enabled = true;
        }
    }

    public string MesString( int mesActual )
    {
        string[] mesString = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        return mesString[ mesActual - 1 ];
    }
}
