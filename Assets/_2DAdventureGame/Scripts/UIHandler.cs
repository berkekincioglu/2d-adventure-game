using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement m_HealthBar;

    public float displayTime = 4f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;

    private VisualElement m_WinScreen;
    private VisualElement m_LoseScreen;

    private Label m_RobotCounter;

    private PlayerController player;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HealthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_WinScreen = uiDocument.rootVisualElement.Q<VisualElement>("WinScreenContainer");
        m_LoseScreen = uiDocument.rootVisualElement.Q<VisualElement>("LoseScreenContainer");

        m_RobotCounter = uiDocument.rootVisualElement.Q<Label>("CounterLabel");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealthBar(1f);
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1f;

        player = FindAnyObjectByType<PlayerController>();
        player.OnTalkedToNPC += DisplayDialogue;
        player.OnHealthChanged += UpdateHealthBar;
    }

    void Update()
    {
        if (m_TimerDisplay > 0f)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay <= 0f)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    void OnDestroy()
    {
        player.OnTalkedToNPC -= DisplayDialogue;
        player.OnHealthChanged -= UpdateHealthBar;

    }

    public void DisplayDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * healthPercentage);
    }

    public void DisplayWinScreen()
    {
        m_WinScreen.style.opacity = 1.0f;
    }

    public void DisplayLoseScreen()
    {
        m_LoseScreen.style.opacity = 1.0f;
    }

    public void SetCounter(int current, int enemies)
    {
        m_RobotCounter.text = $"{current} / {enemies}";
    }

}
