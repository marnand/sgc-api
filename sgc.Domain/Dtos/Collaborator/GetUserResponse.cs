using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.Collaborator;

public class GetUserResponse
{
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
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public GetUserResponse Mapping(Entities.Collaborator user)
    {
        Id = user.Id;
        Name = user.Name;
        Username = user.Username;
        Email = user.Email;
        RoleId = user.RoleId;
        Avatar = user.Avatar;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
        return this;
    }
}
