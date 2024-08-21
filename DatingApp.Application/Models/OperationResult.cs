using DatingApp.Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Application.UserProfiles.Helper;

namespace DatingApp.Application.Models
{
    public class OperationResult<T>
    {
        public T PayLoad { get; set; }
        public bool IsError { get; private set; }
        public List<Error> Errors { get; } = new List<Error>();

        public void AddError(ErrorCode code ,string message)
        {
            HandleError(code,message);
        }
        
        public void AddUnknownError(string message)
        {
            HandleError(ErrorCode.UnknownError,message);
        }

        private void ResetIsError()
        {
            IsError = false;
        }

        private void HandleError(ErrorCode code, string message) 
        {
            Errors.Add(new Error()
            {
                Code = code,
                Message = message
            });
            IsError = true;
        }
    }
}
