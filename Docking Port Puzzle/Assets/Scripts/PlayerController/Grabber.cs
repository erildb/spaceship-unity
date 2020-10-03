using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField]
    private float rayLength = 4f;

    private GameObject m_grabbableObject;
    private GameObject m_grabbedObject;

    private bool m_hasGrabbed;
    private RaycastHit hit;
    private Vector3 m_rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    private IUsable m_toolObject;

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(m_rayOrigin);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            m_grabbableObject = hit.collider.gameObject;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!m_hasGrabbed)
            {
                if (m_grabbableObject?.GetComponent<Tool>())
                {
                    GrabObject(m_grabbableObject);
                }
            }
            else
            {
                if (m_grabbableObject)
                {
                    ReleaseObject();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_hasGrabbed)
            {
                m_toolObject?.PerformAction(m_grabbableObject);
            }

            if (m_grabbableObject?.GetComponent<MonoBehaviour>() is IOpenable)
            {
                m_grabbableObject?.GetComponent<IOpenable>().Open();
            }
        }
    }

    private void GrabObject(GameObject grabbableObject)
    {
        m_hasGrabbed = true;
        m_grabbedObject = grabbableObject;
        m_grabbedObject.GetComponent<Tool>().GetGrabbed(transform);
        m_toolObject = m_grabbedObject.GetComponent<IUsable>();
    }

    private void ReleaseObject()
    {
        m_hasGrabbed = false;
        m_toolObject = null;
        m_grabbedObject.GetComponent<Tool>().GetReleased();
        m_grabbedObject = null;
    }

    public void ObjectReleased()
    {
        m_hasGrabbed = false;
        m_toolObject = null;
        m_grabbedObject = null;
    }
}
