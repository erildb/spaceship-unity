using UnityEngine;
using Utils;

public class Door : MonoBehaviour
{
    private TweenInfo m_openDoor;

    public void OpenDoor()
    {
        m_openDoor.Stop();
        m_openDoor = Tweener.Start(new FloatTweenSettings
        {
            duration = 2f,
            from = 0,
            to = 1,
            easing = Interpolations.Linear,
            onChanged = (amt) =>
            {
                transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0.33f, 0, 0), amt);
            }
            
        }, this);
    }
}
