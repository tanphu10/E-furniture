using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace ERP.TANDUNG.Admin.System.Users
{
    public class UsersAppService : CrudAppService<IdentityUser, UserDto, Guid, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUsersAppService
    {
        private readonly IdentityUserManager _userManager;

        public UsersAppService(IRepository<IdentityUser, Guid> repository, IdentityUserManager userManager) : base(repository)
        {
            _userManager = userManager;
        }

        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<List<UserInListDto>> GetListAllAsync(string filterKeyword)
        {
            var query = await Repository.GetQueryableAsync();
            if (!string.IsNullOrEmpty(filterKeyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filterKeyword) ||
                x.Email.ToLower().Contains(filterKeyword) || x.PhoneNumber.ToLower().Contains(filterKeyword));
            };
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<IdentityUser>, List<UserInListDto>>(data);
        }

        public async Task<PagedResultDto<UserInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();

            if (!input.Keyword.IsNullOrWhiteSpace())
            {
                input.Keyword = input.Keyword.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(input.Keyword) ||
                x.Email.ToLower().Contains(input.Keyword) || x.PhoneNumber.ToLower().Contains(input.Keyword));
            }
            query = query.OrderByDescending(x => x.CreationTime);
            var totalCount = await AsyncExecuter.CountAsync(query);
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var data = await AsyncExecuter.ToListAsync(query);
            var users = ObjectMapper.Map<List<IdentityUser>, List<UserInListDto>>(data);
            return new PagedResultDto<UserInListDto>(totalCount, users);
        }
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            var query = await Repository.GetQueryableAsync();
            var isUserNameExistd = query.Any(x => x.UserName == input.UserName);
            if (isUserNameExistd)
            {
                throw new UserFriendlyException("Account is Existed");
            }
            var isEmailExistd = query.Any(x => x.Email == input.Email);
            if (isEmailExistd)
            {
                throw new UserFriendlyException("Email is Existed");
            }
            var userId = Guid.NewGuid();
            var user = new IdentityUser(userId, input.UserName, input.Email);
            user.Name = input.Name;
            user.Surname = input.SurName;
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                return ObjectMapper.Map<IdentityUser, UserDto>(user);
            }
            else
            {
                List<IdentityError> errorList = result.Errors.ToList();
                string errors = "";
                foreach (var error in errorList)
                {
                    errors = errors + error.Description.ToString();

                }
                throw new UserFriendlyException(errors);
            }
        }
        public override async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto input)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser), id);
            }
            user.Name = input.Name;
            user.Surname = input.SurName;
            user.SetPhoneNumber(input.PhoneNumber, true);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return ObjectMapper.Map<IdentityUser, UserDto>(user);
            }
            else
            {
                List<IdentityError> errorList = result.Errors.ToList();
                string errors = "";
                foreach (var error in errorList)
                {
                    errors = errors + error.Description.ToString();

                }
                throw new UserFriendlyException(errors);
            }
        }
        public override async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser), id);
            }
            var userDto = ObjectMapper.Map<IdentityUser, UserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = roles;
            return userDto;
        }
    }
}