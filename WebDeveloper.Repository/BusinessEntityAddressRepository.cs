using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;


namespace WebDeveloper.Repository
{
    public class BusinessEntityAddressRepository : BaseRepository<BusinessEntityAddress>
    {
       /* public BusinessEntityAddress GetById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityAddress.FirstOrDefault(p => p.BusinessEntityID == id);
            }
        }

        public List<Address> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.Address
                    .OrderByDescending(p => p.ModifiedDate)
                    .Take(size).ToList();
            }
        }

        public Address GetCompletePersonById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.Address
                    .Include(p => p.AddressID)
                    .FirstOrDefault(p => p.AddressID == id);
            }
        }

    */
    }
}
