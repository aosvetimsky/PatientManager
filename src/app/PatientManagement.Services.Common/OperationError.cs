namespace PatientManagement.Services.Common
{
    public class OperationError
    {
        public virtual string Message { get; }
        public OperationError(string message = null)
        {
            Message = message;
        }
    }
}
