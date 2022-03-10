using ColTrain.Shared.Contracts.Interfaces.Repositories;
using ColTrain.Shared.DTO.Models;
using ColTrain.Shared.Infrastructure.DataAccess;

namespace ColTrain.Shared.Infrastructure.Repositories
{
    public class CityRepository :  BaseRepository<CityTable>, ICityRepository
    {
        public CityRepository(ColTrainDbContext context) : base(context)
        {

        }
    }
}
