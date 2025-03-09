using UnityEngine;

public class SkillWindow : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private CanvasGroup m_canvasGroup;
    
    private float m_initialFade;
    private float m_targetFade;

    #endregion

    #region Public Methods

    public void SetInitialAmount(float amount)
    {
        SetFade(amount);
    }

    public void UpdateTargetAmount(float amount)
    {
        m_initialFade = m_canvasGroup.alpha;
        m_targetFade = amount;
    }

    public void UpdateWindowState(float progress)
    {
        SetFade(Mathf.Lerp(m_initialFade, m_targetFade, progress));
    }

    #endregion

    #region Private Methods

    private void SetFade(float amount)
    {
        m_canvasGroup.alpha = amount;
    }

    #endregion
}