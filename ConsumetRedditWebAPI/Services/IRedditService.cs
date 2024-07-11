using ConsumeRedditWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ConsumeRedditWebAPI.Services
{
    public interface IRedditService
    {
        Task<string> GetMostUpVotes(string accessToken);

    }
}
