using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ConsumeRedditWebAPI.Services
{
    public interface IRedditAccountService
    {
        Task<string> GetToken(
            string userName,
            string userPassword,
            string clientId,
            string clientSecret,
            string userAgent
           );
        //Task<string> GetToken(string url,
        //   string username,
        //   string password
        //  );
    }
}
