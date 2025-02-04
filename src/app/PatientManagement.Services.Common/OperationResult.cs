namespace PatientManagement.Services.Common
{
    public class OperationResult
    {
        private OperationError? _error;

        public OperationError? Error => _error;
        public bool IsSucceeded => _error == null;

        public bool IsErrorOf<OperationError>(OperationError error) => _error != null && nameof(_error) == nameof(error);

        private OperationResult(OperationError? error)
        {
            _error = error;
        }

        private OperationResult()
        {
        }

        public static OperationResult Failed(OperationError error) => new OperationResult(error);
        public static OperationResult Succeeded() => new OperationResult();
    }

    public class OperationResult<TResult>
    {
        private OperationError? _error;
        private TResult? _result;

        public OperationError? Error => _error;
        public TResult? Result => _result;

        public bool IsSucceeded => _error == null;

        private OperationResult(OperationError? error)
        {
            _error = error;
        }

        private OperationResult(TResult? result)
        {
            _result = result;
        }

        public static OperationResult<TResult> Failed(OperationError error) => new OperationResult<TResult>(error);
        public static OperationResult<TResult> Succeeded(TResult result) => new OperationResult<TResult>(result);
    }
}
