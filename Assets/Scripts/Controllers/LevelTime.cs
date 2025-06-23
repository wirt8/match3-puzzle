using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : LevelCondition
{
    private float m_time;

    private GameManager m_mngr;

    private Coroutine m_timerCoroutine;

    public override void Setup(float value, Text txt, GameManager mngr)
    {
        base.Setup(value, txt, mngr);

        m_mngr = mngr;

        m_time = value;

        UpdateText();

        if (m_timerCoroutine != null)
            StopCoroutine(m_timerCoroutine);
        m_timerCoroutine = StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        while (!m_conditionCompleted)
        {
            if (m_mngr.State == GameManager.eStateGame.GAME_STARTED)
            {
                m_time -= 1f;
                
                UpdateText();
                
                if (m_time <= -1f)
                {
                    OnConditionComplete();
                    yield break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void UpdateText()
    {
        if (m_time < 0f) return;

        m_txt.text = string.Format("TIME:\n{0:00}", m_time);
    }
}
