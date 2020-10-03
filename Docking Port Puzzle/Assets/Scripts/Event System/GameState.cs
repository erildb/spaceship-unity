using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameState")]
public class GameState : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private GameEvent m_screwsRemoved;

    [SerializeField]
    private GameEvent m_wiresPlugged;

    [SerializeField]
    private int m_csrewCount = 0;

    [SerializeField]
    private int m_correctWires = 0;

    [System.Serializable]
    public struct ColorPairs
    {
        public WireSocket.Colors topColor;
        public WireSocket.Colors bottomColor;
    }

    [SerializeField]

    public List<ColorPairs> m_combinations;

    public void AddScrew()
    {
        m_csrewCount++;

        //4 screws for the first task
        if (m_csrewCount == 4)
        {
            m_csrewCount = 0;
            m_screwsRemoved?.Raise();
        }
    }

    public void AddWire()
    {
        m_correctWires++;

        //5 wires for the first task
        if (m_correctWires == 5)
        {
            m_wiresPlugged?.Raise();
            m_correctWires = 0;
        }
    }

    public void RemoveWire()
    {
        m_correctWires--;
        m_correctWires = Mathf.Clamp(m_correctWires, 0, 5);
    }

    public void OnAfterDeserialize()
    {
        m_csrewCount = 0;
        m_correctWires = 0;
    }

    public void OnBeforeSerialize()
    {

    }
}
