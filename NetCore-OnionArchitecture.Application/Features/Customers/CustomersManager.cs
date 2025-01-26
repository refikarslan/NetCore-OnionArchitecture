using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCore_OnionArchitecture.Application.DTOs;
using NetCore_OnionArchitecture.Application.Exceptions;
using NetCore_OnionArchitecture.Domain.Common.DI;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using NetCore_OnionArchitecture.Domain.Common.UnitOfWork;
using NetCore_OnionArchitecture.Domain.Entities;

namespace NetCore_OnionArchitecture.Application.Features.Customers
{
    public class CustomersManager : ITransientDependency
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Customer, int> _customerRepository;
        private readonly IMapper _mapper;
        
        public CustomersManager(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerRepository = _unitOfWork.GetRepository<Customer, int>();
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            return await _customerRepository.GetAll()
                
                .Select(x => new CustomerDto
                {
                    Address = x.Address,
                    Age = x.Age,
                    DateOfBirth = x.DateOfBirth,
                    EMail = x.EMail,
                    CustomerId = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Tel = x.Tel 
                }).ToListAsync();
        }

        public async Task<CustomerDto> GetCustomerForEdit(int customerId)
        {
            var input = await _customerRepository.GetAll()
               .FirstOrDefaultAsync(x => x.Id == customerId);
            if (input == null)
                throw new ApiException("Müşteri bulunamadı");

            return new CustomerDto
            {
                Address = input.Address,
                Age = input.Age,
                DateOfBirth = input.DateOfBirth,
                EMail = input.EMail,
                Name = input.Name,
                Surname = input.Surname,
                Tel = input.Tel
            };
        }

        public async Task<int> AddOrEditCustomer(CustomerDto input)
        {
            if(input.CustomerId == null)
                return await createCustomer(input);
            else 
                return await updateCustomer(input);
        }
        
        private async Task<int> createCustomer(CustomerDto input)
        {
            var create = new Customer
            {
                Address = input.Address,
                Age = input.Age,
                DateOfBirth = input.DateOfBirth,
                EMail = input.EMail,
                Name = input.Name,
                Surname = input.Surname,
                Tel = input.Tel
            };

            await _customerRepository.InsertAsync(create);
            await _unitOfWork.CompleteAsync();

            return create.Id;
        }

        private async Task<int> updateCustomer(CustomerDto input)
        {
            var customer = await _customerRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == input.CustomerId);
            if (customer == null)
                throw new ApiException("Müşteri bulunamadı");

            customer.Name = input.Name;
            customer.Surname = input.Surname;
            customer.Tel = input.Tel;
            customer.Address = input.Address;
            customer.Age = input.Age;
            customer.DateOfBirth = input.DateOfBirth;
            customer.EMail = input.EMail;
            
            await _unitOfWork.CompleteAsync();
            return customer.Id;
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _customerRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
                throw new ApiException("Müşteri bulunamadı");

            await _customerRepository.DeleteAsync(customer);
            await _unitOfWork.CompleteAsync();
        }

    }
}
