using CustomerMgt.Core.Models;
using CustomerMgt.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Data
{
    public static class Mapper
    {
        #region customer
        public static CustomerModel Map(this Customer entity)
        {
            if (entity == null)
                return null;

            return new CustomerModel
            {
                Id = entity.Id,
                DateCreated = entity.DateCreated,
                Address = entity.Address,
                Email = entity.Email,
                FirstName = entity.FirstName,
                Gender = entity.Gender,
                LastName = entity.LastName,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,


            };
        }
        public static Customer Map(this CustomerModel model)
        {
            if (model == null)
                return null;

            return new Customer
            {
                Id = model.Id,
                Address = model.Address,
                Email = model.Email,
                FirstName = model.FirstName,
                Gender = model.Gender,
                LastName = model.LastName,
                DateCreated = model.DateCreated,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted
            };
        }
        #endregion
    }
}
