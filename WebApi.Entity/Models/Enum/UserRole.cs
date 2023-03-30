using System.Runtime.Serialization;

namespace WebApi.Entity.Models.Enum;
    public enum UserRole
    {
        [EnumMember(Value = "User")]
        User,
        [EnumMember(Value = "Admin")]
        Admin
    }
