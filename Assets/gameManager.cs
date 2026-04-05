using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public int pontos = 0;
    public int vidas = 5;
    public bool venceu = false;
    public int inimigosRestantes = 0;
    
    // Configurações do Slow Motion
    [Header("Slow Motion Settings")]
    public float slowMotionFactor = 0.5f; // 0.5 = metade da velocidade
    public float slowMotionDuration = 3f; // Duração em segundos
    
    private TMP_Text textoPontos;
    private TMP_Text textoVidas;
    private bool isSlowMotionActive = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Registra o método para ser chamado toda vez que uma cena carregar
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Só busca os textos na cena do jogo, não na TelaFinal
        if (scene.name == "fase1")
        {
            textoPontos = GameObject.Find("ScoreText")?.GetComponent<TMP_Text>();
            textoVidas  = GameObject.Find("VidasText")?.GetComponent<TMP_Text>();
            inimigosRestantes = GameObject.FindGameObjectsWithTag("Invaders").Length;
            AtualizarUI();
            
            // Resetar o time scale ao carregar a fase
            Time.timeScale = 1f;
            isSlowMotionActive = false;
        }
    }

    void OnDestroy()
    {
        // Boa prática: desregistrar o evento ao destruir
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void InimigoDestruido()
    {
        pontos++;
        inimigosRestantes--;
        textoPontos.text = "Pontos: " + pontos;

        if (inimigosRestantes <= 0)
        {
            venceu = true;
            // Resetar time scale antes de mudar de cena
            Time.timeScale = 1f;
            SceneManager.LoadScene("TelaFinal");
        }
    }

    public void PlayerAtingido()
    {
        vidas--;
        textoVidas.text = "Vidas: " + vidas;

        if (vidas <= 0)
        {
            venceu = false;
            // Resetar time scale antes de mudar de cena
            Time.timeScale = 1f;
            SceneManager.LoadScene("TelaFinal");
        }
    }

    public void AtualizarUI()
    {
        if (textoPontos != null) textoPontos.text = "Pontos: " + pontos;
        if (textoVidas  != null) textoVidas.text  = "Vidas: "  + vidas;
    }
    
    public void ActivateSlowMotion()
    {
        if (!isSlowMotionActive)
        {
            StartCoroutine(SlowMotionCoroutine());
        }
    }
    
    private IEnumerator SlowMotionCoroutine()
    {
        isSlowMotionActive = true;
        
        // Ativar slow motion
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajustar física
        
        Debug.Log("Slow Motion Ativado!");
        
        // Aguardar a duração do efeito (em tempo real, não afetado pelo timeScale)
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        
        // Desativar slow motion
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        
        isSlowMotionActive = false;
        Debug.Log("Slow Motion Desativado!");
    }
}