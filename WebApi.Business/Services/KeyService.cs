using WebApi.Entity.Models;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Services;

public class KeyService : IKeyService
{
    private readonly IRepository<Key> _keyRepository;
    public KeyService(IRepository<Key> keyRepository)
    {
        _keyRepository = keyRepository;
    }
    public Key GetKey()
    {
        var key = _keyRepository.GetAll().ToList()
            .FirstOrDefault(key => key.Used == false);
        var update = new Key
        {
            Id = key.Id,
            Value = key.Value,
            Used = true
        };
        _keyRepository.Update(update);
        return new Key
        {
            Id = key.Id,
            Value = key.Value,
            Used = true
        };
    }
}
