using hospital__management_system.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Enums;
public enum ResponseStatusCode
{
    [Description("Ok")]
    Ok = 200,

    [Description("Created")]
    Created = 201,

    [Description("Bad Request")]
    BadRequest = 400,

    [Description("Not Authorized")]
    NotAuthorized = 401,

    [Description("Forbidden")]
    Forbidden = 403,

    [Description("Not Found")]
    NotFound = 404,

    [Description("In Valid Model")]
    InValidModel = 410,
    
    [Description("Un Successfull Authenticarion")]
    UnSuccessfullAuthenticarion = 411,
    
    [Description("In Correct Password")]
    InCorrectPassword = 412,

    [Description("Server Error")]
    ServerError = 500,
}
