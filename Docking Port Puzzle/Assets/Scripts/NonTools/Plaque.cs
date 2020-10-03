using UnityEngine;

public class Plaque : MonoBehaviour
{
    [SerializeField]
    private GameState m_gameState;

    public void AddPair(BottomWireEdge socket)
    {
        if (CheckValidPair(socket))
            m_gameState.AddWire();
    }

    public void RemovePair(BottomWireEdge socket)
    {
        if (CheckValidPair(socket))
            m_gameState.RemoveWire();
    }

    public bool CheckValidPair(BottomWireEdge socket)
    {
        foreach (var combination in m_gameState.m_combinations)
        {
            if (combination.topColor == socket.topEdge.socketColor)
            {
                if (combination.bottomColor == socket.socketColor)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SetOpenedColor()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}
