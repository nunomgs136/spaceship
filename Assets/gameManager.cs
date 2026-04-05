using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public int pontos = 0;
    public int vidas = 5;
    public bool venceu = false;
    public int inimigosRestantes = 0;

    private TMP_Text textoPontos;
    private TMP_Text textoVidas;

    public float slowMotionScale = 0.3f;
    public float slowMotionDuration = 5f; 

    private float timer;
    private bool isSlowed = false;
    private float originalFixedDeltaTime;
    void Start()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (isSlowed)
        {
            timer -= Time.unscaledDeltaTime; 

            if (timer <= 0f)
            {
                RestoreNormalTime(); // Acabou o tempo, volta ao normal
            }
        }
    }
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
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = originalFixedDeltaTime * slowMotionScale;
        isSlowed = true;
        timer = slowMotionDuration;

    }

    public void RestoreNormalTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
        isSlowed = false;

    }
}