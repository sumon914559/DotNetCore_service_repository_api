using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repositories
{
    public interface ICustomerInfoRepository
    {
    }

    public class CustomerInfoRepository : ICustomerInfoRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
