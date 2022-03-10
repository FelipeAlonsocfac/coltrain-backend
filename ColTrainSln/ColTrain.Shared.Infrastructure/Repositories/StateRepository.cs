using ColTrain.Shared.Contracts.Interfaces.Repositories;
using ColTrain.Shared.DTO.Models;
using ColTrain.Shared.Infrastructure.DataAccess;

namespace ColTrain.Shared.Infrastructure.Repositories
{
    public class StateRepository :  BaseRepository<StateTable>, IStateRepository
    {
        public StateRepository(ColTrainDbContext context) : base(context)
        {

        }
    }
}
