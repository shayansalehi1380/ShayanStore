using MediatR;

namespace Application.Users.v1.Commands.LoginAdmin;

public class LoginAdminCommand:IRequest<string>
{
    public string Password { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;
}