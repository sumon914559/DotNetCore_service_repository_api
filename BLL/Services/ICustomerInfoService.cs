using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface ICustomerInfoService
    {
    }

    public class CustomerInfoService : ICustomerInfoService
    {
        private readonly CustomerInfoRepository _customerInfoRepository;

        public CustomerInfoService(ICustomerInfoRepository customerInfoRepository)
        {
          //  _customerInfoRepository = customerInfoRepository;
        }

    }
}
