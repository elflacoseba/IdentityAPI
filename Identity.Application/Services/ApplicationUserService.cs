using AutoMapper;
using Identity.Application.Dtos.Request;
using Identity.Application.Dtos.Response;
using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using Identity.Application.Validators;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;

namespace Identity.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly CreateApplicationUserRequestDtoValidator _applicationUserRequestDtoValidationRules;
        private readonly UpdateApplicationUserRequestDtoValidator _updateApplicationUserRequestDtoValidator;

        public ApplicationUserService(IApplicationUserRepository userRepository, IMapper mapper, CreateApplicationUserRequestDtoValidator applicationUserRequestDtoValidationRules, UpdateApplicationUserRequestDtoValidator updateApplicationUserRequestDtoValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _applicationUserRequestDtoValidationRules = applicationUserRequestDtoValidationRules;
            _updateApplicationUserRequestDtoValidator = updateApplicationUserRequestDtoValidator;
        }

        #region

        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
        {

            var usersEntity = await _userRepository.GetUsersAsync();

            return _mapper.Map<IEnumerable<UserResponseDto>>(usersEntity);
        }

        public async Task<UserResponseDto?> GetUserByUsernameAsync(string username)
        {
            var userEntity = await _userRepository.GetUserByUsernameAsync(username);

            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(email);

            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(string userId)
        {
            var userEntity = await _userRepository.GetUserByIdAsync(userId);

            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<bool> CreateUserAsync(CreateApplicationUserRequestDto user)
        {
     
            var validationResult = await _applicationUserRequestDtoValidationRules.ValidateAsync(user);           

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage))
                                             .ToList();

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el username en la base de datos
            var userUsername = await _userRepository.GetUserByUsernameAsync(user.UserName!);

            if (userUsername != null)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(user.UserName), "El nombre de usuario ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el email en la base de datos
            var userEmail = await _userRepository.GetUserByEmailAsync(user.Email!);

            if (userEmail != null)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(user.Email), "El email ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            var userEnity = _mapper.Map<ApplicationUser>(user);

            return await _userRepository.CreateUserAsync(userEnity, user.Password!);
        }

        public async Task<bool> UpdateUserAsync(UpdateApplicationUserRequestDto user)
        {
            var validationResult = await _updateApplicationUserRequestDtoValidator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                var errorValidations = validationResult.Errors.Select(error => new ErrorValidation(error.PropertyName, error.ErrorMessage))
                                             .ToList();

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el username en la base de datos
            var userUsername = await _userRepository.GetUserByUsernameAsync(user.UserName!);

            //si obtuvo un usuario con el mismo username, valido que no sea el mismo usuario que se esta actualizando
            if (userUsername != null && userUsername.Id != user.Id!)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(user.UserName), "El nombre de usuario ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            //Valido que no exista el email en la base de datos
            var userEmail = await _userRepository.GetUserByEmailAsync(user.Email!);

            //si obtuvo un usuario con el mismo email, valido que no sea el mismo usuario que se esta actualizando
            if (userEmail != null && userEmail.Id != user.Id!)
            {
                List<ErrorValidation> errorValidations = new List<ErrorValidation>();

                errorValidations.Add(new ErrorValidation(nameof(user.Email), "El email ya existe en el sistema."));

                throw new ValidationException(errorValidations);
            }

            var userEntity = await _userRepository.GetUserByIdAsync(user.Id!);

            userEntity = _mapper.Map(user, userEntity);

            return await _userRepository.UpdateUserAsync(userEntity!);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        #endregion

        #region Roles

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            return await _userRepository.GetRolesAsync(userId);
        }

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
            return await _userRepository.AddToRoleAsync(userId, roleName);
        }

        public Task<bool> AddToRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            return _userRepository.AddToRolesAsync(userId, roleNames);
        }

        public async Task<bool> RemoveFromRoleAsync(string userId, string roleName)
        {
            return await _userRepository.RemoveFromRoleAsync(userId, roleName);
        }

        public async Task<bool> RemoveFromRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            return await _userRepository.RemoveFromRolesAsync(userId, roleNames);
        }

        #endregion
    }
}
