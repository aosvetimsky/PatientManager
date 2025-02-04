using System;

namespace PatientManagement.Persistence.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public override string Message => $"Entity {typeof(T).Name} with Id {Id} not found";
        public object Id { get; private set; }

        public EntityNotFoundException(object id)
        {
            Id = id;
        }
    }
}
