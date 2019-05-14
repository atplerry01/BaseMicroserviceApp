using API.Data.CodeCamp.Entities;
using API.Data.CodeCamp.Repositories;
using API.Data.CodeCamp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.CodeCamp
{
    public class CampService : ICampService
    {
        private readonly ICampRepository _repository;
        public CampService(ICampRepository repository)
        {
            _repository = repository;
        }

        public Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
        {
            throw new NotImplementedException();
        }

        public Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker[]> GetAllSpeakersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Camp> GetCampAsync(string moniker, bool includeTalks = false)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker> GetSpeakerAsync(int speakerId)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker)
        {
            throw new NotImplementedException();
        }

        public Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        public Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }
    }
}
