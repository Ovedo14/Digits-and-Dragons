using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MenuPrincipal : MonoBehaviour
{

    private UIDocument menuPrincipal;
    private VisualElement principal;
    private VisualElement opciones;
    private VisualElement filter;
    private Button elegirPersonaje;
    private Button cargarPartida;
    private Button salir;
    private Button botonOpciones;
    private Button regresarOpciones;

    void OnEnable()
    {
        menuPrincipal = GetComponent<UIDocument>();
        var root = menuPrincipal.rootVisualElement;

        principal = root.Q<VisualElement>("Principal");
        opciones = root.Q<VisualElement>("Opciones");
        filter = root.Q<VisualElement>("Filter");
        elegirPersonaje = root.Q<Button>("ElegirPersonaje");
        cargarPartida = root.Q<Button>("CargarPartida");
        salir = root.Q<Button>("Salir");
        botonOpciones = root.Q<Button>("BotonOpciones");
        regresarOpciones = root.Q<Button>("RegresarOpciones");
        
        elegirPersonaje.RegisterCallback<ClickEvent>(AbrirElegirPersonaje);
        salir.RegisterCallback<ClickEvent>(CerrarJuego);
        botonOpciones.RegisterCallback<ClickEvent>(AbrirOpciones);
        regresarOpciones.RegisterCallback<ClickEvent>(cerrarOpciones);


    }

    private void AbrirElegirPersonaje(ClickEvent evt)
    {
        SceneManager.LoadScene("CharSelect");
    }

        private void CerrarJuego(ClickEvent evt)
    {
        Debug.Log("Cerrando Juego...");
        Application.Quit();
    }

        private void AbrirOpciones(ClickEvent evt)
    {
        filter.style.opacity = 0.9f;
        opciones.style.display = DisplayStyle.Flex;
    }

        private void cerrarOpciones(ClickEvent evt)
    {
        filter.style.opacity = 0.0f;
        opciones.style.display = DisplayStyle.None;
    }

    void OnDisable()
    {
        elegirPersonaje.UnregisterCallback<ClickEvent>(AbrirElegirPersonaje);
        salir.UnregisterCallback<ClickEvent>(CerrarJuego);
        botonOpciones.UnregisterCallback<ClickEvent>(AbrirOpciones);
        regresarOpciones.UnregisterCallback<ClickEvent>(cerrarOpciones);
    }

    void Update()
    {
        
    }
}
