using Microsoft.AspNetCore.Authorization;
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
    [Authorize(IdentityPermissions.Users.Default, Policy = "AdminOnly")]

    public class UsersAppService : CrudAppService<IdentityUser, UserDto, Guid, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUsersAppService
    {
        private readonly IdentityUserManager _userManager;

        public UsersAppService(IRepository<IdentityUser, Guid> repository, IdentityUserManager userManager) : base(repository)
        {
            _userManager = userManager;
            GetPolicyName = IdentityPermissions.Users.Default;
            GetListPolicyName = IdentityPermissions.Users.Default;
            CreatePolicyName = IdentityPermissions.Users.Create;
            UpdatePolicyName = IdentityPermissions.Users.Update;
            DeletePolicyName = IdentityPermissions.Users.Delete;
        }

        [Authorize(IdentityPermissions.Users.Delete)]

        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(IdentityPermissions.Users.Default)]
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

        [Authorize(IdentityPermissions.Users.Default)]
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
        [Authorize(IdentityPermissions.Users.Create)]
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
            user.SetPhoneNumber(input.PhoneNumber, true);
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
       
        [Authorize(IdentityPermissions.Users.Update)]
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
        [Authorize(IdentityPermissions.Users.Default)]

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

        [Authorize(IdentityPermissions.Users.Create)]
        public async Task AssignRoleAsync(Guid userId, string[] roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser), userId);
            }
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removedResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var addedResullt = await _userManager.AddToRolesAsync(user, roleNames);

            if (!addedResullt.Succeeded || !removedResult.Succeeded)
            {
                List<IdentityError> addedErrorList = addedResullt.Errors.ToList();
                List<IdentityError> removedErrorList = removedResult.Errors.ToList();
                var errorList = new List<IdentityError>();
                errorList.AddRange(addedErrorList);
                errorList.AddRange(removedErrorList);
                string errors = "";
                foreach (var error in errorList)
                {
                    errors = errors + error.Description.ToString();

                }
                throw new UserFriendlyException(errors);
            }

        }


        [Authorize(IdentityPermissions.Users.Create)]

        public async Task SetPasswordAsync(Guid userId, SetPasswordDto input)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser), userId);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, input.NewPassword);
            if (!result.Succeeded)
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
    }
}