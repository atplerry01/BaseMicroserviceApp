using API.Data.CodeCamp.Entities;
using API.Models.CodeCamp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.CodeCamp.Services
{
    public interface ITalkService
    {

        // Talks
        Task<(int statusCode, string message, TalkModel[] talkModel)> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false);
        Task<(int statusCode, string message, TalkModel talkModel)> GetTalkByMonikerAsync(string moniker, int id);
        Task<(int statusCode, string message, TalkModel talkModel)> CreateTalk(string moniker, TalkModel model);
        Task<(int statusCode, string message, TalkModel talkModel)> UpdateTalk(string moniker, int id, TalkModel model);
        Task<(int statusCode, string message)> RemoveTalk(string moniker, int id);

    }
}
