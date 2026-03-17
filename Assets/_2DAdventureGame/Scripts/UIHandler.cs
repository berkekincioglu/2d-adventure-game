using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement m_HealthBar;

    public float displayTime = 4f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HealthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        UpdateHealthBar(1f);

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1f;
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

    public void DisplayDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * healthPercentage);
    }
}
