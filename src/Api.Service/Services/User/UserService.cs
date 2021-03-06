using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private IRepository<Api.Domain.Entities.User> _repository;
        
        public UserService(IRepository<Api.Domain.Entities.User> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            var result = await _repository.SelectAsync(id);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var listresult =  await _repository.SelectAsync();
              return _mapper.Map<IEnumerable<UserDto>>(listresult);
        } 

        public async Task<UserDtoCreateResult> Post(UserDto user)
        {
            var model = _mapper.Map<UserModel>(user); 
            var entity = _mapper.Map<User>(user); 
            
            foreach (var adress in entity.Addresses)
            {
                adress.CreateAt = entity.CreateAt;
            }

            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<UserDtoCreateResult>(result);
        }

        public async Task<UserDtoCreateResult> Put(UserDto user)
        {
             var model = _mapper.Map<UserModel>(user);
             var entity = _mapper.Map<User>(user); 
            foreach (var adress in entity.Addresses)
            {
                adress.CreateAt = entity.CreateAt;
                adress.UpdateAt = entity.UpdateAt;
            }

             var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDtoCreateResult>(result);
        }

 
    }
}
