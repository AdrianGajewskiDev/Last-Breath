using UnityEngine;
using System.Collections.Generic;
using LB.UI;

namespace LB.Quests
{
    public class Quest : MonoBehaviour
    {
        public string Name;
        public Transform startPoint;
        public Transform endPoint;

        public List<QuestGoal> QuestGoals;
        [HideInInspector] public QuestGoal currentQuestGoal;
        public bool QuestGoalFinished(QuestGoal goal)
        {
            return goal.Finished();
        }

        int currentQuestGoalIndex = 0;

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

                if (QuestGoals.Count > 1)
                {
                    currentQuestGoalIndex += 1;
                    currentQuestGoal = QuestGoals[currentQuestGoalIndex];
                }
            }


            Debug.Log(PlayerQuestsManager.Singleton.availableQuests.IndexOf(this));

            if (QuestFinished())
            {
                StartCoroutine( UIManager.Singleton.ShowQuestEndScreen(Name));
                PlayerQuestsManager.Singleton.availableQuests.RemoveAt(PlayerQuestsManager.Singleton.availableQuests.IndexOf(this));
                Destroy(this.transform.parent.gameObject, 2f);
            }

        }

        bool QuestFinished()
        {
            int num = 0;

            foreach (var q in QuestGoals)
            {
                if (q.Finished())
                    num += 1;
            }

            return num == QuestGoals.Count;
        }
    }

}
