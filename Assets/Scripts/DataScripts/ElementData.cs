using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ElementData", fileName = "ElementData")]
public class ElementData : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private string m_elementId;
    [SerializeField] private Sprite m_activeIcon;
    [SerializeField] private Sprite m_inactiveIcon;

    #endregion

    #region Public Properties

    public string ID => m_elementId;
    public Sprite ActiveIcon => m_activeIcon;
    public Sprite InactiveIcon => m_inactiveIcon;

    #endregion
}