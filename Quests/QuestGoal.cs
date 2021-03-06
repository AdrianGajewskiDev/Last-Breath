﻿using LB.GameMechanics;
using LB.Health;
using LB.Player.Inventory;
using UnityEngine;

namespace LB.Quests
{
    [System.Serializable]
    public class QuestGoal : IQuestGoal
    {
        public string Description;

        /// <summary>
        /// Quest targets
        /// </summary>
        public Transform CheckPoint;
        public QuestItem itemToPickUp;
        public ZombieHealth targetToKill;

        public QuestGoalType QuestGoalType;

        public bool Finished()
        {

            bool result = false;

            switch (QuestGoalType)
            {
                case QuestGoalType.GoToPoint:
                    {
                        if (DestinationReached(CheckPoint))
                            result = true;
                    }break;
                case QuestGoalType.PickUpItem:
                    {
                        if (PickedUpItem(itemToPickUp))
                            result = true;
                    }break;
                case QuestGoalType.Kill:
                    {
                        if (TargetKilled())
                            result = true;
                    }break;
                default:
                    break;
            }

            return result;
        }


        public void OnFinish()
        {
            CustomBehaviourOnFinish?.Invoke();

            if (this.CheckPoint != null)
                this.CheckPoint.gameObject.SetActive(false);

        }

        bool DestinationReached(Transform d)
        {
            return Vector3.Distance(d.position, GameManager.Singleton.localPlayer.transform.position) <= 2f;
        }
        bool PickedUpItem(IInventoryItem item) => PlayerInventoryManager.Singleton.questItems.Contains(item);
        bool TargetKilled() => targetToKill.IsDead();

        public event System.Action CustomBehaviourOnFinish;
    }

}
