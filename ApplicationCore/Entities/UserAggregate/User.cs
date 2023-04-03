using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.UserAggregate;

public class User : EntityBase
{
    public string Name { get; private set; }

    [EmailAddress]
    public string Email { get; private set; }

    public string Address { get; private set; }

    [Phone]
    public string Phone { get; private set; }

    public UserType Type { get; private set; }
    public decimal Money { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public User(string name, string email, string address, string phone, UserType type, decimal money)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(name, nameof(email));
        Guard.Against.NullOrWhiteSpace(name, nameof(address));
        Guard.Against.NullOrWhiteSpace(name, nameof(phone));
        Guard.Against.EnumOutOfRange(type, nameof(type));
        Guard.Against.OutOfRange(money, nameof(money), 0, decimal.MaxValue);
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
        Type = type;
        Money = money;
    }

    public void AddGift(decimal gift)
    {
        Money += gift;
    }
}
