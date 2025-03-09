using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BehaviourData", fileName = "BehaviourData")]
public class BehaviourData : ScriptableObject
{
    #region Serialized Fields

    [Header("Elements Buttons")]
    [SerializeField] private float m_animateDuration = 0.5f;
    [SerializeField] private int m_baseAngle = 90;
    [SerializeField] private int m_stepAngle = 45;
    [SerializeField] private float m_buttonsInitialScale = 1f;
    [SerializeField] private float m_minButtonScale = 1f;
    [SerializeField] private float m_maxButtonScale = 1.2f;
    [SerializeField] private float m_closeRadius = 130;
    [SerializeField] private float m_openRadius = 200;

    [Header("Skill Buttons")]
    [SerializeField] private Vector2 m_mainButtonCloseAnchorPosition;
    [SerializeField] private Vector2 m_mainButtonOpenAnchorPosition;
    [SerializeField] private float m_mainButtonCloseScale;
    [SerializeField] private float m_mainButtonOpenScale;

    [Header("Skill Window")] 
    [SerializeField] private float m_minFadeAmount = 0;
    [SerializeField] private float m_maxFadeAmount = 1;

    #endregion

    #region Public Properties

    public float AnimateDuration => m_animateDuration;
    public int BaseAngle => m_baseAngle;
    public int StepAngle => m_stepAngle;
    public float ButtonInitialScale => m_buttonsInitialScale;
    public float MinScale => m_minButtonScale;
    public float MaxScale => m_maxButtonScale;
    public float OpenRadius => m_openRadius;
    public float CloseRadius => m_closeRadius;

    public Vector2 MainButtonOpenPosition => m_mainButtonOpenAnchorPosition;
    public Vector2 MainButtonClosePosition => m_mainButtonCloseAnchorPosition;
    public float MainButtonOpenScale => m_mainButtonOpenScale;
    public float MainButtonCloseScale => m_mainButtonCloseScale;

    public float FadeOut => m_minFadeAmount;
    public float FadeIn => m_maxFadeAmount;

    #endregion
}