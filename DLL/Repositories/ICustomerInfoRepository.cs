using DLL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repositories
{
    public interface ICustomerInfoRepository : IBaseRepository<CustomerInfo>
    {
    }

    public class CustomerInfoRepository : BaseRepository<CustomerInfo>, ICustomerInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerInfoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
