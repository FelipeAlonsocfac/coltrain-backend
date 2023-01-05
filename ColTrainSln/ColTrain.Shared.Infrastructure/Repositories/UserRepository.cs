using ColTrain.Shared.Contracts.Interfaces.Repositories;
using ColTrain.Shared.DTO.Models;
using ColTrain.Shared.Infrastructure.DataAccess;

namespace ColTrain.Shared.Infrastructure.Repositories
{
    public class UserRepository :  BaseRepository<UserTable>, IUserRepository
    {
        public UserRepository(ColTrainDbContext context) : base(context)
        {

        }
    }
}
