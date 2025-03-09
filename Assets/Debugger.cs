using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private bool m_run;
    [SerializeField] private int m_childCount; 
    [SerializeField] private float m_radius;
    [SerializeField] private float m_startAngle;
    [SerializeField] private float m_stepAngle;
    private Vector3 position;

    private void Reset()
    {
        m_radius = 2;
        m_startAngle = 90;
        m_stepAngle = 45;
    }

    private void OnDrawGizmos()
    {
        if (!m_run)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_radius);
        Gizmos.color = Color.red;
        for (int i = 0; i < m_childCount; i++)
        {
            float angleRad = (m_startAngle + (i * m_stepAngle)) * Mathf.Deg2Rad;
            position.x = Mathf.Cos(angleRad) * m_radius;
            position.y = Mathf.Sin(angleRad) * m_radius;
            Gizmos.DrawLine(transform.position, transform.position + position);
        }
    }
}
