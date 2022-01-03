using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Verify.V2.Service;

namespace TravelPalAPI.Helpers
{
    public class PhoneVerificationService : IPhoneVerificationService
    {
        private readonly IConfiguration configuration;

        public PhoneVerificationService(IConfiguration configuration)
        {
            this.configuration = configuration;
            TwilioClient.Init(
                configuration.GetValue<string>("Twilio:AccountSid"),
                configuration.GetValue<string>("Twilio:AuthToken"));
        }

        public async Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code)
        {
            try
            {
                var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: code,
                    pathServiceSid: configuration.GetValue<string>("Twilio:VerificationSid")
                );
                return verificationCheckResource.Status.Equals("approved") ?
                    new VerificationResult(verificationCheckResource.Sid) :
                    new VerificationResult(new List<string> { "Wrong code. Try again." });
            }
            catch (TwilioException e)
            {
                return new VerificationResult(new List<string> { e.Message });
            }
        }

        public async Task<VerificationResult> StartVerificationAsync(string phoneNumber)
        {
            try
            {
                var verificationResource = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: "sms",
                    pathServiceSid: configuration.GetValue<string>("Twilio:VerificationSid")
                );
                return new VerificationResult(verificationResource.Sid);
            }
            catch (TwilioException e)
            {
                return new VerificationResult(new List<string> { e.Message });
            }
        }
    }
}
