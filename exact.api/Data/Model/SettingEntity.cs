using exact.data.Enum;

namespace exact.api.Data.Model
{
    public class SettingEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public string Value { get; set; }
        
        public string Key { get; set; }
        
        public string SubKey { get; set; }
        
        /// <summary>
        /// Indicates that if the configuration will go down to the applications,
        /// which per hour in LavaSim will not be used :)
        /// </summary>
        public bool ClientUses { get; set; }
        
        public SettingType Type { get; set; }
        
    }
}