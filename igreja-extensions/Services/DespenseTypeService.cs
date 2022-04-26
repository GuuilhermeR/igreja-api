using nrmcontrolextension.Filters;
using nrmcontrolextension.IRepositories;
using nrmcontrolextension.IServices;
using nrmcontrolextension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.Services
{
    public class DespenseTypeService : IDespenseTypeService
    {
        private readonly IDespenseTypeRepository _IDespenseTypeRepository;
        public DespenseTypeService(IDespenseTypeRepository iDespenseTypeRepositoy)
        {
            this._IDespenseTypeRepository = iDespenseTypeRepositoy;
        }
        public async Task<List<DespenseType>> GetDespenseTypesByUser(string userId)
        {
            ValidateUser(userId);
            DespenseTypeFilter filter = new() { UserId = userId };
            return await this._IDespenseTypeRepository.GetDespensesTypesByUser(filter);
        }

        private static void ValidateUser(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("O usuário é obrigatório!");
            }
        }


        public async Task<DespenseType> InsertDespenseType(DespenseType despenseType)
        {
            return await this._IDespenseTypeRepository.InsertDespenseType(despenseType);
        }

    }
}
