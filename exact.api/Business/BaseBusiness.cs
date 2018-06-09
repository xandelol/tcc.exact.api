using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using exact.api.Data.Model;
using exact.api.Exception;
using exact.api.Repository;

namespace exact.api.Business
{
    public class BaseBusiness<T> where T : class 
    {
        private readonly IDataRepository<T> _baseRepository;
        private readonly UserRepository _userRepository;
        
        public BaseBusiness(IDataRepository<T>  baseRepository, UserRepository userRepository)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
        }
        
        public BaseBusiness(IDataRepository<T>  baseRepository)
        {
            _baseRepository = baseRepository;
        }
        
        public BaseBusiness()
        {
        }

        public Task Create(T item)
        {
            return _baseRepository.Create(item);
        }
        
        public UserEntity GetUserByUsername(string username)
        {
            if (_userRepository == null)
                throw new NullReferenceException("Ocorreu um erro, por favor, verifique com os responsáveis!");
            
            var user = _userRepository.Where(w => w.Email == username).FirstOrDefault();

            if (user == null)
                throw new TokenInvalidException("Email não encontrado!");
            return user;
        }
        
        public UserEntity GetUserByToken(string authorization)
        {
            if (_userRepository == null)
                throw new NullReferenceException("UserRepository is not inicialized.");
            
            var user = _userRepository.Where(w => w.Token == authorization).FirstOrDefault();

            if (user == null)
                throw new TokenInvalidException($"Token inválido ou expirado! ");
            return user;
        }
        
        public UserEntity GetUserById(Guid id)
        {
            if (_userRepository == null)
                throw new NullReferenceException("UserRepository is not inicialized.");
            
            var user = _userRepository.Where(w => w.Id == id).FirstOrDefault();

            if (user == null)
                throw new TokenInvalidException($"Usuário não encontrado!");
            return user;
        }

        protected UserEntity GetUserByEmail(string email)
        {
            if (_userRepository == null)
                throw new NullReferenceException("UserRepository is not inicialized.");
            
            var user = _userRepository.Where(w => w.Email == email).FirstOrDefault();

            if (user == null)
                throw new TokenInvalidException($"Email não encontrado! ");
            return user;
        }

        public async Task<int> Update(T item)
        {
            return await _baseRepository.UpdateAndSaveAsync(item);
        }

        public void Delete(T item)
        {
            _baseRepository.Remove(item);
        }
        
        protected OrderTo Map<Order, OrderTo>(Order item)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderTo>());
            var mapper = config.CreateMapper();
            return mapper.Map<OrderTo>(item);
        }
    }
}