using System.Collections.Generic;
using UnityEngine.Events;

public class SkillModel
{
    private readonly List<Skill> m_skills;
    private UnityAction<IReadOnlyList<Skill>> m_onNewSkillAdd;
    public BehaviourData Data { get; }
    
    public SkillModel(BehaviourData data)
    {
        Data = data;
        m_skills = new List<Skill>();
    }
    
    public void AddSkill(Skill skill)
    {
        m_skills.Add(skill);
        m_onNewSkillAdd?.Invoke(m_skills);
    }
    
    public void RegisterListener(UnityAction<IReadOnlyList<Skill>> listener)
    {
        m_onNewSkillAdd += listener;
    }
    
    public IReadOnlyList<Skill> Skills => m_skills;
}

public class Skill
{
    public ElementData Data { get; }
    
    public Skill(ElementData data)
    {
        Data = data;
    }
}