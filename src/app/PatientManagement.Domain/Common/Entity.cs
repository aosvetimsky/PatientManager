namespace PatientManagement.Domain.Common
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public class Entity
    {
    }

    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public TKey Id { get; set; }

        protected Entity(TKey id)
        {
            Id = id;
        }

        protected Entity()
        {
            Id = default!;
        }
    }
}
