using UnityEngine;

public class SkillController
{
    #region Private Fields

    private readonly SkillModel m_model;
    private readonly SkillView m_view;
    private float m_timer;
    private bool m_isTabOpened;
    private bool m_isWindowAnimating;
    private float m_currentRadius, m_startRadius;

    #endregion

    #region Constructor

    private SkillController(SkillModel model, SkillView view)
    {
        m_model = model;
        m_view = view;
        m_timer = model.Data.AnimateDuration;
        ConnectView();
        ConnectModel();
    }

    #endregion

    #region Public Methods

    public void Update(float deltaTime)
    {
        if (m_timer < m_model.Data.AnimateDuration)
        {
            m_timer += deltaTime;
            float progress = m_timer / m_model.Data.AnimateDuration;

            if (m_isWindowAnimating)
            {
                m_currentRadius = Mathf.Lerp(m_startRadius, GetButtonRadius(), progress);
                m_view.UpdateMainButton(progress);
                m_view.UpdateSkillWindow(progress);
            }

            m_view.UpdateSkillButtons(progress, m_currentRadius, m_isWindowAnimating);
        }
        else if (m_isWindowAnimating)
        {
            m_isWindowAnimating = false;
        }
    }

    #endregion

    #region Private Methods

    private void ConnectModel()
    {
        m_model.RegisterListener(m_view.UpdateButtonSprites);
    }
    private void ConnectView()
    {
        m_currentRadius = GetButtonRadius();
        m_view.SkillButton.RegisterListener(OnMainButtonPressed);
        m_view.SkillButton.SetInitialTransform(m_model.Data.MainButtonClosePosition, m_model.Data.MainButtonCloseScale);
        m_view.SkillWindow.SetInitialAmount(m_model.Data.FadeOut);

        for (int i = 0; i < m_view.Buttons.Length; i++)
        {
            m_view.Buttons[i].RegisterListener(OnSkillButtonPressed);
            m_view.Buttons[i].SetInitialTransform(CalculateButtonAngle(i, m_view.Buttons.Length), 
                                                  CalculateButtonScale(i, m_view.Buttons.Length), 
                                                  m_currentRadius);
            m_view.Buttons[i].gameObject.name = m_model.Skills[i].Data.ID;
        }

        m_view.UpdateButtonSprites(m_model.Skills);
    }

    private void OnSkillButtonPressed(int index)
    {
        if (!m_isTabOpened) return;

        int count = m_view.Buttons.Length;
        int startIndex = (index - Mathf.FloorToInt(count / 2f) + count) % count;

        for (int i = 0; i < count; i++)
        {
            int angle = CalculateButtonAngle(i, count);
            m_view.Buttons[startIndex].UpdateTargetValues(angle, CalculateButtonScale(i, count));
            startIndex = (startIndex + 1) % count;
        }

        ResetTimer();
    }

    private void OnMainButtonPressed()
    {
        m_isTabOpened = !m_isTabOpened;

        m_view.SkillButton.UpdateTargetValues(
            m_isTabOpened ? m_model.Data.MainButtonOpenPosition : m_model.Data.MainButtonClosePosition,
            m_isTabOpened ? m_model.Data.MainButtonOpenScale : m_model.Data.MainButtonCloseScale
        );

        m_view.SkillWindow.UpdateTargetAmount(m_isTabOpened ? m_model.Data.FadeIn : m_model.Data.FadeOut);

        int count = m_view.Buttons.Length;
        for (int i = 0; i < count; i++)
        {
            int angle = CalculateButtonAngle(i, count);
            m_view.Buttons[i].UpdateTargetValues(angle, CalculateButtonScale(i, count));
        }

        m_isWindowAnimating = true;
        m_startRadius = m_currentRadius;
        ResetTimer();
    }

    private int CalculateButtonAngle(int index, int count)
    {
        if (m_isTabOpened)
        {
            return m_model.Data.BaseAngle + (index * m_model.Data.StepAngle);
        }

        int stepAngle = 360 / count;
        return 70 + index * stepAngle;
    }

    private float CalculateButtonScale(int index, int count)
    {
        if (!m_isTabOpened)
        {
            return m_model.Data.MinScale;
        }

        float normalizedValue = Mathf.Abs((index - (count - 1) / 2f) / ((count - 1) / 2f));
        float scale = Mathf.Lerp(m_model.Data.MaxScale, m_model.Data.MinScale, normalizedValue);
        return m_model.Data.ButtonInitialScale * scale;
    }

    private void ResetTimer()
    {
        m_timer = 0f;
    }

    private float GetButtonRadius()
    {
        return m_isTabOpened ? m_model.Data.OpenRadius : m_model.Data.CloseRadius;
    }

    #endregion

    #region Builder Class

    public class Builder
    {
        private SkillModel m_model;

        public Builder WithData(ElementData[] datas, BehaviourData behaviourData)
        {
            m_model = new SkillModel(behaviourData);
            foreach (var elementData in datas)
            {
                m_model.AddSkill(new Skill(elementData));
            }
            return this;
        }

        public SkillController Build(SkillView view)
        {
            return new SkillController(m_model, view);
        }
    }

    #endregion
}
