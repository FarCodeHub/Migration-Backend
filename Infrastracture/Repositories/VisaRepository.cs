using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence;
using Persistence.Repositories;

namespace Infrastracture.Repositories
{
    public class VisaRepository : Repository<Visa>,IVisaRepository
    {
        public VisaRepository(MigrationContext context) : base(context)
        {
        }

    }
}
