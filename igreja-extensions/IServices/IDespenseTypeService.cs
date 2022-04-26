using MongoDB.Driver;
using nrmcontrolextension.Filters;
using nrmcontrolextension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.IServices
{
    public interface IDespenseTypeService
    {
        public Task<List<DespenseType>> GetDespenseTypesByUser(string userId);
        public Task<DespenseType> InsertDespenseType(DespenseType despenseType);
    }
}
