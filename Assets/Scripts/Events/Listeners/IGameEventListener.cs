namespace _Scripts.Events.Listeners
{
    public interface IGameEventListener<in T>
    {
        void OnEventRaised(T item);
    }
}
