using PatientManagement.Services.Common;

namespace PatientManagement.Services.Application.Patient
{
    public static class PatientErrors
    {
        public static readonly OperationError PatientNotFoundError = new("Patient is not found");
    }
}
