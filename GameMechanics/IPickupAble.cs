namespace LB.GameMechanics
{
    public interface IPickupAble 
    {
        void Execute();
        Type ItemType();
        string GetName();
    }
    

    public enum Type
    {
        Quest,
        Item
    }
}