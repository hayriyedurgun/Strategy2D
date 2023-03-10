using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class SoldierProduct : BaseProduct
    {
        private int m_CurrentPathIndex;
        private List<TileBehaviour> m_Path;
        private TileBehaviour m_Target;

        public Animator Animator;
        public GamePlaySettings Settings => GameManager.Instance.GamePlaySettings;

        public override void Update()
        {
            base.Update();

            if (m_Path != null)
            {
                if (Vector2.Distance(transform.position, m_Target.transform.position) > 0.05)
                {
                    var direction = (m_Target.transform.position - transform.position).normalized;
                    transform.position += direction * Settings.MovementSpeed * Time.deltaTime;
                }
                else
                {
                    m_CurrentPathIndex++;
                    if (m_CurrentPathIndex >= m_Path.Count)
                    {
                        m_Target.Product = this;
                        m_Target.ClearStatus();
                        m_Path = null;
                        Animator.SetBool("IsWalking", false);
                    }
                    else
                    {
                        m_Target = m_Path[m_CurrentPathIndex];
                    }
                }
            }
        }

        public void SetTarget(List<TileBehaviour> path)
        {
            Animator.SetBool("IsWalking", true);

            m_CurrentPathIndex = 0;
            m_Path = path;
            m_Target = m_Path[m_CurrentPathIndex];
        }
    }
}
