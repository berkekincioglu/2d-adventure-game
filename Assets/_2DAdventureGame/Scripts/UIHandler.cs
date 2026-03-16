using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement m_HealthBar;

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
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        m_HealthBar.style.width = Length.Percent(100 * healthPercentage);
    }
}
