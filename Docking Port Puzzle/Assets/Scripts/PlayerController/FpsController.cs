using UnityEngine;

public class FpsController : MonoBehaviour
{
    [SerializeField]
    public float m_sensitivity = 3.0f;
    [SerializeField]
    public float m_smoothing = 2.0f;

    [SerializeField]
    private GameObject character;

    private Vector2 m_mouseLook;
    private Vector2 m_smoothV;

    void Update()
    {
        var mouseMove = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseMove = Vector2.Scale(mouseMove, new Vector2(m_sensitivity * m_smoothing, m_sensitivity * m_smoothing));

        m_smoothV.x = Mathf.Lerp(m_smoothV.x, mouseMove.x, 1f / m_smoothing);
        m_smoothV.y = Mathf.Lerp(m_smoothV.y, mouseMove.y, 1f / m_smoothing);

        m_mouseLook += m_smoothV;
        m_mouseLook.y = Mathf.Clamp(m_mouseLook.y, -70, 50);

        transform.localRotation = Quaternion.AngleAxis(-m_mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(m_mouseLook.x, character.transform.up);
    }
}