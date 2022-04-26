using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.Models
{
    [BsonIgnoreExtraElements]
    public class DespenseType
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string UserId { get; set; }

        public bool MonthFixed { get; set; }

        public DateTime? StartDate { get; set; }

        public DespenseType(int id,
            string userId,
            string description,
            bool monthFixed,
            DateTime? startDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.Description = description;
            this.MonthFixed = monthFixed;
            this.StartDate = startDate;
        }
    }
}
