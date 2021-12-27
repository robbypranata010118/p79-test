using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GaransiBank.Base.Dtos.Login;
using GaransiBank.Base.Interfaces;
using GaransiBank.Domain.Entities;

using System;
using System.Linq;
using System.Threading.Tasks;
using GaransiBank.Base.Extentions;
using GaransiBank.Base.Dtos.User;
using System.Collections.Generic;
using System.Text;
using GaransiBank.Base.Exceptions;
using Constant = GaransiBank.Base.Commons.CommonConstants;
using System.Linq.Dynamic.Core;
using GaransiBank.Base.Extensions;
using GaransiBank.Base.Commons;

namespace GaransiBank.Infrastructure.Persistence.Interfaces
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _appDbContext;
        private readonly ILogger<UserService> _logger;
        private readonly ILogService _logService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMailService _mailService;
        public UserService(AppDBContext appDbContext, ILogger<UserService> logger, ILogService logService, ICurrentUserService currentUserService,
            IMailService mailService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _logService = logService;
            _currentUserService = currentUserService;
            _mailService = mailService;
        }

        public async Task<int> Delete(Guid Id)
        {

            var checkUser = await _appDbContext.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (checkUser == null)
                throw new Exception("Data tidak terdaftar");
            var checkUserInLog = await _appDbContext.Logs.Where(x => x.UserId == Id.ToString()).FirstOrDefaultAsync();
            if (checkUserInLog != null)
                throw new Exception("User tidak dapat dihapus");
            LogDto log = new LogDto
            {
                Id = Guid.NewGuid(),
                Action = Constant.ACTION_DELETE,
                ApplicationName = Constant.NAMA_APLIKASI,
                Detail = "",
                Feature = Constant.FEATURE_USERS,
                LogDate = DateTime.Now,
                Message = "Success",
                Module = Constant.MODULE_HAK_AKSES,
                ReferenceId = Id.ToString(),
                RoleId = _currentUserService.RoleId,
                RoleName = _currentUserService.RoleName,
                UserId = _currentUserService.UserId,
                UserName = _currentUserService.UserName
            };
            _appDbContext.Users.Remove(checkUser);
            await _logService.InsertLog(log);
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUserAdmin(GetUserAdminRequestVm request)
        {
            var data = _appDbContext.Users
                .Include(x => x.Unit)
                .Include(x => x.Role).AsQueryable();
            if (request.Filters != null && request.Filters.Count > 0)
            {

                foreach (var item in request.Filters)
                {
                    var where = item.Field + item.GetComparasionType() + (item.IsUseDoubleQuote() ? "\"" : "") + item.GetFilterValue() + (item.IsUseDoubleQuote() ? "\"" : "");
                    if (item.ComparisonOperator == "like" || item.ComparisonOperator == "not like")
                    {
                        where += item.GetClosedTagComparisonOperator();
                    }
                    data = data.Where(where);
                }

            }
            if (request.Orders != null && request.Orders.Count > 0)
            {
                foreach (var item in request.Orders)
                {
                    data = data.OrderBy(item + " " + request.SortType);
                }
            };
            return await data
            .Skip(request.CalculateOffset())
            .Take(request.Length)
            .ToListAsync();
        }

        public async Task<int> GetCountUserAdmin(GetUserAdminRequestVm request)
        {
            var data = _appDbContext.Users
                .Include(x => x.Unit)
                .Include(x => x.Role).AsQueryable();
            if (request.Filters != null && request.Filters.Count > 0)
            {
                foreach (var item in request.Filters)
                {
                    var where = item.Field + item.GetComparasionType() + (item.IsUseDoubleQuote() ? "\"" : "") + item.GetFilterValue() + (item.IsUseDoubleQuote() ? "\"" : "");
                    if (item.ComparisonOperator == "like" || item.ComparisonOperator == "not like")
                    {
                        where += item.GetClosedTagComparisonOperator();
                    }
                    data = data.Where(where);
                }
            }
            if (request.Orders != null && request.Orders.Count > 0)
            {
                foreach (var item in request.Orders)
                {
                    data = data.OrderBy(item + " " + request.SortType);
                }
            };
            return await data.CountAsync();
        }

        public async Task<User> GetUserByIdAdmin(Guid Id)
        {
            return await _appDbContext.Users
                .Include(x => x.Unit)
                .Include(x => x.Role)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<User> LoginPublic(LoginRequestDto loginRequest)
        {
            bool passwordValid = false;
            var checkAPIClient = await _appDbContext.ApiClients.Where(x => x.Id == loginRequest.ClientId && x.ClientSecret == loginRequest.ClientSecret).FirstOrDefaultAsync();
            if (checkAPIClient != null)
            {
                var user = await _appDbContext.Users
                    .Include(x => x.Role)
                    .Include(x => x.Unit)
                    .Where(x => x.NIK == loginRequest.NIK)
                    .FirstOrDefaultAsync();
               
                if (user != null)
                {
                    if (!user.IsActive)
                        throw new BadRequestException("Akun anda tidak aktif silahkan kontak sistem administrator saleskit");
                    var password = (loginRequest.Password + user.Salt).ToSHA512();
                    passwordValid = user.Password == password ? true : false;
                }
                return passwordValid ? user : null;
            }
            else
            {
                throw new BadRequestException("Client ID is required");
            }
        }

        public async Task<int> Post(PostUserAdminDto request)
        {
            #region Check Role
            var roleExist = _appDbContext.Roles.Where(x => x.Id == request.Role).FirstOrDefault();
            if (roleExist == null)
                throw new Exception("Role belum terdaftar dalam sistem");
            #endregion
            #region Check Unit Level
            var unitExist = _appDbContext.Units.Where(x => x.Id == request.Unit).FirstOrDefault();
            if (unitExist == null)
                throw new Exception("Unit belum terdaftar dalam sistem");
            #endregion
            #region Check NIK
            var userExist = _appDbContext.Users.Where(x => x.NIK == request.NIK).FirstOrDefault();
            if (userExist != null)
                throw new DuplicateException("NIK sudah terdaftar untuk user lain");
            #endregion
            #region Check Email
            var emailExist = _appDbContext.Users.Where(x => x.Email == request.Email).FirstOrDefault();
            if (emailExist != null)
                throw new DuplicateException("Email sudah terdaftar untuk user lain");
            #endregion
            string salt = SaltGenerator.GetUniqueToken();
            byte[] generatedSalt = Encoding.Default.GetBytes(salt);
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                EmailVerified = false,
                Password = "123",//add by egi
                Name = request.Name,
                NIK = request.NIK,
                RoleID = request.Role,
                Title = request.Title,
                UnitID = request.Unit,
                Salt = generatedSalt,
                PasswordSalt = Convert.ToBase64String(generatedSalt),
                IsActive = false,
            };
            LogDto log = new LogDto
            {
                Id = Guid.NewGuid(),
                Action = Constant.ACTION_INSERT,
                ApplicationName = Constant.NAMA_APLIKASI,
                Detail = "",
                Feature = Constant.FEATURE_USERS,
                LogDate = DateTime.Now,
                Message = "Success",
                Module = Constant.MODULE_HAK_AKSES,
                ReferenceId = user.Id.ToString(),
                RoleId = _currentUserService.RoleId,
                RoleName = _currentUserService.RoleName,
                UserId = _currentUserService.UserId,
                UserName = _currentUserService.UserName
            };
            _appDbContext.Users.Add(user);
            await _logService.InsertLog(log);
            await _mailService.SendEmailVerification(user.Id.ToString(), request.Email, "Verification", Constant.EMAIL_VERIFICATION, true);
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> Put(PutUserAdminDto request)
        {
            var checkUser = await _appDbContext.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (checkUser == null)
                throw new Exception("User tidak terdaftar");
            #region Check Role
            var roleExist = await _appDbContext.Roles.Where(x => x.Id == request.Role).FirstOrDefaultAsync();
            if (roleExist == null)
                throw new Exception("Role belum terdaftar dalam sistem");
            #endregion
            #region Check Unit Level
            var unitExist = await _appDbContext.Units.Where(x => x.Id == request.Unit).FirstOrDefaultAsync();
            if (unitExist == null)
                throw new Exception("Unit belum terdaftar dalam sistem");
            #endregion
            #region Check NIK
            if (checkUser.NIK != request.NIK)
            {
                var userExist = await _appDbContext.Users.Where(x => x.NIK == request.NIK).Where(x => x.IsActive).FirstOrDefaultAsync();
                if (userExist != null)
                    throw new Exception("NIK sudah terdaftar untuk user lain");
            }
            #endregion
            #region Check Email
            if (checkUser.Email != request.Email)
            {
                var emailExist = await _appDbContext.Users.Where(x => x.Email == request.Email).Where(x => x.IsActive).FirstOrDefaultAsync();
                if (emailExist != null)
                    throw new Exception("Email sudah terdaftar untuk user lain");
            }
            #endregion
            checkUser.Name = request.Name;
            checkUser.NIK = request.NIK;
            checkUser.RoleID = request.Role;
            checkUser.UnitID = request.Unit;
            checkUser.Title = request.Title;
            checkUser.Email = request.Email;
            checkUser.IsActive = request.IsActive;
            LogDto log = new LogDto
            {
                Id = Guid.NewGuid(),
                Action = Constant.ACTION_UPDATE,
                ApplicationName = Constant.NAMA_APLIKASI,
                Detail = "",
                Feature = Constant.FEATURE_USERS,
                LogDate = DateTime.Now,
                Message = "Success",
                Module = Constant.MODULE_HAK_AKSES,
                ReferenceId = checkUser.Id.ToString(),
                RoleId = _currentUserService.RoleId,
                RoleName = _currentUserService.RoleName,
                UserId = _currentUserService.UserId,
                UserName = _currentUserService.RoleName
            };
            _appDbContext.Users.Update(checkUser);
            await _logService.InsertLog(log);
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
