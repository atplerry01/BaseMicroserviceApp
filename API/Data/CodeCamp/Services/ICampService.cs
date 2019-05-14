using API.Data.CodeCamp.Entities;
using System;
using System.Threading.Tasks;

namespace API.Data.CodeCamp.Services
{
    public interface ICampService
    {
        // Camps
        Task<Camp[]> GetAllCampsAsync(bool includeTalks = false);
        Task<Camp> GetCampAsync(string moniker, bool includeTalks = false);
        Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);
    }
}
