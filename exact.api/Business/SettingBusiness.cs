using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exact.api.Business;
using exact.api.Data.Model;
using exact.api.Model.Proxy;
using exact.api.Repository;
using exact.common.Extension;
using exact.common.Model.Payload;
using exact.data.Enum;

namespace exact.business.Business
{
    public class SettingBusiness: BaseBusiness<SettingEntity>
    {
        private readonly SettingRepository _repository;
        
        public SettingBusiness(SettingRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> GetInt(string key)
        {
            return Convert.ToInt32((await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == null && f.Type == SettingType.Int && f.Key.Equals(key))).Value);
        }
        
        public async Task<string> GetString(string key)
        {
            return (await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == null && f.Type == SettingType.String && f.Key.Equals(key))).Value;
        }
        
        public async Task<double> GetDouble(string key)
        {
            var result = await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == null && f.Type == SettingType.Double && f.Key.Equals(key));

            if (result == null)
                throw new NullReferenceException("Setting not found.");
            
            return Convert.ToDouble(result.Value);
        }
        
        public async Task<DateTime> GetDateTime(string key)
        {
            return Convert.ToDateTime((await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == null && f.Type == SettingType.DateTime && f.Key.Equals(key))).Value);
        }
        
        public async Task<int> GetInt(string key, string subKey)
        {
            return Convert.ToInt32((await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == subKey && f.Type == SettingType.Int && f.Key.Equals(key))).Value);
        }
        
        public async Task<string> GetString(string key, string subKey)
        {
            return (await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == subKey && f.Type == SettingType.String && f.Key.Equals(key))).Value;
        }
        
        public async Task<double> GetDouble(string key, string subKey)
        {
            var result = await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == subKey && f.Type == SettingType.Double && f.Key.Equals(key));

            if (result == null)
                throw new NullReferenceException("Setting not found.");
            
            return Convert.ToDouble(result.Value);
        }
        
        public async Task<DateTime> GetDateTime(string key, string subKey)
        {
            return Convert.ToDateTime((await _repository.FirstOrDefaultAsync(f =>
                f.IsActive && f.SubKey == subKey && f.Type == SettingType.DateTime && f.Key.Equals(key))).Value);
        }
        
        public async Task<SettingEntity> Get(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(f => f.Id == id);
        }
        

        public DataTableProxy<SettingEntity> Get(DataTablePayload payload)
        {
            var list = _repository.GetAll();

            var recordsTotal = list.Count();

            //Sorting  
            if (!(string.IsNullOrEmpty(payload.SortColumn)))
            {
                switch (payload.SortColumn)
                {
                    case "Name":
                        list = payload.IsAsc() ? list.OrderBy(s => s.Name) : list.OrderByDescending(s => s.Name);
                        break;
                    case "Value":
                        list = payload.IsAsc() ? list.OrderBy(s => s.Value) : list.OrderByDescending(s => s.Value);
                        break;
                }
            }

            //Search  
            if (!string.IsNullOrEmpty(payload.SearchValue))
            {
                var array = payload.SearchValue.Split(' ');

                array = array.Where(w => !string.IsNullOrEmpty(w)).ToArray();

                foreach (var s in array)
                {
                    list = list.Where(m => m.Name.ToLower().Contains(s.ToLower()) || 
                                           m.Name.ToLower().RemoveDiacritics().Contains(s.ToLower().RemoveDiacritics()));
                }
                
                
            }

            //total number of rows counts   
            var recordsFiltred = list.Count();
            //Paging   
            var data = list.Skip(payload.Skip).Take(payload.PageSize).ToList();

            //Returning Json Data  
            return new DataTableProxy<SettingEntity>()
            {
                Draw = payload.Draw,
                RecordsFiltered = recordsFiltred,
                RecordsTotal = recordsTotal,
                Data = data
            };
        }
        
       

        public List<SettingProxy> GetClientSettings()
        {
            var list = _repository.Where(w => w.IsActive && w.ClientUses).Select(s => new SettingProxy()
            {
                Value = s.Value,
                Key = s.Key
            }).ToList();

            return list;
        }

        public async Task<SettingEntity> UpdateSetting(SettingEntity payload)
        {
            var setting = await _repository.FirstOrDefaultAsync(f => f.Id == payload.Id);

            setting.Value = payload.Value;

            await _repository.UpdateAndSaveAsync(setting);

            return payload;
        }
    }
}