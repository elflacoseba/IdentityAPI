using AutoMapper;
using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;
using Identity.API.Exceptions;
using Identity.API.Interfaces;
using Identity.API.Validators;

namespace Identity.API.Services
{
    public class ApplicationUserService : IApplicationUserService
    {        
        private readonly IMapper _mapper;
        private readonly CreateApplicationUserRequestDtoValidator _applicationUserRequestDtoValidationRules;
        private readonly UpdateApplicationUserRequestDtoValidator _updateApplicationUserRequestDtoValidator;

        public ApplicationUserService(IMapper mapper, CreateApplicationUserRequestDtoValidator applicationUserRequestDtoValidationRules, UpdateApplicationUserRequestDtoValidator updateApplicationUserRequestDtoValidator)
        {            
            _mapper = mapper;
            _applicationUserRequestDtoValidationRules = applicationUserRequestDtoValidationRules;
            _updateApplicationUserRequestDtoValidator = updateApplicationUserRequestDtoValidator;
        }

        #region

        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {

            //var usersEntity = await _userRepository.GetUsersAsync();

            //return _mapper.Map<IEnumerable<UserResponseDto>>(usersEntity);

            return new List<UserResponseDto>();
        }

        public async Task<UserResponseDto?> GetUserByUsernameAsync(string username)
        {
            //var userEntity = await _userRepository.GetUserByUsernameAsync(username);

            //return _mapper.Map<UserResponseDto>(userEntity);

            return new UserResponseDto();
        }

        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            //var userEntity = await _userRepository.GetUserByEmailAsync(email);

            //return _mapper.Map<UserResponseDto>(userEntity);

            return new UserResponseDto();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(string userId)
        {
            //var userEntity = await _userRepository.GetUserByIdAsync(userId);

            //return _mapper.Map<UserResponseDto>(userEntity);
            return new UserResponseDto();
        }

        public async Task<bool> CreateUserAsync(CreateApplicationUserRequestDto user)
        {

            //var validationResult = await _applicationUserRequestDtoValidationRules.ValidateAsync(user);           

            //if (!validationResult.IsValid)
            //{
            //    var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage))
            //                                 .ToList();

            //    throw new ValidationException(errorValidations);
            //}

            ////Valido que no exista el username en la base de datos
            //var userUsername = await _userRepository.GetUserByUsernameAsync(user.UserName!);

            //if (userUsername != null)
            //{
            //    List<ErrorValidation> errorValidations = new List<ErrorValidation>();

            //    errorValidations.Add(new ErrorValidation(nameof(user.UserName), "El nombre de usuario ya existe en el sistema."));

            //    throw new ValidationException(errorValidations);
            //}

            ////Valido que no exista el email en la base de datos
            //var userEmail = await _userRepository.GetUserByEmailAsync(user.Email!);

            //if (userEmail != null)
            //{
            //    List<ErrorValidation> errorValidations = new List<ErrorValidation>();

            //    errorValidations.Add(new ErrorValidation(nameof(user.Email), "El email ya existe en el sistema."));

            //    throw new ValidationException(errorValidations);
            //}

            //var userEnity = _mapper.Map<ApplicationUser>(user);

            //return await _userRepository.CreateUserAsync(userEnity, user.Password!);

            return true;
        }

        public async Task<bool> UpdateUserAsync(UpdateApplicationUserRequestDto user)
        {
            //var validationResult = await _updateApplicationUserRequestDtoValidator.ValidateAsync(user);

            //if (!validationResult.IsValid)
            //{
            //    var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage))
            //                                 .ToList();

            //    throw new ValidationException(errorValidations);
            //}

            ////Valido que no exista el username en la base de datos
            //var userUsername = await _userRepository.GetUserByUsernameAsync(user.UserName!);

            ////si obtuvo un usuario con el mismo username, valido que no sea el mismo usuario que se esta actualizando
            //if (userUsername != null && userUsername.Id != user.Id!)
            //{
            //    List<ErrorValidation> errorValidations = new List<ErrorValidation>();

            //    errorValidations.Add(new ErrorValidation(nameof(user.UserName), "El nombre de usuario ya existe en el sistema."));

            //    throw new ValidationException(errorValidations);
            //}

            ////Valido que no exista el email en la base de datos
            //var userEmail = await _userRepository.GetUserByEmailAsync(user.Email!);

            ////si obtuvo un usuario con el mismo email, valido que no sea el mismo usuario que se esta actualizando
            //if (userEmail != null && userEmail.Id != user.Id!)
            //{
            //    List<ErrorValidation> errorValidations = new List<ErrorValidation>();

            //    errorValidations.Add(new ErrorValidation(nameof(user.Email), "El email ya existe en el sistema."));

            //    throw new ValidationException(errorValidations);
            //}            

            //var userEntity = _mapper.Map<ApplicationUser>(user);

            //return await _userRepository.UpdateUserAsync(userEntity!);

            return true;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            // return await _userRepository.DeleteUserAsync(userId);

            return true;
        }

        #endregion

        #region SingIn

        public async Task<bool> CheckPasswordSignInAsync(string userId, string password, bool lockoutOnFailure)
        {
            //var userEntity = await _userRepository.GetUserByIdAsync(userId);

            //return await _userRepository.CheckPasswordSignInAsync(userEntity!, password, lockoutOnFailure); 

            return false;
        }

        #endregion

        #region Roles

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            //return await _userRepository.GetRolesAsync(userId);

            return new List<string>();
        }

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
            //return await _userRepository.AddToRoleAsync(userId, roleName);
            return true;
        }

        public async Task<bool> AddToRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            // return _userRepository.AddToRolesAsync(userId, roleNames);

            return true;
        }

        public async Task<bool> RemoveFromRoleAsync(string userId, string roleName)
        {
            // return await _userRepository.RemoveFromRoleAsync(userId, roleName);
            return true;
        }

        public async Task<bool> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            // return await _userRepository.RemoveFromRolesAsync(userId, roleNames);
            return true;
        }
        
        #endregion
    }
}
