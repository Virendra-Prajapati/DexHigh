using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private RectTransform m_mainRectTransform;
    [SerializeField] private RectTransform m_iconRectTransform;
    [SerializeField] private Button m_skillButton;

    private Vector2 m_initialPosition, m_finalPosition;
    private float m_initialScale, m_finalScale;

    private event UnityAction m_onMainButtonPressed = delegate { };

    #endregion

    #region Public Properties

    public RectTransform ReferenceTransform => m_mainRectTransform;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        m_skillButton.onClick.AddListener(() => m_onMainButtonPressed());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_onMainButtonPressed();
        }
    }

    #endregion

    #region Public Methods

    public void SetInitialTransform(Vector2 position, float scale)
    {
        m_finalPosition = position;
        m_finalScale = scale;
        SetPosition(position);
        SetScale(scale);
    }

    public void RegisterListener(UnityAction listener)
    {
        m_onMainButtonPressed += listener;
    }

    public void UpdateTargetValues(Vector2 position, float scale)
    {
        m_initialPosition = m_mainRectTransform.anchoredPosition;
        m_finalPosition = position;

        m_initialScale = m_iconRectTransform.localScale.x;
        m_finalScale = scale;
    }

    public void UpdateButtonState(float progress)
    {
        SetPosition(Vector2.Lerp(m_initialPosition, m_finalPosition, progress));
        SetScale(Mathf.Lerp(m_initialScale, m_finalScale, progress));
    }

    #endregion

    #region Private Helper Methods

    private void SetPosition(Vector2 position)
    {
        m_mainRectTransform.anchoredPosition = position;
    }

    private void SetScale(float scale)
    {
        m_iconRectTransform.localScale = scale * Vector3.one;
    }

    #endregion
}
