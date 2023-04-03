using ApplicationCore.Entities.UserAggregate;
using MediatR;
using Users.Api.Models;

namespace Users.Api.Features.Commands;

public class AddUserCommand : IRequest<UserDto>
{
    public AddUserCommand(string name, string email, string address, string phone, UserType type, decimal money)
    {
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
        Type = type;
        Money = money;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    public UserType Type { get; private set; }
    public decimal Money { get; private set; }
}
