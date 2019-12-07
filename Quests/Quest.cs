using UnityEngine;
using System.Collections.Generic;

namespace LB.Quests
{
    public class Quest : MonoBehaviour
    {
        public string Name;
        public Transform startPoint;
        public Transform endPoint;

        public List<QuestGoal> QuestGoals;
        [HideInInspector]public QuestGoal currentQuestGoal;
        public bool QuestGoalFinished(QuestGoal goal) => goal.Finished();

        int currentQuestGoalIndex = 0;

        private void Update()
        {
            if (QuestGoalFinished(currentQuestGoal))
            {
                currentQuestGoal.OnFinish();

                if(QuestGoals.Count > 1)
                {
                    currentQuestGoalIndex += 1;
                    currentQuestGoal = QuestGoals[currentQuestGoalIndex];
                }
            }

            Debug.Log(currentQuestGoal.Description);
        }
    }

}
