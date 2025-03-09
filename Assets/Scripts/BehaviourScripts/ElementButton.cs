using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElementButton : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private Image m_skillIcon;
    [SerializeField] private Button m_skillButton;

    private int m_skillIndex;
    private KeyCode m_skillKey;

    private float m_targetRotationAngle;
    private float m_initialRotationAngle, m_finalRotationAngle;
    private float m_targetScale;
    private float m_initialScale, m_finalScale;

    private event UnityAction<int> m_onSkillButtonPressed = delegate { };

    private RectTransform m_referenceTransform;
    private RectTransform m_skillRectTransform;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        m_skillRectTransform = GetComponent<RectTransform>();
        m_skillButton.onClick.AddListener(() => m_onSkillButtonPressed(m_skillIndex));
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_skillKey))
        {
            m_onSkillButtonPressed(m_skillIndex);
        }
    }

    #endregion

    #region Public Methods

    public void Initialize(int index, KeyCode key, RectTransform reference)
    {
        m_skillIndex = index;
        m_skillKey = key;
        m_referenceTransform = reference;
    }

    public void RegisterListener(UnityAction<int> listener)
    {
        m_onSkillButtonPressed += listener;
    }

    public void SetInitialTransform(float rotationAngle, float scale, float radius)
    {
        m_targetRotationAngle = rotationAngle;
        SetPositionFromRotation(rotationAngle, radius);
        m_targetScale = scale;
        SetScale(scale);
    }

    public void UpdateTargetValues(float rotationAngle, float scale)
    {
        CalculateRotation(m_targetRotationAngle, rotationAngle);
        CalculateScale(m_targetScale, scale);
        m_targetRotationAngle = rotationAngle;
        m_targetScale = scale;
    }

    public void UpdateSkillIcon(Sprite icon)
    {
        m_skillIcon.sprite = icon;
    }

    public void UpdateButtonState(float progress, float radius, bool isAnimating)
    {
        float angle = isAnimating
            ? Mathf.LerpAngle(m_initialRotationAngle, m_finalRotationAngle, progress)
            : Mathf.Lerp(m_initialRotationAngle, m_finalRotationAngle, progress);
        
        SetPositionFromRotation(angle, radius);

        float scale = Mathf.Lerp(m_initialScale, m_finalScale, progress);
        SetScale(scale);
    }

    #endregion

    #region Private Helper Methods

    private void CalculateScale(float startScale, float endScale)
    {
        m_initialScale = startScale;
        m_finalScale = endScale;
    }

    private void CalculateRotation(float startAngle, float endAngle)
    {
        m_initialRotationAngle = startAngle;
        m_finalRotationAngle = endAngle;

        float angleDifference = m_initialRotationAngle - m_finalRotationAngle;
        if (Mathf.Abs(angleDifference) > 90)
        {
            m_finalRotationAngle += (angleDifference > 0) ? 360 : -360;
        }
    }

    private void SetPositionFromRotation(float angle, float radius)
    {
        float radians = angle * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * 2;
        m_skillRectTransform.anchoredPosition = m_referenceTransform.anchoredPosition + offset * radius;
    }

    private void SetScale(float scale)
    {
        m_skillRectTransform.localScale = Vector3.one * scale;
    }

    #endregion
}
