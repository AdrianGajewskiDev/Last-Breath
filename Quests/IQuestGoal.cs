
namespace LB.Quests
{
    public interface IQuestGoal
    {
        bool Finished();
        void OnFinish();
    }
}
