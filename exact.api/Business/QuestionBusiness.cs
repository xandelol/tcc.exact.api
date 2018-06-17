using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using exact.api.Business;
using exact.api.Data.Model;
using exact.api.Exception;
using exact.api.Model.Payload;
using exact.api.Model.Proxy;
using exact.api.Repository;
using exact.api.Storage;
using exact.common.Model.Payload;

namespace exact.business.Business
{
    public class QuestionBusiness : BaseBusiness<QuestionEntity>
    {
        private readonly IMapper _mapper;
        private readonly QuestionRepository _questionRepository;
        private readonly SettingRepository _settingRepository;
        private readonly UserRepository _userRepository;

        public QuestionBusiness(QuestionRepository questionRepository, IMapper mapper, SettingRepository settingRepository, UserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _settingRepository = settingRepository;
            _userRepository = userRepository;
        }


        public async Task Create(QuestionPayload payload)
        {
            var question = new QuestionEntity()
            {
                Id = Guid.NewGuid(),
                Statement = payload.Statement
            };

            await _questionRepository.AddAndSaveAsync(question);
        }

        public async Task Update(QuestionPayload payload)
        {
            var questionEntity = await _questionRepository.FirstOrDefaultAsync(f => f.Id == payload.Id);

            questionEntity.Statement = payload.Statement;
            questionEntity.IsActive = payload.IsActive;

          

            await _questionRepository.UpdateAndSaveAsync(questionEntity);
        }

        public async Task<QuestionProxy> Get(Guid id)
        {
            var questionEntity = await _questionRepository.FirstOrDefaultAsync(f => f.Id == id);

            if (questionEntity == null)
                throw new InvalidArgumentException(nameof(questionEntity), "Questão não encontrada!");

            return new QuestionProxy()
            {
                Id = questionEntity.Id,
                Statement = questionEntity.Statement,
                IsActive = questionEntity.IsActive,
            };
        }

        public DataTableProxy<QuestionProxy> Get(DataTablePayload payload)
        {
            var list = _questionRepository.GetAll();

            var recordsTotal = list.Count();

            //Sorting  
            if (!(string.IsNullOrEmpty(payload.SortColumn)))
            {
                switch (payload.SortColumn)
                {
                    case "Statement":
                        list = payload.IsAsc() ? list.OrderBy(s => s.Statement) : list.OrderByDescending(s => s.Statement);
                        break;

                    case "IsActive":
                        list = payload.IsAsc() ? list.OrderBy(s => s.IsActive) : list.OrderByDescending(s => s.IsActive);
                        break;
                }
            }

            //Search  
            if (!string.IsNullOrEmpty(payload.SearchValue))
            {
                list = list.Where(m => m.Statement.Contains(payload.SearchValue));
            }

            //total number of rows counts   
            var recordsFiltred = list.Count();

            //Paging   
            var data = list.Skip(payload.Skip).Take(payload.PageSize).Select(s => new QuestionProxy()
            {
                Id = s.Id,
                Statement = s.Statement,
                IsActive = s.IsActive
            }).ToList();

            //Returning Json Data  
            return new DataTableProxy<QuestionProxy>()
            {
                Draw = payload.Draw,
                RecordsFiltered = recordsFiltred,
                RecordsTotal = recordsTotal,
                Data = data
            };
        }

        public async Task<List<QuestionProxy>> Get() {
            var list = _questionRepository.GetAll();
            return list.Select(s => new QuestionProxy() {
                Id = s.Id,
                Statement = s.Statement,
                IsActive = s.IsActive
            }).ToList();
        }
    }
}