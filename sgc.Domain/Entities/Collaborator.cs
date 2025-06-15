using sgc.Domain.Entities.Handlers;
using System.Net;
using System.Text.RegularExpressions;

namespace sgc.Domain.Entities;

public class Collaborator : Entity
{
    private const int MinPasswordLength = 8;
    private const int MaxUsernameLength = 50;
    private const int MaxNameLength = 100;
    private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private readonly List<string> _alertMessages = [];

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    private RoleEnum _roleId;
    public RoleEnum RoleId
    {
        get => _roleId;
        private set => _roleId = value;
    }
    public string Avatar { get; private set; } = string.Empty;
    public DateTime? DeactivatedAt { get; private set; }

    public ResultData<Collaborator> Create(string username, string pass, string name, string email, RoleEnum role = RoleEnum.Client)
    {
        var statusCode = HttpStatusCode.BadRequest;

        if (!IsValidUsername(username)) return ResultData<Collaborator>.Failure(_alertMessages[0], statusCode);
        if (!IsValidName(name)) return ResultData<Collaborator>.Failure(_alertMessages[0], statusCode);
        if (!IsValidEmail(email)) return ResultData<Collaborator>.Failure(_alertMessages[0], statusCode);
        if (!IsValidPassword(pass)) return ResultData<Collaborator>.Failure(_alertMessages[0], statusCode);

        Id = Guid.NewGuid();
        Username = username;
        Name = name;
        Email = email;
        RoleId = role;
        Password = PasswordHandler.HashPassword(pass);

        return ResultData<Collaborator>.Success(this);
    }

    public void UpdateInfo(string name, string email)
    {
        Name = name;
        Email = email;
        Update();
    }

    public void ChangeRole(RoleEnum newRole)
    {
        RoleId = newRole;
        Update();
    }

    public void UpdateAvatar(string avatarPath)
    {
        Avatar = avatarPath;
        Update();
    }

    private bool IsValidUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            _alertMessages.Add("Username não pode ser vazio");
            return false;
        }

        if (username.Length > MaxUsernameLength)
        {
            _alertMessages.Add($"Username não pode exceder {MaxUsernameLength} caracteres");
            return false;
        }

        return true;
    }

    private bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _alertMessages.Add("Nome não pode ser vazio");
            return false;
        }

        if (name.Length > MaxNameLength)
        {
            _alertMessages.Add($"Nome não pode exceder {MaxNameLength} caracteres");
            return false;
        }

        return true;
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            _alertMessages.Add("Email não pode ser vazio");
            return false;
        }

        if (!Regex.IsMatch(email, EmailPattern))
        {
            _alertMessages.Add("Email inválido");
            return false;
        }

        return true;
    }

    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            _alertMessages.Add("Senha não pode ser vazia");
            return false;
        }

        if (password.Length < MinPasswordLength)
        {
            _alertMessages.Add($"Senha deve conter pelo menos {MinPasswordLength} caracteres");
            return false;
        }

        return true;
    }
}
