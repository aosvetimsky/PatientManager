using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Services.Common;

namespace PatientManager.Services.Api.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IResult HandleOperationInvalidResult(OperationError error) => Results.BadRequest(error.Message);
    }
}
