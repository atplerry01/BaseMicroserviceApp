using API.Data.CodeCamp.Entities;
using API.Data.CodeCamp.Repositories;
using API.Data.CodeCamp.Services;
using API.Models.CodeCamp;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace API.Services.CodeCamp
{
    public class TalkService : ITalkService
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public TalkService(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(int statusCode, string message, TalkModel[] talkModel)> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
        {
            try
            {
                var talks = await _repository.GetTalksByMonikerAsync(moniker, includeSpeakers);

                return (200, null, _mapper.Map<TalkModel[]>(talks));
            }
            catch (Exception)
            {
                return (500, "Failed to get Talks", null);
            }
        }

        public async Task<(int statusCode, string message, TalkModel talkModel)> GetTalkByMonikerAsync(string moniker, int id)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null)
                {
                    return (404, $"Couldn't find a talk with id of {id}", null);
                }

                return  (200, null, _mapper.Map<TalkModel>(talk));
            }
            catch (Exception)
            {
                return (500, "Failed to get Talk", null);
            }
        }


        public async Task<(int statusCode, string message, TalkModel talkModel)> CreateTalk(string moniker, TalkModel model)
        {
            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null)
                {
                    return (400, "Camp does not exist", null);
                }

                var talk = _mapper.Map<Talk>(model);
                talk.Camp = camp;

                if (model.Speaker == null)
                {
                    return (400, "Speaker ID is required", null);
                }

                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);

                if (speaker == null)
                {
                    return (400, "Speaker could not be found", null);
                }

                talk.Speaker = speaker;

                _repository.Add(talk);

                if (await _repository.SaveChangesAsync())
                {
                    return (200, null, _mapper.Map<TalkModel>(talk));
                }
                else
                {
                    return (400, "Failed to save new Talk", null);
                }
            }
            catch (Exception)
            {
                return (500, "Failed to get Talks", null);
            }
        }

        public async Task<(int statusCode, string message, TalkModel talkModel)> UpdateTalk(string moniker, int id, TalkModel model)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);

                if (talk == null)
                {
                    return (404, "Couldn't find the talk", null);
                }

                _mapper.Map(model, talk);

                if (model.Speaker != null)
                {
                    var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if (speaker != null)
                    {
                        talk.Speaker = speaker;
                    }
                }

                if (await _repository.SaveChangesAsync())
                {
                    return (200, null, _mapper.Map<TalkModel>(talk));
                }
                else
                {
                    return (400, "Failed to update database.", null);
                }
            }
            catch (Exception)
            {
                return (500, "Failed to get Talks", null);
            }
        }


        public async Task<(int statusCode, string message)> RemoveTalk(string moniker, int id)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id);

                if (talk == null)
                {
                    return (404, "Failed to find the talk to delete");
                }

                _repository.Delete(talk);

                if (await _repository.SaveChangesAsync())
                {
                    return (200, null);
                }
                else
                {
                    return (400, "Failed to delete talk");
                }
            }
            catch (Exception)
            {
                return (500, "Failed to get Talks");
            }
        }

     
    }
}
