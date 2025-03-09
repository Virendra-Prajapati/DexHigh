using System.Collections.Generic;
using UnityEngine;

public class SkillView : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private ElementButton[] m_buttons;
    [SerializeField] private SkillButton m_skillButton;
    [SerializeField] private SkillWindow m_skillWindow;
    
    private readonly KeyCode[] m_keyCodes =
    { 
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 
    };

    #endregion

    #region Public Properties

    public ElementButton[] Buttons => m_buttons;
    public SkillButton SkillButton => m_skillButton;
    public SkillWindow SkillWindow => m_skillWindow;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        InitializeSkillButtons();
    }

    #endregion

    #region Public Methods

    public void UpdateSkillButtons(float progress, float radius, bool isWindowAnimating)
    {
        if (float.IsNaN(progress))
        {
            progress = 0;
        }
        
        foreach (var button in m_buttons)
        {
            button.UpdateButtonState(progress, radius, isWindowAnimating);
        }
    }

    public void UpdateMainButton(float progress)
    {
        m_skillButton.UpdateButtonState(progress);
    }

    public void UpdateSkillWindow(float progress)
    {
        m_skillWindow.UpdateWindowState(progress);
    }

    public void UpdateButtonSprites(IReadOnlyList<Skill> skills)
    {
        int count = Mathf.Min(m_buttons.Length, skills.Count);
        for (int i = 0; i < count; i++)
        {
            m_buttons[i].UpdateSkillIcon(skills[i].Data.InactiveIcon);
        }
    }

    #endregion

    #region Private Methods

    private void InitializeSkillButtons()
    {
        for (int i = 0; i < m_buttons.Length; i++)
        {
            if (i < m_keyCodes.Length)
            {
                m_buttons[i].Initialize(i, m_keyCodes[i], m_skillButton.ReferenceTransform);
            }
            else
            {
                m_buttons[i].gameObject.SetActive(false);
            }
        }
    }

    #endregion
}