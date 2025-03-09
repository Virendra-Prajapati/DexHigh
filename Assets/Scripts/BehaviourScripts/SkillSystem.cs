using System;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private SkillView m_skillView;
    [SerializeField] private ElementData[] m_startingElementDatas;
    [SerializeField] private BehaviourData m_behaviourData;

    private SkillController m_skillController;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        #if UNITY_ANDROID || UNITY_IOS
        Application.targetFrameRate = 60;
        #endif
    }

    private void Start()
    {
        InitializeSkillController();
    }

    private void Update()
    {
        m_skillController?.Update(Time.deltaTime);
    }

    #endregion

    #region Private Methods

    private void InitializeSkillController()
    {
        m_skillController = new SkillController.Builder()
            .WithData(m_startingElementDatas, m_behaviourData)
            .Build(m_skillView);
    }

    #endregion
}