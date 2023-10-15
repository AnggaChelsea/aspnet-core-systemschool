using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.Services;

public interface IMappelService{
    IEnumerable<Mapel> GetMapels();
}