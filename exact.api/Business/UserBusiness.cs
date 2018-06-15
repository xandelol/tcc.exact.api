using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using exact.api.Business;
using exact.api.Data.Enum;
using exact.api.Data.Model;
using exact.api.Exception;
using exact.api.Model.Payload;
using exact.api.Model.Proxy;
using exact.api.Repository;
using exact.api.Storage;
using lavasim.common.Extension;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace lavasim.business.Business
{
    public class UserBusiness : BaseBusiness<UserEntity>
    {
        private readonly UserRepository _repository;
        private readonly GroupRepository _groupRepository;
        private readonly SettingBusiness _settingBusiness;
        private readonly IStorageRepository _storeRepository;
        private readonly IConfiguration _configuration;
        private readonly GroupActionBusiness _groupActionBusiness;
        private readonly QuestionRepository _questionRespository;

        public UserBusiness(UserRepository repository,
            UserRepository userRepository, 
            SettingBusiness settingBusiness, 
            IStorageRepository storeRepository,
            IConfiguration configuration, 
            GroupRepository groupRepository,
            GroupActionBusiness groupActionBusiness, 
            QuestionRepository questionRespository)
            : base(repository, userRepository)
        {
            _repository = repository;
            _settingBusiness = settingBusiness;
            _storeRepository = storeRepository;
            _configuration = configuration;
            _groupRepository = groupRepository;
            _groupActionBusiness = groupActionBusiness;
            _questionRespository = questionRespository;
        }

        public async Task<JwtTokenProxy> CreateUser(CreateUserPayload payload) {
            return null;
        }

        public async Task<UserInfoProxy> GetUserInfo(Guid id)
        {
            var user = GetUserById(id);

            var userProxy = Map<UserEntity, UserInfoProxy>(user);

            if (userProxy.Phone.IsNotNullOrEmpty())
                userProxy.Phone = userProxy.Phone.Replace("+", "");

            userProxy.Settings = _settingBusiness.GetClientSettings();

            if (user.Type == UserType.Backoffice)
            {
                userProxy.Actions = _groupActionBusiness.GetFromGroupId(user.GroupId);
            }

            return userProxy;
        }

        public async Task SetLoginDate(Guid id)
        {
            var user = GetUserById(id);

            user.LastLogin = DateTime.UtcNow;

            await _repository.UpdateAndSaveAsync(user);
        }

        public async Task SendEmailResetPassword(string email)
        {
            var user = GetUserByEmail(email);

            user.ResetPasswordCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            await _repository.UpdateAndSaveAsync(user);

            //TODO: enviar email para o usuário com código.
        }

        public async Task<JwtSecurityToken> ResetPassword(string email, string password, string code)
        {
            var user = GetUserByEmail(email);

            if (user.ResetPasswordCode != code.ToUpper())
            {
                throw new InvalidArgumentException(nameof(email),
                    $"Código inválido!");
            }

            user.Password = password.CalculateMd5Hash();
            user.ResetPasswordCode = "";

            await _repository.UpdateAndSaveAsync(user);

            return await GetJwtSecurityToken(email, password, user.Type);
        }

        /// <summary>
        ///     Get a token to the given user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="type">Client type</param>
        /// <returns></returns>
        public async Task<JwtSecurityToken> GetJwtSecurityToken(string username,
            string password, UserType type)
        {
            var user = _repository.Where(w => w.Email == username && w.Type == type).FirstOrDefault();

            if (user == null)
                throw new InvalidArgumentException("Usuário não encontrado!");

            if (string.IsNullOrEmpty(user.Password))
                throw new InvalidArgumentException("Por favor, informe a senha para efetuar o login!");

            if (user.Password != password.CalculateMd5Hash())
                throw new InvalidArgumentException("Senha inválida!");

            var jwtToken = new JwtSecurityToken(
                _configuration["SiteUrl"],
                _configuration["SiteUrl"],
                GetTokenClaims(user),
                expires: DateTime.UtcNow.AddMonths(6),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"])),
                    SecurityAlgorithms.HmacSha256)
            );


            user.Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(jwtToken)}";

            await _repository.UpdateAndSaveAsync(user);

            return jwtToken;
        }

        private static IEnumerable<Claim> GetTokenClaims(UserEntity user)
        {
            var values = new List<Claim> {new Claim(ClaimTypes.Name, user.Id.ToString())};

            values.AddRange(user.Roles.Split('|').Select(role => new Claim(ClaimTypes.Role, role)));

            return values;
        }

        public async Task ChangePassword(string authorization, string newPassword, string oldPassword)
        {
            var user = GetUserByToken(authorization);

            if (oldPassword.CalculateMd5Hash() != user.Password)
            {
                throw new InvalidArgumentException(nameof(oldPassword),
                    $"Senha antiga inválida!");
            }

            user.Password = newPassword.CalculateMd5Hash();

            await _repository.UpdateAndSaveAsync(user);
        }
    }
}