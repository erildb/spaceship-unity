using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 10.0f;

    private float m_vertical;
    private float m_horizontal;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        m_vertical = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
        m_horizontal = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
        transform.Translate(m_horizontal, 0, m_vertical);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}