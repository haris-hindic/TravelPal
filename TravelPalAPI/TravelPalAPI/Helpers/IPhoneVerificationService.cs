using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Verify.V2.Service;

namespace TravelPalAPI.Helpers
{
    public interface IPhoneVerificationService
    {
        Task<VerificationResult> StartVerificationAsync(string phoneNumber);

        Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code);
    }

    public class VerificationResult
    {
        public VerificationResult(string sid)
        {
            Sid = sid;
            IsValid = true;
        }

        public VerificationResult(List<string> errors)
        {
            Errors = errors;
            IsValid = false;
        }

        public bool IsValid { get; set; }

        public string Sid { get; set; }

        public List<string> Errors { get; set; }
    }
}
