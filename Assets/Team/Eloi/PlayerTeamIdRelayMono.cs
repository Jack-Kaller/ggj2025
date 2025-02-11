﻿using UnityEngine;
using UnityEngine.Events;

public class PlayerTeamIdRelayMono: MonoBehaviour
{
    public int m_teamId;
    public UnityEvent<int> m_onTeamIdChanged;

    public int GetTeamId()
    {
        return m_teamId;
    }

    public void SetTeamId(int teamId)
    {
        m_teamId = teamId;
        m_onTeamIdChanged.Invoke(m_teamId);
    }
}
