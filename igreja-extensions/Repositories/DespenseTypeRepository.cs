using BibliotecaDLL.DbContexts;
using MongoDB.Bson;
using MongoDB.Driver;
using nrmcontrolextension.Filters;
using nrmcontrolextension.IRepositories;
using nrmcontrolextension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.Repositories
{
    public class DespenseTypeRepository : IDespenseTypeRepository
    {
        private readonly MongoDbContext _Connection;
        public DespenseTypeRepository()
        {
            this._Connection = new MongoDbContext();
        }

        public async Task<List<DespenseType>> GetDespensesTypesByUser(DespenseTypeFilter despenseTypeFilter)
        {
            FilterDefinitionBuilder<DespenseType> builder = Builders<DespenseType>.Filter;
            FilterDefinition<DespenseType> filter = builder.Empty;
            if (despenseTypeFilter != null && !string.IsNullOrEmpty(despenseTypeFilter.UserId))
            {
                filter &= builder.Where(u => u.UserId == despenseTypeFilter.UserId);
            }
            List<DespenseType> listaTiposDespesa = await _Connection.DespenseTypes.Find(filter).ToListAsync();
            return listaTiposDespesa;
        }

        public async Task<DespenseType> InsertDespenseType(DespenseType despenseType)
        {
            ValidateDespenseType(despenseType);
            DespenseType despenseTypeBase = _Connection.DespenseTypes.Aggregate().SortByDescending(x => x.Id).FirstOrDefault();
            if (despenseTypeBase != null)
            {
                despenseType.Id = despenseTypeBase.Id + 1;
            }
            else
            {
                despenseType.Id = 1;
            }
            await _Connection.DespenseTypes.InsertOneAsync(despenseType);
            return despenseType;
        }

        public DespenseType UpdateDespenseType(DespenseType despenseType)
        {
            ValidateDespenseType(despenseType);
            var filter = Builders<DespenseType>.Filter.Where(e => e.Id == despenseType.Id);
            var update = Builders<DespenseType>.Update
                .Set(x => x.Description, despenseType.Description)
                .Set(x => x.MonthFixed, despenseType.MonthFixed)
                .Set(x => x.StartDate, despenseType.StartDate);
            _Connection.DespenseTypes.UpdateOne(filter, update);
            return despenseType;
        }


        public void DeleteDespenseType(DespenseType despenseType)
        {
            var filter = Builders<DespenseType>.Filter.Where(e => e.Id == despenseType.Id);
            _Connection.DespenseTypes.DeleteOne(filter);

        }

        public static void ValidateDespenseType(DespenseType despenseType)
        {
            if (string.IsNullOrEmpty(despenseType.Description))
            {
                throw new ArgumentException("Descrição");
            }
            else if (string.IsNullOrEmpty(despenseType.UserId))
            {
                throw new ArgumentException("Usuário");
            }
        }
    }
}
