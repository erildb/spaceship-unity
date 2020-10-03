using UnityEngine;

public class WireSocket : MonoBehaviour
{
    [SerializeField]
    private Plaque m_plaque;

    public Colors color;

    public Position position;

    [System.Serializable]
    public enum Colors
    {
        RED,
        BLUE,
        GREEN,
        YELLOW,
        PURPLE
    }

    [System.Serializable]
    public enum Position
    {
        TOP,
        BOTTOM,
    }

    private void Start()
    {
        var material = GetComponent<Renderer>().material;

        switch (color)
        {
            case Colors.RED:
                material.color = Color.red;
                break;
            case Colors.BLUE:
                material.color = Color.blue;
                break;
            case Colors.GREEN:
                material.color = Color.green;
                break;
            case Colors.YELLOW:
                material.color = Color.yellow;
                break;
            case Colors.PURPLE:
                material.color = Color.magenta;
                break;
        }
    }

    public void AddPair(BottomWireEdge socket)
    {
        m_plaque.AddPair(socket);
    }

    public void RemovePair(BottomWireEdge socket)
    {
        m_plaque.RemovePair(socket);
    }
}
