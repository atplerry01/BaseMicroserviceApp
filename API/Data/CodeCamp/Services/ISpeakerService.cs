using API.Data.CodeCamp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.CodeCamp.Services
{
   public interface ISpeakerService
    {
        // Speakers
        Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker);
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<Speaker[]> GetAllSpeakersAsync();
    }
}
