using UnityEngine;
using System.Collections.Generic;
using LB.UI;
using LB.GameMechanics;
using LB.Player;

namespace LB.Quests
{
    public class Quest : MonoBehaviour
    {
        public string Name;
        public float EXP;
        public Transform startPoint;
        public Transform endPoint;

        public List<QuestGoal> QuestGoals;
        [HideInInspector] public QuestGoal currentQuestGoal;

        
        public bool QuestGoalFinished(QuestGoal goal)
        {
            return goal.Finished();
        }

        public void Init()
        {
            this.enabled = true;
            PlayerQuestsManager.Singleton.availableQuests.Add(this);
        }

        private void Update()
        {
            if (QuestGoalFinished(currentQuestGoal))
            {
                currentQuestGoal.OnFinish();
                QuestGoals.RemoveAt(QuestGoals.IndexOf(currentQuestGoal));
            }

            if (QuestGoals.Count != 0)
                currentQuestGoal = QuestGoals[0];

            if (QuestFinished())
            {
                PlayerStats.Singleton.AddEXP(EXP);
                StartCoroutine( UIManager.Singleton.ShowQuestEndScreen(Name));
                PlayerQuestsManager.Singleton.availableQuests.RemoveAt(PlayerQuestsManager.Singleton.availableQuests.IndexOf(this));
                Destroy(this.transform.parent.gameObject);
            }
        }


        bool QuestFinished()
        {
            return QuestGoals.Count == 0;
        }

    }

}
